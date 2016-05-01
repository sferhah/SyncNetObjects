using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.NetModels;


namespace Ferhah.SyncNetObjects.Test.NetSyncConfigs
{
    public class ObjectWithCompositeKeySyncConfig : SyncConfiguration<ObjectWithCompositeKey>
    {
        public ObjectWithCompositeKeySyncConfig()
        {
            Key(x => x.Key_1);
            Key(x => x.Key_2);
            KeyNavigationProperty(x => x.Key_3);
        }
    }

    public class CompanySyncConfig : SyncConfiguration<Company>
    {
        public CompanySyncConfig()
        {
            Key(x => x.Name);
            PrimitiveProperty(x => x.Address);
        }
    }

  
}
