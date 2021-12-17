using System;
using System.Collections.Generic;
using System.Text;

namespace NuxtTemplate.BLL.Results
{
    public class ListResult<T> : EmptyResult
    {
        public List<T> Data { get; set; }

        public int? RecordOffset { get; set; } = null;

        public int? TotalRecords { get; set; } = null;
    }
}
