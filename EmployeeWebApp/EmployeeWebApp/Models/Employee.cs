using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeWebApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Employee Name")]
        public string Name { get; set; }

        [DisplayName("Position")]
        public string Position { get; set; }

        [DisplayName("Department")]
        public string Department { get; set; }
    }
}
