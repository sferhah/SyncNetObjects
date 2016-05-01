using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;

namespace Ferhah.SyncNetObjects.Test.EfSyncConfigs
{
    public class ContactSyncConfig : SyncConfiguration<Contact>
    {
        public ContactSyncConfig()
        {
            Key(x => x.FirstName);
            Key(x => x.Name);

            PrimitiveProperty(x => x.Address);
            NavigationProperty(x => x.Company);

            ComplexType(x => x.Condition, new ConditionSyncConfig());
        }
    }
}
