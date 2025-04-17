using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Submit([FromBody] SubmitLeaveRequestDto dto)
        {
            var id = await _leaveRequestService.SubmitRequest(dto.EmployeeId, dto.StartDate, dto.EndDate, dto.Type, dto.Comment);
            return Ok(new { Id = id });
        }

        [HttpPost("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Approve(Guid id, [FromBody] ManagerActionDto dto)
        {
            await _leaveRequestService.ApproveRequest(id, dto.Comment);
            return NoContent();
        }

        [HttpPost("{id}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Reject(Guid id, [FromBody] ManagerActionDto dto)
        {
            await _leaveRequestService.RejectRequest(id, dto.Comment);
            return NoContent();
        }
    }
}