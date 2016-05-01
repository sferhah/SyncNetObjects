using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfModels
{
    public class CircularChild
    {
        public int ID { get; set; }
        public String Name { get; set; }

        public int CircularParentID { get; set; }
        public CircularCompositeObjectPartOne CircularParent { get; set; }

        public int CircularParent2ID { get; set; }
        public CircularCompositeObjectPartOne CircularParent2 { get; set; }
    }
}
