using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OrganizationMVC.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(100)]
        [Display(Name = "Department Name")]
        [Remote("IsDepartmentNameAvailable", "Departments", ErrorMessage = "Department name already exists")]
        public string DepartmentName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}




























