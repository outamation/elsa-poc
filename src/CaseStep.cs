using Elsa;
using Elsa.Activities.Email;
using Elsa.Activities.Email.Options;
using Elsa.Activities.Email.Services;
using Elsa.Activities.Http;
using Elsa.Activities.Signaling;
using Elsa.Activities.Signaling.Models;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Serialization;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace ElsaQuickstarts.Server.DashboardAndServer
{


    public class CaseStep : SignalReceived
    {
        [ActivityInput(Hint = "Select target date.", DefaultValue = "Target Date")]
        public string? TargetDate { get; set; }
    }

    //[Activity(Category = "VIA", DisplayName = "Referral received")]
    [Trigger(Category = "VIA", DisplayName = "File Referred To Attorney", Description = "", Outcomes = new string[] { "Done" })]
    public class FileReferred : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(FileReferred), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "File Received By Attorney", Description = "", Outcomes = new string[] { "Done" })]
    public class FileReceived : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(FileReceived), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "FC Title Ordered", Description = "", Outcomes = new string[] { "Done" })]
    public class FCTitleOrdered : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(FCTitleOrdered), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "FC SCRA Eligibility Review", Description = "", Outcomes = new string[] { "Done" })]
    public class FCSCRAEligibilityReview : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(FCSCRAEligibilityReview), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Title Report Received", Description = "", Outcomes = new string[] { "Done" })]
    public class TitleReportReceived : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(TitleReportReceived), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Preliminary Title Clear", Description = "", Outcomes = new string[] { "Done" })]
    public class PreliminaryTitleClear : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(PreliminaryTitleClear), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Complaint Filed", Description = "", Outcomes = new string[] { "Done" })]
    public class ComplaintFiled : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(ComplaintFiled), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Approve", Description = "", Outcomes = new string[] { "Done" })]
    public class Approve : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(Approve), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Reject", Description = "", Outcomes = new string[] { "Done" })]
    public class Reject : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(Reject), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Document Received", Description = "", Outcomes = new string[] { "Done" })]
    public class DocumentReceived : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(DocumentReceived), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Quality Control", Description = "", Outcomes = new string[] { "Done" })]
    public class QualityControl : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(QualityControl), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }
}
