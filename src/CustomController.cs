using DotLiquid;
using Elsa.Activities.Signaling;
using Elsa.Activities.Signaling.Models;
using Elsa.Activities.Signaling.Services;
using Elsa.Models;
using Elsa.Server.Api.ActionFilters;
using Elsa.Server.Api.Endpoints.Signals;
using Elsa.Server.Api.Services;
using Elsa.Services.Models;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Open.Linq.AsyncExtensions;
using Swashbuckle.AspNetCore.Annotations;

namespace ElsaQuickstarts.Server.DashboardAndServer
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{apiVersion:apiVersion}/custom-signals/{signalName}/execute")]
    [Produces("application/json")]
    public class Execute : Controller
    {
        private readonly ICustomSignaler _signaler;
        private readonly IEndpointContentSerializerSettingsProvider _serializerSettingsProvider;

        public Execute(ICustomSignaler signaler, IEndpointContentSerializerSettingsProvider serializerSettingsProvider)
        {
            _signaler = signaler;
            _serializerSettingsProvider = serializerSettingsProvider;
        }

        [HttpPost]
        [ElsaJsonFormatter]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExecuteSignalResponse))]
        [SwaggerOperation(
            Summary = "Signals all workflows waiting on the specified signal name synchronously.",
            Description = "Signals all workflows waiting on the specified signal name synchronously.",
            OperationId = "Signals.Execute",
            Tags = new[] { "Signals" })
        ]
        public async Task<IActionResult> Handle(string signalName, ExecuteSignalRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _signaler.TriggerSignalAsync(signalName, request.Input, request.WorkflowInstanceId, request.CorrelationId, cancellationToken).ToList();

            if (Response.HasStarted)
                return new EmptyResult();

            return Json(
                new ExecuteSignalResponse(result.Select(x => new CollectedWorkflow(x.WorkflowInstanceId, x.WorkflowInstance, x.ActivityId)).ToList()),
                _serializerSettingsProvider.GetSettings());
        }

        [HttpGet]
        [ElsaJsonFormatter]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExecuteSignalResponse))]
        [SwaggerOperation(
            Summary = "Signals all workflows waiting on the specified signal name synchronously.",
            Description = "Signals all workflows waiting on the specified signal name synchronously.",
            OperationId = "Signals.Execute",
            Tags = new[] { "Signals" })
        ]
        public async Task<IActionResult> Handle(string signalName, string workflowInstanceId, CancellationToken cancellationToken = default)
        {
            ExecuteSignalRequest request = new ExecuteSignalRequest { WorkflowInstanceId = workflowInstanceId };

            var result = await _signaler.TriggerSignalAsync(signalName, request.Input, request.WorkflowInstanceId, request.CorrelationId, cancellationToken).ToList();

            if (Response.HasStarted)
                return new EmptyResult();

            return Json(
                new ExecuteSignalResponse(result.Select(x => new CollectedWorkflow(x.WorkflowInstanceId, x.WorkflowInstance, x.ActivityId)).ToList()),
                _serializerSettingsProvider.GetSettings());
        }
    }
}
