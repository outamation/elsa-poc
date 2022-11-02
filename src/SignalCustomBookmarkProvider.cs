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
    public class SignalCustomBookmarkProvider : BookmarkProvider<SignalCustomBookmark, ReferralReceived>
    {
        public override async ValueTask<IEnumerable<BookmarkResult>> GetBookmarksAsync(BookmarkProviderContext<ReferralReceived> context, CancellationToken cancellationToken) => await GetBookmarksInternalAsync(context, cancellationToken).ToListAsync(cancellationToken);

        private async IAsyncEnumerable<BookmarkResult> GetBookmarksInternalAsync(BookmarkProviderContext<ReferralReceived> context, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var signalName = (await context.ReadActivityPropertyAsync(x => x.Signal, cancellationToken))?.ToLowerInvariant().Trim();

            // Can't do anything with an empty signal name.
            if (string.IsNullOrEmpty(signalName))
                yield break;

            yield return Result(new SignalCustomBookmark
            {
                Signal = signalName
            });
        }
    }
}