using Ferhah.SyncNetObjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Ferhah.SyncNetObjects
{

    public abstract class SyncConfiguration
    {       

        public List<SyncProperty> Keys { get; set; } = new List<SyncProperty>();
        public List<SyncProperty> KeyNavigationProperties { get; set; } = new List<SyncProperty>();
        public IEnumerable<SyncProperty> AllKeys { get { return KeyNavigationProperties.Concat(this.Keys); } }
        public List<SyncProperty> NavigationProperties { get; set; } = new List<SyncProperty>();
        public IEnumerable<SyncProperty> AllNavigationProperties { get { return KeyNavigationProperties.Concat(this.NavigationProperties); } }
        public List<SyncProperty> PrimitiveProperties { get; set; } = new List<SyncProperty>();
        public List<SyncProperty> ComplexTypes { get; set; } = new List<SyncProperty>();
        public IEnumerable<SyncProperty> UpdatableProperties { get { return PrimitiveProperties.Concat(this.NavigationProperties); } }

        public abstract Type GetGenericType();


        public virtual String GetKeyAsStringValue(Object left)
        {
            List<String> values = new List<string>();

            foreach (var key in this.Keys)
            {
                Object value_object = key.GetValueFor(left);

                if (value_object == null)
                {
                    throw new NullKeyException(GetGenericType().Name, key.PropertyInfo.Name);
                }

                String value_string = value_object.ToString();
                values.Add(value_string.ToUpper());                
            }       

            foreach (var keyNavigationProperties in this.KeyNavigationProperties)
            {
                Object value_object = keyNavigationProperties.GetValueFor(left);

                if (value_object == null)
                {
                    throw new NullKeyException(GetGenericType().Name, keyNavigationProperties.PropertyInfo.Name);
                }

                var keyNavigationConfig = keyNavigationProperties.Configuration;

                if (keyNavigationConfig == null)
                {
                    throw new Exception("");                 
                }

                values.Add(keyNavigationConfig.GetKeyAsStringValue(value_object));
            }
            

            return String.Join("\0", values);
        }


        public virtual bool RequiresUpdate(Object original, Object newest)
        {

            foreach (var updatableProperty in this.UpdatableProperties)
            {
                Object original_value = updatableProperty.GetValueFor(original);
                Object newest_value = updatableProperty.GetValueFor(newest);

                //both null, go to next
                if (original_value == null && newest_value == null)
                {
                    continue;
                }

                //one is null, the other has value
                if ((original_value == null && newest_value != null)
                    || (newest_value == null && original_value != null))
                {
                    return true;
                }

                //none is null, check if the value is different

                var navigationPropertyConfig = updatableProperty.Configuration;

                //primitive property
                if (navigationPropertyConfig == null)
                {
                    if (!String.Equals(original_value.ToString(), newest_value.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }//navigation property
                else
                {
                    String key_1 = navigationPropertyConfig.GetKeyAsStringValue(original_value);
                    String key_2 = navigationPropertyConfig.GetKeyAsStringValue(newest_value);

                    if (!String.Equals(key_1, key_2, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }               
                }

            }


            foreach (var complexType in this.ComplexTypes)
            {
                Object complexType_original = complexType.GetValueFor(original);
                Object complexType_newest = complexType.GetValueFor(newest);

                if(complexType.Configuration.RequiresUpdate(complexType_original, complexType_newest))
                {
                    return true;
                }
            }

            return false;

        }


    }

  
}
