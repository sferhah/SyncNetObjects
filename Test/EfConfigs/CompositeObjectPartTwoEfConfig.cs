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
    public class CompositeObjectPartTwoEfConfig : EntityTypeConfiguration<CompositeObjectPartTwo>
    {
        public CompositeObjectPartTwoEfConfig()
        {
            HasKey(e => e.CompositeObjectPartOneID);
            Property(t => t.CompositeObjectPartOneID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Description).HasColumnName("Description");
            Property(e => e.CompositeObjectPartOneID).HasColumnName("CompositeObjectPartOneID");

            Property(e => e.CompanyID).HasColumnName("CompanyID");
            HasRequired(e => e.Company).WithMany().HasForeignKey(e => e.CompanyID);

            ToTable("CompositeObjectPartTwo");
        }
    }
}
