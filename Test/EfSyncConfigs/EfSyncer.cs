using Ferhah.SyncNetObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfSyncConfigs
{
    public class EfSyncer<T> : Syncer<T> where T : class, new()
    {
        DbContext context;        

        public EfSyncer(DbContext context, T originalSet, T newestSet, SyncConfigSet<T> syncConfigSet)
           : base(originalSet, newestSet, syncConfigSet)
        {
            this.context = context;
        }

        public override void SyncSet()
        {
            base.SyncSet();

            context.SaveChanges();
        }

        #region remove
        public override void Remove(IndexedCollectionSet<T> delta)
        {
            context.Configuration.AutoDetectChangesEnabled = false;

            foreach (SyncConfiguration config in this.SyncConfigSet.Configurations)
            {
                DbSet originalList = context.Set(config.GetGenericType());                
                IList deltaList = delta[config].Collection;

                foreach (var item in deltaList)
                {
                    originalList.Remove(item);
                }
                
            }

            context.Configuration.AutoDetectChangesEnabled = true;

            base.Remove(delta);
        }



        #endregion

        #region add
        public override void Add(IndexedCollectionSet<T> delta)
        {
            context.Configuration.AutoDetectChangesEnabled = false;

            foreach (SyncConfiguration config in this.SyncConfigSet.Configurations)
            {
                DbSet originalList = context.Set(config.GetGenericType());
                IList deltaList = delta[config].Collection;

                foreach (var item in deltaList)
                {
                    originalList.Add(item);
                }
               
            }

            context.Configuration.AutoDetectChangesEnabled = true;

            base.Add(delta);
        }
        #endregion
    }
}
