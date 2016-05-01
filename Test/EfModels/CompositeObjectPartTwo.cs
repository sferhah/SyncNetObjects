using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.EfModels
{
    public class CompositeObjectPartTwo
    {
       
        public String Description { get; set; }

        public int CompositeObjectPartOneID { get; set; }
        public CompositeObjectPartOne CompositeObjectPartOne { get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }


        public override string ToString()
        {
            return nameof(CompositeObjectPartTwo) + " " + Description;
        }
    }
}
