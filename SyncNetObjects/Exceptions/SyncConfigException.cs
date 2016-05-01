using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferhah.SyncNetObjects.Exceptions
{
    public class SyncConfigException : SyncException
    {
        public String ClassName { get; private set; }
        public String PropertyName { get; private set; }
        public String Description { get; private set; }

        public SyncConfigException(string className, string propertyName, string message)
            : base(className, propertyName + message)
        {
            ClassName = className;
            PropertyName = propertyName;
            Description = message;
        }
    }

  

 
}
