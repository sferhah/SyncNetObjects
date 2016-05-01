using Ferhah.SyncNetObjects.Test.EfModels;
using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfSyncConfigs
{
    public class CircularCompositeObjectPartTwoSyncConfig : SyncConfiguration<CircularCompositeObjectPartTwo>
    {
        public CircularCompositeObjectPartTwoSyncConfig()
        {
            PrimitiveProperty(x => x.Description);
            KeyNavigationProperty(x => x.CircularCompositeObjectPartOne);
            NavigationProperty(x => x.CircularChild);
        }
    }
}
