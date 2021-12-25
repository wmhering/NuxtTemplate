using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuxtTemplate.DAL.Dto
{
    public class Employee
    {
        [Key, Column("EmployeeKey")]
        public int? Id { get; set; }

        [ForeignKey(nameof(ChangedByEmployee)), Column("ChangedBy_EmployeeKey")]
        public int? ChangedByEmployeeId { get; set; }

        [ForeignKey(nameof(ChangedByEmployeeId))]
        public Employee ChangedByEmployee { get; set; }

        public string SamAccountName { get; set; }

        public string UserPrincipalName { get; set; }

        public string? Email { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public string? WorkPhone { get; set; }

        public bool IsSupervisor { get; set; }

        public bool IsDirector { get; set; }

        public bool IsProvisional { get; set; } = true;
    }
}
