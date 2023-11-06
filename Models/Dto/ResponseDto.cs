namespace EmployeeManagementApi.Models.Dto
{
    public class ResponseDto
    {
        public Guid EmployeeId { get; set; }
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set;}
    }
}
