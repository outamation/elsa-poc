using Elsa.Services;

namespace Elsa.Workflows.CustomActivities.Signals.Bookmark
{
    public class VIABookmark : IBookmark
    {
        public string Signal { get; set; } = default!;
    }
}