using Ferhah.SyncNetObjects.Test.EfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfConfigs
{
    public class CircularCompositeObjectPartTwoEfConfig : EntityTypeConfiguration<CircularCompositeObjectPartTwo>
    {
        public CircularCompositeObjectPartTwoEfConfig()
        {
            HasKey(e => e.CircularCompositeObjectPartOneID);
            Property(t => t.CircularCompositeObjectPartOneID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Description).HasColumnName("Description");
            Property(e => e.CircularCompositeObjectPartOneID).HasColumnName("CircularCompositeObjectPartOneID");

            Property(e => e.CircularChildID).HasColumnName("CircularChildID");
            HasRequired(e => e.CircularChild).WithMany().HasForeignKey(e => e.CircularChildID);

            ToTable("CircularCompositeObjectPartTwo");
        }
    }
}
