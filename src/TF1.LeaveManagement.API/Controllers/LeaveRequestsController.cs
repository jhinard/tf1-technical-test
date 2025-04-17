using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TF1.LeaveManagement.API.Dtos;
using TF1.LeaveManagement.Application.LeaveRequests;

namespace TF1.LeaveManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/leave-requests")]
    public class LeaveRequestsController : Controller
    {
        private readonly LeaveRequestService _leaveRequestService;

        public LeaveRequestsController(LeaveRequestService service)
        {
            _leaveRequestService = service;
        }

        /// <summary>
        /// Retrieves all leave requests.
        /// </summary>
        /// <returns>List of leave requests</returns>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> GetAll()
        {
            var requests = await _leaveRequestService.GetAllAsync();
            var result = requests.Select(r => new LeaveRequestDto
            {
                Id = r.Id,
                EmployeeId = r.EmployeeId,
                StartDate = r.Period.Start,
                EndDate = r.Period.End,
                Type = r.Type.ToString(),
                Comment = r.Comment,
                Status = r.Status.ToString(),
                ManagerComment = r.ManagerComment,
                CreatedAt = r.CreatedAt
            }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Submits a new leave request.
        /// </summary>
        /// <param name="model">Leave request data</param>
        /// <returns>Identifier of the created request</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Submit([FromBody] SubmitLeaveRequestDto model)
        {
            var id = await _leaveRequestService.SubmitRequest(model.EmployeeId!.Value, model.StartDate!.Value, model.EndDate!.Value, model.Type!.Value, model.Comment);
            return Ok(new { Id = id });
        }

        /// <summary>
        /// Approves a leave request.
        /// </summary>
        /// <param name="id">Identifier of the leave request</param>
        /// <param name="model">Manager's comment</param>
        /// <returns></returns>
        [HttpPost("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Approve(Guid id, [FromBody] ManagerActionDto model)
        {
            await _leaveRequestService.ApproveRequest(id, model.Comment);
            return NoContent();
        }

        /// <summary>
        /// Rejects a leave request.
        /// </summary>
        /// <param name="id">Identifier of the leave request</param>
        /// <param name="model">Manager's comment</param>
        /// <returns></returns>
        [HttpPost("{id}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Reject(Guid id, [FromBody] ManagerActionDto model)
        {
            await _leaveRequestService.RejectRequest(id, model.Comment);
            return NoContent();
        }

        [NonAction]
        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var errors = modelStateDictionary
                .Where(kvp => kvp.Value?.Errors.Count > 0)
                .Select(kvp => new
                {
                    Field = kvp.Key,
                    Errors = kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                });

            return BadRequest(new { Message = "Validation failed", Errors = errors });
        }
    }
}