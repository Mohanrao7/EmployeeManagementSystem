using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApi.Models.Dto
{
    public class DepartmentDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }
        
    }
}
