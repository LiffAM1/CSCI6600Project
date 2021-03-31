using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI6600Project.Responses
{
    public abstract class ResponseBase
    {
        public void CopyProperties(object model, object response,List<string> skip=null)
        {
            var modelProps = model.GetType().GetProperties();
            foreach (var responseProp in response.GetType().GetProperties())
            {
                if (skip != null && skip.Contains(responseProp.Name)) continue;
                var modelProp = modelProps.Where(m => m.Name == responseProp.Name).FirstOrDefault();
                if (modelProp != null)
                    responseProp.SetValue(response, modelProp.GetValue(model));
            }
        }

    }
}
