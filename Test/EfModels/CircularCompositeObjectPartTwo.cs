using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfModels
{
    public class CircularCompositeObjectPartTwo
    {
       
        public String Description { get; set; }

        public int CircularCompositeObjectPartOneID { get; set; }
        public CircularCompositeObjectPartOne CircularCompositeObjectPartOne { get; set; }

        public int CircularChildID { get; set; }
        public CircularChild CircularChild { get; set; }


        public override string ToString()
        {
            return nameof(CircularCompositeObjectPartTwo) + " " + Description;
        }
    }
}
