using System.Collections.Generic;
using System.Linq;

namespace NuxtTemplate.BLL.Common.Results
{
    public class EmptyResult
    {
        public virtual bool HasErrors =>
            Messages.Any(m => m.Status == ResultStatus.Error);

        public List<ResultMessage> Messages { get; } = new List<ResultMessage>();

        public Dictionary<string, object> Values { get; private set; } = null;

        public void Error(string message, string property = null)
        {
            Messages.Add(new ResultMessage { Status = ResultStatus.Error, Message = message, Property = property });
        }

        public void Information(string message, string property = null)
        {
            Messages.Add(new ResultMessage { Status = ResultStatus.Information, Message = message, Property = property });
        }

        public void Value(string name, object value)
        {
            if (Values == null)
                Values = new Dictionary<string, object>();
            Values[name] = value;
        }
        public void Warning(string message, string property = null)
        {
            Messages.Add(new ResultMessage { Status = ResultStatus.Warning, Message = message, Property = property });
        }
    }
}
