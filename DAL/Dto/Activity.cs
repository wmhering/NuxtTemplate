using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuxtTemplate.DAL.Dto
{
    public class Activity
    {
        [Key, Column("ActivityKey")]
        public int? Id { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}
