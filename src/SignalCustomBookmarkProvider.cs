using Elsa.Activities.Signaling;
using Elsa.Activities.Signaling.Services;
using Elsa.Services;
using ElsaQuickstarts.Server.DashboardAndServer;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Elsa.Workflows.CustomActivities.Signals.Bookmark
{
    public class ReferralReceivedBookmarkProvider : BookmarkProvider<ReferralReceivedBookmark, ReferralReceived>
    {
        public override async ValueTask<IEnumerable<BookmarkResult>> GetBookmarksAsync(BookmarkProviderContext<ReferralReceived> context, CancellationToken cancellationToken) => await GetBookmarksInternalAsync(context, cancellationToken).ToListAsync(cancellationToken);

        private async IAsyncEnumerable<BookmarkResult> GetBookmarksInternalAsync(BookmarkProviderContext<ReferralReceived> context, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var signalName = (await context.ReadActivityPropertyAsync(x => x.Signal, cancellationToken))?.ToLowerInvariant().Trim();

            // Can't do anything with an empty signal name.
            if (string.IsNullOrEmpty(signalName))
                yield break;

            yield return Result(new ReferralReceivedBookmark
            {
                Signal = signalName
            });
        }
    }

    public class FileReceivedBookmarkProvider : BookmarkProvider<FileReceivedBookmark, FileReceived>
    {
        public override async ValueTask<IEnumerable<BookmarkResult>> GetBookmarksAsync(BookmarkProviderContext<FileReceived> context, CancellationToken cancellationToken) => await GetBookmarksInternalAsync(context, cancellationToken).ToListAsync(cancellationToken);

        private async IAsyncEnumerable<BookmarkResult> GetBookmarksInternalAsync(BookmarkProviderContext<FileReceived> context, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var signalName = (await context.ReadActivityPropertyAsync(x => x.Signal, cancellationToken))?.ToLowerInvariant().Trim();

            // Can't do anything with an empty signal name.
            if (string.IsNullOrEmpty(signalName))
                yield break;

            yield return Result(new FileReceivedBookmark
            {
                Signal = signalName
            });
        }
    }

    public class TitleOrderedBookmarkProvider : BookmarkProvider<TitleOrderedBookmark, TitleOrdered>
    {
        public override async ValueTask<IEnumerable<BookmarkResult>> GetBookmarksAsync(BookmarkProviderContext<TitleOrdered> context, CancellationToken cancellationToken) => await GetBookmarksInternalAsync(context, cancellationToken).ToListAsync(cancellationToken);

        private async IAsyncEnumerable<BookmarkResult> GetBookmarksInternalAsync(BookmarkProviderContext<TitleOrdered> context, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var signalName = (await context.ReadActivityPropertyAsync(x => x.Signal, cancellationToken))?.ToLowerInvariant().Trim();

            // Can't do anything with an empty signal name.
            if (string.IsNullOrEmpty(signalName))
                yield break;

            yield return Result(new TitleOrderedBookmark
            {
                Signal = signalName
            });
        }
    }
}