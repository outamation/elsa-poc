using Elsa;
using Elsa.Activities.Http;
using Elsa.Activities.Signaling;
using Elsa.Activities.Signaling.Models;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Services;
using Elsa.Services.Models;

namespace ElsaQuickstarts.Server.DashboardAndServer
{


    public class CaseStep : SignalReceived
    {
        [ActivityInput(Hint = "Select target date.", DefaultValue = "This must NOT show up.")]
        public string? TargetDate { get; set; }
    }

    //[Activity(Category = "VIA", DisplayName = "Referral received")]
    [Trigger(Category = "VIA", DisplayName = "File Referred To Attorney", Description = "Suspend workflow execution until the specified signal is received.", Outcomes = new string[] { "Done" })]
    public class FileReferred : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(FileReferred), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "File Received By Attorney", Description = "Suspend workflow execution until the specified signal is received.", Outcomes = new string[] { "Done" })]
    public class FileReceived : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(FileReceived), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "FC Title Ordered", Description = "Suspend workflow execution until the specified signal is received.", Outcomes = new string[] { "Done" })]
    public class FCTitleOrdered : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(FCTitleOrdered), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "FC SCRA Eligibility Review", Description = "Suspend workflow execution until the specified signal is received.", Outcomes = new string[] { "Done" })]
    public class FCSCRAEligibilityReview : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(FCSCRAEligibilityReview), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Title Report Received", Description = "Suspend workflow execution until the specified signal is received.", Outcomes = new string[] { "Done" })]
    public class TitleReportReceived : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(TitleReportReceived), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Preliminary Title Clear", Description = "Suspend workflow execution until the specified signal is received.", Outcomes = new string[] { "Done" })]
    public class PreliminaryTitleClear : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(PreliminaryTitleClear), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }

    [Trigger(Category = "VIA", DisplayName = "Complaint Filed", Description = "Suspend workflow execution until the specified signal is received.", Outcomes = new string[] { "Done" })]
    public class ComplaintFiled : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(ComplaintFiled), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }
}
