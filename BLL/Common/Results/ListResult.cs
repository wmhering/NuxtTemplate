using System;
using System.Collections.Generic;
using System.Text;

namespace NuxtTemplate.BLL.Common.Results
{
    public class ListResult<T> : EmptyResult
    {
        public List<T> Data { get; set; }
    }
}
