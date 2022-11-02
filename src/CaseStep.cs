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
    [Trigger(Category = "VIA", DisplayName = "Referral received", Description = "Suspend workflow execution until the specified signal is received.", Outcomes = new string[] { "Done" })]
    public class ReferralReceived : CaseStep
    {
        [ActivityInput(Hint = "Select target date.", DefaultValue = "11/7/2022")]
        public string? TargetDate { get; set; }

        //public ReferralReceived()
        //{
        //    this.Signal = nameof(ReferralReceived);
        //}
    }

    [Trigger(Category = "VIA", DisplayName = "File received", Description = "Suspend workflow execution until the specified signal is received.", Outcomes = new string[] { "Done" })]
    public class FileReceived : CaseStep
    {
        [ActivityInput(Hint = "The name of the signal to wait for.", DefaultValue = nameof(FileReceived), IsReadOnly = true, SupportedSyntaxes = new[] { SyntaxNames.JavaScript, SyntaxNames.Liquid })]
        public new string Signal
        {
            get { return base.Signal; }
            set { base.Signal = value; }
        }
    }
}
