using System;
using System.Collections.Generic;
using System.Text;

namespace NuxtTemplate.BLL.Results
{
    public class ResultMessage
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; } = "";
        public string Property { get; set; } = null;
    }
}
