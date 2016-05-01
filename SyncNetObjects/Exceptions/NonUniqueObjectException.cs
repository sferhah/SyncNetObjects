using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Exceptions
{
    public class NonUniqueObjectException : SyncException
    {
        public NonUniqueObjectException(string type_name, string value_of_key)
            : base(type_name, value_of_key)
        {

        }
    }
}
