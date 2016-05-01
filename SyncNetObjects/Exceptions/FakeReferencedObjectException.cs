using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Exceptions
{
    public class FakeReferencedObjectException : SyncException
    {
        public FakeReferencedObjectException(string type_name, string type_of_ref, string value_of_key)
            : base(type_name, value_of_key)
        {

        }
    }
}
