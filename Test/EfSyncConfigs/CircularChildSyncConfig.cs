using Ferhah.SyncNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;

namespace Ferhah.SyncNetObjects.Test.EfSyncConfigs
{
    public class CircularChildSyncConfig : SyncConfiguration<CircularChild>
    {
        public CircularChildSyncConfig()
        {
            Key(x => x.Name);            
            NavigationProperty(x => x.CircularParent);
            NavigationProperty(x => x.CircularParent2);
        }
    }

   
  

}
