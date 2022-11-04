import { HttpClient } from '@angular/common/http';
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
  workflowStart = "/cms/fcfha"
  worflowInstanceId = ""
  worflowProgressUrl = this.workflowURL + "/workflow-instances"
  title = 'Workflow';
  menuItems: MenuItem[] = [];
  stepItems: MenuItem[] = [];
  caseStepTasks: any = [];
  activeIndex = 0
  caseCompleted = false;
  case: any = {
    File: "Test-File-XYZ",
    FileType: "Default Servicing",
    InvType: "FHA",
    Loan: "123456789",
    Case: "9876543",
    CaseType: "Foreclosure",
    User: "Hawkins, Sam",
    Atty: "Hawkins, Sam",
    Address: "12529 State Road 535. Ste 531",
    City: "Orlando",
    County: "Bay County",
    State: "Florida",
    Zip: "32836"
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
      { label: 'Create Case' },
      { label: 'Case Steps' },
      { label: 'Complete' }
    ];

    this.menuItems = [{
      label: 'Workflow ',
      items: [
        { label: 'New Case', icon: 'pi pi-fw pi-plus' },
        { label: 'Workflow', icon: 'pi pi-fw pi-sitemap', url: this.workflowURL + "/workflow-instances" }
      ]
    }, {
      label: 'Administration',
      items: [
        { label: 'Configuration', icon: 'pi pi-fw pi-wrench' },
        { label: 'Fees', icon: 'pi pi-fw pi-dollar' },
        { label: 'Users', icon: 'pi pi-fw pi-users' }
      ]
    }, {
      label: 'Accounting',
      items: [
        { label: 'Onboarding', icon: 'pi pi-fw pi-user-plus' },
        { label: 'Cost Payables ', icon: 'pi pi-fw pi-paypal' },
        { label: 'Deposits', icon: 'pi pi-fw pi-dollar' }, { label: 'Vendor Fees', icon: 'pi pi-fw pi-dollar' }
      ]
    },
    {
      label: 'Case Management',
      items: [
        { label: 'Case Report', icon: 'pi pi-fw pi-print' },
        { label: 'Case Docs', icon: 'pi pi-fw pi-verified' },
        { label: 'Messages', icon: 'pi pi-fw pi-inbox' },
        { label: 'Notes', icon: 'pi pi-fw pi-file-edit' }
      ]
    }, {
      label: 'Reporting ',
      items: [
        { label: 'Design', icon: 'pi pi-fw pi-palette' },
        { label: 'Case Report', icon: 'pi pi-fw pi-slack' }
      ]
    },];

    this.setCaseStepTasks();
  }
  setCaseStepTasks() {
    this.caseStepTasks = [
      { stepName: 'File Referred To Attorney', stepSignal: 'FileReferred', disabled: false },
      { stepName: 'File Received By Attorney', stepSignal: 'FileReceived', disabled: false },
      { stepName: 'FC Title Ordered', stepSignal: 'FCTitleOrdered', disabled: false },
      { stepName: 'FC SCRA Eligibility Review', stepSignal: 'FCSCRAEligibilityReview', disabled: false },
      { stepName: 'Title Report Received', stepSignal: 'TitleReportReceived', disabled: true },
      { stepName: 'Preliminary Title Clear', stepSignal: 'PreliminaryTitleClear', disabled: true },
      { stepName: 'Complaint Filed', stepSignal: 'ComplaintFiled', disabled: true },
      // { stepName: 'HUD First Action Expires', stepSignal: 'HUDFirstActionExpires', disabled: true },
    ];
  }

  reforecastStepTasks() {
    this.httpGet(this.workflowStart);
    this.setCaseStepTasks();
    this.overDueSteps();
  }
  caseCreate() {
    if (this.case.Case) {
      if (!this.worflowInstanceId) {
        this.httpGet(this.workflowStart);
        this.showSuccess("Case is created successfully, Please proceed with case steps.");
      }
      this.overDueSteps();
      this.next();
    } else {
      this.showError("Case Number is required.");
    }
  }

  overDueSteps() {
    setTimeout(() => {
      this.caseStepTasks.map((x: { stepName: string; stepSignal: string; completed: boolean; disabled: boolean; overdue: boolean; }) => {
        if (x.stepSignal === 'FCSCRAEligibilityReview' && !x.stepName.includes('Complete')) {
          x.stepName = x.stepName.replace(' - OverDue', '') + ' - OverDue';
          x.overdue = true;
        }
      });
    }, 60 * 1000);
  }
  caseStepsComplete() {
    this.isWorkflowComplete();
  }

  caseComplete() {
    this.caseCompleted = true;
    this.showSuccess("The case is now completed.");
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
      this.worflowInstanceId = data;
      console.log(this.worflowInstanceId);
      this.worflowProgressUrl = this.workflowURL + "/workflow-instances/" + this.worflowInstanceId;
    })
  }

  isWorkflowComplete() {
    this.http.get(this.workflowURL + "/v1/workflow-instances/" + this.worflowInstanceId)
      .subscribe((data: any) => {
        console.log(data);
        if (data.workflowStatus === "Finished") {
          if (!this.caseCompleted) {
            this.showSuccess("All the Case steps are Complete, the case can be marked complete.");
          }
          this.next();
        } else {
          this.showError("Before Case Completion, Please make sure that all case steps are Complete.");
        }
      });
  }

  httpPost(step: any) {
    console.log(step);

    this.http.post<any>(`${this.workflowURL}/v1/custom-signals/${step.stepSignal}/execute`, { workflowInstanceId: this.worflowInstanceId })
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