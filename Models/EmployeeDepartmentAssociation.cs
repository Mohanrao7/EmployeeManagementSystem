using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementApi.Models
{
    public class EmployeeDepartmentAssociation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Employee Id is required.")]
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Department Id is required.")]
        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedOn { get; set; }
    }
}
