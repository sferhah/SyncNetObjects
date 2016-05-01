using Ferhah.SyncNetObjects.Test.EfModels;
using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfSyncConfigs
{
    public class CompositeObjectPartTwoSyncConfig : SyncConfiguration<CompositeObjectPartTwo>
    {
        public CompositeObjectPartTwoSyncConfig()
        {
            PrimitiveProperty(x => x.Description);
            KeyNavigationProperty(x => x.CompositeObjectPartOne);
            NavigationProperty(x => x.Company);
        }
    }
}
