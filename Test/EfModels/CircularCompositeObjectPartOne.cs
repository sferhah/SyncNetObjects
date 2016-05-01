using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfModels
{
    public class CircularCompositeObjectPartOne
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public Boolean? IsNice { get; set; }
        public CircularCompositeObjectPartTwo CircularCompositeObjectPartTwo { get; set; }

        public override string ToString()
        {
            return nameof(CircularCompositeObjectPartOne) + " " + Name;
        }
    }
}
