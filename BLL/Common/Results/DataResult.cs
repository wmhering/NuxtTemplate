using System;
using System.Collections.Generic;
using System.Text;

namespace NuxtTemplate.BLL.Common.Results
{
    public class DataResult<T> : EmptyResult
    {
        public T Data { get; set; }
    }
}
