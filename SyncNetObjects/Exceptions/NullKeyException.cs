using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Exceptions
{
    public class NullKeyException : SyncException
    {
        public String ClassName { get; private set; }
        public String PropertyName { get; private set; }
        public String Description { get; private set; }

        public NullKeyException(string className, string propertyName)
            : base(className, propertyName)
        {
            ClassName = className;
            PropertyName = propertyName;
        }
    }    

}
