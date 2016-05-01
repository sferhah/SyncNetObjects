using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;
using System.Data.Entity.ModelConfiguration;

namespace Ferhah.SyncNetObjects.Test.EfConfigs
{
    public class CircularChildEfConfig : EntityTypeConfiguration<CircularChild>
    {
        public CircularChildEfConfig()
        {
            HasKey(x => x.ID);

            Property(e => e.ID).HasColumnName("ID");
            Property(e => e.Name).HasColumnName("Name");
            Property(e => e.CircularParentID).HasColumnName("CircularParentID");
            Property(e => e.CircularParent2ID).HasColumnName("CircularParent2ID");

            HasRequired(e => e.CircularParent).WithMany().HasForeignKey(e => e.CircularParentID);
            HasRequired(e => e.CircularParent2).WithMany().HasForeignKey(e => e.CircularParent2ID);

            ToTable("CircularChild");
        }
    }

   
  

}
