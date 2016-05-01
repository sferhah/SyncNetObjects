using Ferhah.SyncNetObjects.Test.EfModels;
using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfSyncConfigs
{
    public class CompositeObjectPartOneSyncConfig : SyncConfiguration<CompositeObjectPartOne>
    {
        public CompositeObjectPartOneSyncConfig()
        {
            Key(x => x.Name);
            PrimitiveProperty(x => x.IsNice);

            ComplexType(x => x.CompositeObjectPartTwo, new CompositeObjectPartTwoSyncConfig());
        }
    }
}
