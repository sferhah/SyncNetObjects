using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ferhah.SyncNetObjects.Test.EfModels;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ferhah.SyncNetObjects.Test.EfConfigs
{
    public class CircularCompositeObjectPartOneEfConfig : EntityTypeConfiguration<CircularCompositeObjectPartOne>
    {
        public CircularCompositeObjectPartOneEfConfig()
        {
            HasKey(x => x.ID);

            Property(e => e.ID).HasColumnName("ID");            
            Property(e => e.Name).HasColumnName("Name");
            Property(e => e.IsNice).HasColumnName("IsNice");

            HasOptional(quote => quote.CircularCompositeObjectPartTwo)
            .WithRequired(order => order.CircularCompositeObjectPartOne);

            ToTable("CircularCompositeObjectPartOne");
        }
    }

  
}
