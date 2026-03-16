using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationMVC.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string DepartmentName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}




























