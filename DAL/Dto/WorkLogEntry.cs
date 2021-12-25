using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuxtTemplate.DAL.Dto
{
    public class WorkLogEntry
    {
        [Key, Column("WorkLogEntryKey")]
        public int Id { get; set; }

        [ForeignKey(nameof(Employee)), Column("EmployeeKey")]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }

        [ForeignKey(nameof(Activity)), Column("ActivityKey")]
        public int ActivityId { get; set; }

        [ForeignKey(nameof(ActivityId))]
        public Activity Activity { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Comment { get; set; }
    }
}
