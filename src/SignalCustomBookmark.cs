using Elsa.Services;

namespace Elsa.Workflows.CustomActivities.Signals.Bookmark
{
    public class SignalCustomBookmark : IBookmark
    {
        public string Signal { get; set; } = default!;
    }
}