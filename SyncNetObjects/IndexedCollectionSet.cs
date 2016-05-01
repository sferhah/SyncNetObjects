using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects
{
    public class IndexedCollectionSet<T>
    {
        public List<IndexedCollection> IndexedCollections { get; set; }
        public T ObjectSet { get; set; }

        public IndexedCollection this[SyncConfiguration key]
        {
            get
            {
                return IndexedCollections.Where(x => x.Configuration == key).First();
            }
        }
      
    }
}
