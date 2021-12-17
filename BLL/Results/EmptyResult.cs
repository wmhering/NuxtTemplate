using System;
using System.Collections.Generic;
using System.Linq;

namespace NuxtTemplate.BLL.Results
{
    public class EmptyResult
    {
        public virtual bool HasErrors =>
            Messages.Any(m => m.Status == ResultStatus.Error);

        public List<ResultMessage> Messages { get; } = new List<ResultMessage>();
    }
}
