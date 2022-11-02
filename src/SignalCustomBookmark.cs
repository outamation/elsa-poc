using Elsa.Services;

namespace Elsa.Workflows.CustomActivities.Signals.Bookmark
{
    public class ReferralReceivedBookmark : IBookmark
    {
        public string Signal { get; set; } = default!;
    }

    public class FileReceivedBookmark : IBookmark
    {
        public string Signal { get; set; } = default!;
    }
}