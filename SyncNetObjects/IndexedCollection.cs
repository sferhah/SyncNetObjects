using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects
{
    public class IndexedCollection
    {

        

        public SyncConfiguration Configuration { get; private set; }        
        public IList Collection { get; private set; }

        protected Dictionary<String, Object> dictionary;

        public IndexedCollection(SyncConfiguration configuration, Dictionary<String, Object> dictionary, IList collection)
        {
            this.Configuration = configuration;
            this.dictionary = dictionary;
            this.Collection = collection;            
        }

        public IEnumerator<KeyValuePair<String, object>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public Object this[String key]
        {
            get
            {
                Object value;

                dictionary.TryGetValue(key, out value);

                return value;
            }
        }

        public void Add(String k, Object v)
        {
            dictionary.Add(k, v);
            Collection.Add(v);
        }

        public void Remove(String k, Object v)
        {
            dictionary.Remove(k);
            Collection.Remove(v);
        }

    }
}
