using System;
using System.Collections.Generic;
using System.Text;

namespace NuxtTemplate.BLL.Common.Results
{
    public class DataResult<T> : EmptyResult
    {
        public bool ConcurrencyError { get; set; }

        public override bool HasErrors =>
            ConcurrencyError || base.HasErrors;

        public T Data { get; set; }
    }
}
