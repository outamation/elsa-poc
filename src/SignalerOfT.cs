﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Activities.Signaling.Models;
using Elsa.Models;
using Elsa.Services;
using Elsa.Services.Models;
using Elsa.Workflows.CustomActivities.Signals.Bookmark;
using ElsaQuickstarts.Server.DashboardAndServer;

namespace Elsa.Activities.Signaling.Services
{
    public interface ICustomSignaler
    {
        Task<IEnumerable<CollectedWorkflow>> TriggerSignalTokenAsync(string signalToken, object? input = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<CollectedWorkflow>> TriggerSignalAsync(string signal, object? input = null, string? workflowInstanceId = null, string? correlationId = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<CollectedWorkflow>> DispatchSignalTokenAsync(string token, object? input = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<CollectedWorkflow>> DispatchSignalAsync(string signal, object? input = null, string? workflowInstanceId = null, string? correlationId = null, CancellationToken cancellationToken = default(CancellationToken));
    }

    public class CustomSignaler : Signaler, ICustomSignaler
    {
        /// <summary>
        /// (Immutable) the workflow launchpad.
        /// </summary>
        private readonly IWorkflowLaunchpad _workflowLaunchpad;

        /// <summary>
        /// (Immutable) the token service.
        /// </summary>
        private readonly ITokenService _tokenService;

        /// <summary>
        /// (Immutable) the tenant accessor.
        /// </summary>
        private readonly ITenantAccessor _tenantAccessor;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="workflowLaunchpad"> The workflow launchpad.</param>
        /// <param name="tokenService"> The token service.</param>
        /// <param name="tenantAccessor"> The tenant accessor.</param>
        public CustomSignaler(IWorkflowLaunchpad workflowLaunchpad, ITokenService tokenService, ITenantAccessor tenantAccessor)
            : base(workflowLaunchpad, tokenService)
        {
            _workflowLaunchpad = workflowLaunchpad;
            _tokenService = tokenService;
            _tenantAccessor = tenantAccessor;
        }


        /// <summary>
        /// Trigger signal asynchronous.
        /// </summary>
        /// <param name="signal"> The signal.</param>
        /// <param name="input"> (Optional) The input.</param>
        /// <param name="workflowInstanceId"> (Optional) Identifier for the workflow instance.</param>
        /// <param name="correlationId"> (Optional) Identifier for the correlation.</param>
        /// <param name="cancellationToken"> (Optional) A token that allows processing to be cancelled.</param>
        /// <returns>
        /// The trigger signal.
        /// </returns>
        public new async Task<IEnumerable<CollectedWorkflow>> TriggerSignalAsync(string signal, object? input = default, string? workflowInstanceId = default, string? correlationId = default, CancellationToken cancellationToken = default)
        {
            var normalizedSignal = signal.ToLowerInvariant();

            var tenantId = await _tenantAccessor.GetTenantIdAsync();

            var wf = await _workflowLaunchpad.CollectAndExecuteWorkflowsAsync(new WorkflowsQuery(
                signal,
                new SignalReceivedBookmark { Signal = normalizedSignal },
                correlationId,
                workflowInstanceId,
                default,
                tenantId
            ), new WorkflowInput(new Signal(normalizedSignal, input)), cancellationToken);
            return wf;
        }


        /// <summary>
        /// Dispatch signal asynchronous.
        /// </summary>
        /// <param name="signal"> The signal.</param>
        /// <param name="input"> (Optional) The input.</param>
        /// <param name="workflowInstanceId"> (Optional) Identifier for the workflow instance.</param>
        /// <param name="correlationId"> (Optional) Identifier for the correlation.</param>
        /// <param name="cancellationToken"> (Optional) A token that allows processing to be cancelled.</param>
        /// <returns>
        /// The dispatch signal.
        /// </returns>
        public new async Task<IEnumerable<CollectedWorkflow>> DispatchSignalAsync(string signal, object? input = default, string? workflowInstanceId = default, string? correlationId = default, CancellationToken cancellationToken = default)
        {
            var normalizedSignal = signal.ToLowerInvariant();
            var tenantId = await _tenantAccessor.GetTenantIdAsync();
            var wf = await _workflowLaunchpad.CollectAndDispatchWorkflowsAsync(new WorkflowsQuery(
                    nameof(SignalReceived),
                    new SignalReceivedBookmark { Signal = normalizedSignal },
                    correlationId,
                    workflowInstanceId,
                    default,
                    tenantId
                ),
                new WorkflowInput(new Signal(normalizedSignal, input)),
                cancellationToken);
            return wf;
        }
    }

}
