import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { MenuItem } from 'primeng/api';

import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  workflowURL = "https://localhost:7190";
  workflowID = '8cebbf90aa474c589cac6cf1b8cbb086';
  workflowStart = "/condo"
  workflowInstanceId = ""
  workflowProgressUrl = this.workflowURL + "/workflow-instances"
  title = 'Workflow';
  menuItems: MenuItem[] = [];
  stepItems: MenuItem[] = [];
  caseStepTasks: any = [];
  activeIndex = 0
  caseCompleted = false;
  checkedSwitch = true;
  isQC = false;
  documents: any = [];
  case: any = {
    File: "Test-File-XYZ",
    FileType: "Default Servicing",
    InvType: "FHA",
    Loan: "123456789",
    Case: "9876543",
    OrderID: 'Order-12345',
    CaseType: "Foreclosure",
    User: "Hawkins, Sam",
    ReviewerEmail: 'sam.hawkins@outamation.com',
    Atty: "Hawkins, Sam",
    Address: "12529 State Road 535. Ste 531",
    City: "Orlando",
    County: "Bay County",
    State: "Florida",
    Zip: "32836",
    Email: "borrower@outamation.com",
    FirstName: 'Sam', LastName: 'Hawkins'
  }

  constructor(private primengConfig: PrimeNGConfig,
    private messageService: MessageService,
    private http: HttpClient) { }

  ngOnInit() {
    this.primengConfig.ripple = true;
    this.loadStartupData();
  }

  loadStartupData() {
    this.stepItems = [
      { label: 'Create Order' },
      { label: 'Document Received' },
      { label: 'Document Review' },
      { label: 'Order Fulfilled' }
    ];

    this.menuItems = [{
      label: 'Workflow ',
      items: [
        { label: 'New Order', icon: 'pi pi-fw pi-plus', command: (event) => { location.reload(); } },
        { label: 'Workflow', icon: 'pi pi-fw pi-sitemap', url: this.workflowURL + "/workflow-instances" }
      ]
    },
      // }, {
      //   label: 'Administration',
      //   items: [
      //     { label: 'Configuration', icon: 'pi pi-fw pi-wrench' },
      //     { label: 'Fees', icon: 'pi pi-fw pi-dollar' },
      //     { label: 'Users', icon: 'pi pi-fw pi-users' }
      //   ]
      // }, {
      //   label: 'Accounting',
      //   items: [
      //     { label: 'Onboarding', icon: 'pi pi-fw pi-user-plus' },
      //     { label: 'Cost Payables ', icon: 'pi pi-fw pi-paypal' },
      //     { label: 'Deposits', icon: 'pi pi-fw pi-dollar' }, { label: 'Vendor Fees', icon: 'pi pi-fw pi-dollar' }
      //   ]
      // },
      // {
      //   label: 'Case Management',
      //   items: [
      //     { label: 'Case Report', icon: 'pi pi-fw pi-print' },
      //     { label: 'Case Docs', icon: 'pi pi-fw pi-verified' },
      //     { label: 'Messages', icon: 'pi pi-fw pi-inbox' },
      //     { label: 'Notes', icon: 'pi pi-fw pi-file-edit' }
      //   ]
      // }, {
      //   label: 'Reporting ',
      //   items: [
      //     { label: 'Design', icon: 'pi pi-fw pi-palette' },
      //     { label: 'Case Report', icon: 'pi pi-fw pi-slack' }
      //   ]
      // },
    ];

    this.documents = [
      { name: 'Questionnaire', code: 'NJ', inactive: false },
      { name: 'CC&R', code: 'NY', inactive: false },
      { name: 'Insurance Document', code: 'RM', inactive: false },
      { name: 'Budget', code: 'PRS', inactive: false }
    ];
    this.setCaseSteps();
  }
  setCaseSteps() {
    this.http.get(this.workflowURL + '/v1/workflow-definitions/' + this.workflowID + '/Latest')
      .subscribe((data: any) => {
        console.log(data.activities);
        const excludeSteps = ['Fork', 'Finish', 'Join', 'HttpEndpoint', 'WriteHttpResponse']
        data.activities.map((actvt: any) => {
          if (actvt.type === 'QualityControl') {
            this.isQC = true;
            console.log('isQC is ', this.isQC);
            if (this.isQC) {
              this.stepItems = [
                { label: 'Create Order' },
                { label: 'Document Received' },
                { label: 'Document Review' },
                { label: 'Quality Control' },
                { label: 'Order Fulfilled' }
              ];
            }
            return;
          }
          this.caseStepTasks.push({ stepName: actvt.displayName, stepSignal: actvt.type, disabled: false });
          // }
        })
      })
  }

  orderStep(step: any) {
    console.log(step);
    if (this.caseCompleted) {
      this.next();
    } else {
      this.http.post<any>(`${this.workflowURL}/v1/custom-signals/${step}/execute`, { workflowInstanceId: this.workflowInstanceId })
        .subscribe(data => {
          console.log('Post Response', data);
          if (data.startedWorkflows.length > 0) {
            this.showSuccess(`Changes Saved, Order step is completed successfully.`);
            this.next();
          } else {
            this.showError(`Order step cannot be is completed, please check dependent steps and try again.`);
            this.next();
          }
        });
    }
  }

  orderStepApproveReject(step: any) {
    this.http.get(`${this.workflowURL}/v1/custom-signals/${step}/execute?workflowInstanceId=${this.workflowInstanceId}`,
      { responseType: 'text' }).subscribe(data => {
        if (data.includes('Thanks')) {
          this.showSuccess(`Changes Saved, Order step is completed successfully.`);
          this.next();
        } else {
          this.showError(`Order step cannot be is completed, please check dependent steps and try again.`);
          this.next();
        }
      })
  }

  caseCreate() {
    if (this.case.Loan) {
      if (!this.workflowInstanceId) {
        // { responseType: 'text' }
        // const headers = new HttpHeaders()
        //   .set('Content-Type', 'application/json; charset=utf-8');
        // const options: any = { headers, responseType: 'text' };
        this.http.post<any>(this.workflowURL + this.workflowStart,
          { OrderName: this.case.OrderID, ReviewerName: this.case.User, ReviewerEmail: this.case.ReviewerEmail }) //,options)
          .subscribe((data: any) => {
            this.workflowID = data.workflowDefinitionId
            this.workflowInstanceId = data.workflowInstanceId;
            this.workflowProgressUrl = this.workflowURL + "/workflow-instances/" + this.workflowInstanceId;
            this.showSuccess(`Changes Saved, Order step is completed successfully.`);
            this.next();
          })
      } else {
        this.next();
      }
    } else {
      this.showError("Loan Number is required.");
    }
  }

  caseStepsComplete() {
    this.http.get(this.workflowURL + "/v1/workflow-instances/" + this.workflowInstanceId)
      .subscribe((data: any) => {
        console.log(data);
        if (data.workflowStatus === "Finished") {
          if (!this.caseCompleted) {
            this.showSuccess("Order is completed successfully, All the Case steps are Complete.");
            this.caseCompleted = true;
          }
        } else {
          this.showError("Before Case Completion, Please make sure that all case steps are Complete.");
        }
      });
  }

  back() {
    this.activeIndex = this.activeIndex - 1;
  }
  next() {
    this.activeIndex = this.activeIndex + 1;
  }

  openWorkflow(url: any) {
    window.open(url, "WorkflowProgress");
  }

  showSuccess(msg: any) {
    this.messageService.clear();
    this.messageService.add({ severity: 'success', summary: 'Success', detail: msg });
  }

  showError(msg: any) {
    this.messageService.clear();
    this.messageService.add({ severity: 'error', summary: 'Error', detail: msg });
  }

  httpGet(endpoint: any) {
    this.http.get(this.workflowURL + endpoint, { responseType: 'text' }).subscribe(data => {
      this.workflowInstanceId = data;
      console.log(this.workflowInstanceId);
      this.workflowProgressUrl = this.workflowURL + "/workflow-instances/" + this.workflowInstanceId;
    })
  }

  httpPost(step: any) {
    console.log(step);

    this.http.post<any>(`${this.workflowURL}/v1/custom-signals/${step.stepSignal}/execute`, { workflowInstanceId: this.workflowInstanceId })
      .subscribe(data => {
        console.log('Post Response', data);
        if (data.startedWorkflows.length > 0) {
          this.showSuccess(`Case step '${step.stepName}' is completed successfully.`);
          this.caseStepTasks.map((x: { stepName: string; stepSignal: string; completed: boolean; disabled: boolean; overdue: boolean; }) => {
            if (x.stepSignal === step.stepSignal) {
              x.stepName = x.stepName.replace(' - OverDue', '') + ' - Complete';
              x.completed = true;
              x.overdue = false;
            }
            if (step.stepSignal === 'FCTitleOrdered') {
              x.disabled = false;
              if (x.stepSignal === 'FCSCRAEligibilityReview') {
                x.disabled = true;
              }
            }
            if (step.stepSignal === 'FCSCRAEligibilityReview') {
              if (x.stepSignal === 'FCTitleOrdered') {
                x.disabled = true;
              }
            }
          });
        } else {
          this.showError(`Case step '${step.stepName}' cannot be is completed, please check dependent steps and try again.`);
        }
      })
  }
}