using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects
{
    public class SyncProperty
    {
        public PropertyInfo PropertyInfo { get; set; }
        public SyncConfiguration Configuration { get; set; }


        public SyncProperty(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
        }

        public Object GetValueFor(Object o)
        {
            return this.PropertyInfo.GetMethod.Invoke(o, null);
        }

        public void SetValueFor(Object o, Object value)
        {
            this.PropertyInfo.SetMethod.Invoke(o, new object[] { value });
        }
    }
}
