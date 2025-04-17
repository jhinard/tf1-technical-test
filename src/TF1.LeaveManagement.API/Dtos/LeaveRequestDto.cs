namespace TF1.LeaveManagement.API.Dtos
{
    public class LeaveRequestDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ManagerComment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
