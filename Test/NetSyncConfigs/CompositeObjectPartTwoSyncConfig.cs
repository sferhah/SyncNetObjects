using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.NetModels;


namespace Ferhah.SyncNetObjects.Test.NetSyncConfigs
{
    public class CompositeObjectPartTwoSyncConfig : SyncConfiguration<CompositeObjectPartTwo>
    {
        public CompositeObjectPartTwoSyncConfig()
        {
            PrimitiveProperty(x => x.Description);
            NavigationProperty(x => x.CompositeObjectPartOne);
            NavigationProperty(x => x.Company);
        }
    }
}
