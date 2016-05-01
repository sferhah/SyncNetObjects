using Ferhah.SyncNetObjects.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects
{
 

    public class SyncConfigSet<T>
    {
      
        public List<SyncConfiguration> Configurations { get; set; } = new List<SyncConfiguration>();
    
        public SyncConfigSet(params SyncConfiguration[] configs)
        {
            foreach(var config in configs)
            {
                Configurations.Add(config);
            }

            BuildConfigs();
        }
        

        public SyncConfiguration GetSingle(Type configType)
        {
            return this.Configurations.Where(cfg => cfg.GetGenericType() == configType).Single();
        }

        public SyncConfiguration GetSingleOrDefault(Type configType)
        {
            return this.Configurations.Where(cfg => cfg.GetGenericType() == configType).SingleOrDefault();
        }

        public void BuildConfigs()
        {
            foreach (SyncConfiguration config in this.Configurations)
            {
                BuildConfig(config);
            }
        }

        public void BuildConfig(SyncConfiguration config)
        {
            foreach (var reference in config.AllNavigationProperties)
            {
                reference.Configuration = GetSingleOrDefault(reference.PropertyInfo.PropertyType);                
            }

            foreach (var complexType in config.ComplexTypes)
            {
                BuildConfig(complexType.Configuration);
            }
        }

        public virtual IndexedCollectionSet<T> IndexColections(T objectSet)
        {  

            List<IndexedCollection> dicos = new List<IndexedCollection>();

            foreach (var config in this.Configurations)
            {             

                PropertyInfo containerProperty = typeof(T).GetProperties().Where(p => typeof(IEnumerable).IsAssignableFrom(p.PropertyType)
                                           && p.CanWrite
                                         && p.PropertyType.GenericTypeArguments.Any()
                                         && p.PropertyType.GenericTypeArguments.First() == config.GetGenericType())
                                        .Single();

                Object lefts = containerProperty.GetMethod.Invoke(objectSet, null);

                Dictionary<String, Object> dicoLefts = GetDico((IEnumerable<Object>)lefts, config);

                dicos.Add(new IndexedCollection (config, dicoLefts, (IList)lefts));
            }

            IndexedCollectionSet<T> indexedCollectionSet = new IndexedCollectionSet<T>
            {
                ObjectSet = objectSet,
                IndexedCollections = dicos,
            };

            return indexedCollectionSet;
        }

        protected Dictionary<String, Object> GetDico(IEnumerable<Object> elements, SyncConfiguration config)
        {
            Dictionary<String, Object> dico = new Dictionary<String, Object>();

            foreach (Object item in elements)
            {
                String key = config.GetKeyAsStringValue(item);

                try
                {
                    dico.Add(key, item);
                }
                catch (ArgumentException)
                {
                    throw new NonUniqueObjectException(item.GetType().Name, key);
                }
            }

            return dico;
        }
    }
}
