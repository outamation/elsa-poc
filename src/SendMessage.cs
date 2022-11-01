using Elsa;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services;
using Elsa.Services.Models;

namespace ElsaQuickstarts.Server.DashboardAndServer
{
    [Activity(
    Category = "Demo",
    DisplayName = "Send message",
    Description = "Sends the specified message.",
    Outcomes = new[] { OutcomeNames.Done }
)]
    public class SendMessage : Activity
    {
        private readonly CrudClient _sender;

        public SendMessage(CrudClient sender)
        {
            _sender = sender;
        }

        [ActivityInput(Hint = "Receiver name.")]
        public string ReceiverName { get; set; } = String.Empty;


        [ActivityInput(Hint = "The message to send.")]
        public string Message { get; set; } = String.Empty;

        [ActivityInput(Hint = "Test boolean field.")]
        public string Flag { get; set; } = String.Empty;

        //[ActivityInput(Hint = "Test date field.")]
        //public DateTime Date { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            try
            {
                await _sender.SendAsync(ReceiverName, Message);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Done();
        }
    }
}
