using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApi.Models
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        //public string[] Roles { get; set; }
    }
}
