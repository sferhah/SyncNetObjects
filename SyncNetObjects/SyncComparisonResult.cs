using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects
{
    public class SyncComparisonResult<T>
    {
        public IndexedCollectionSet<T> DeltaAddedIndexedCollectionSet { get; set; }
        public IndexedCollectionSet<T> DeltaUpdateIndexedCollectionSet { get; set; }
        public IndexedCollectionSet<T> DeltaRemoveIndexedCollectionSet { get; set; }
    }
}
