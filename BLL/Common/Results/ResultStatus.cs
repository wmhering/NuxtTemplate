using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NuxtTemplate.BLL.Common.Results
{
    public enum ResultStatus
    {
        [Display(Name = "Information")]
        Information,

        [Display(Name = "Warning")]
        Warning,

        [Display(Name = "Error")]
        Error

    }
}
