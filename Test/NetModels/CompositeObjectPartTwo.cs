using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.NetModels
{
    public class CompositeObjectPartTwo
    {
        public String Description { get; set; }

        public CompositeObjectPartOne CompositeObjectPartOne { get; set; }

        public Company Company { get; set; }

        public override string ToString()
        {
            return nameof(CompositeObjectPartTwo) + " " + Description;
        }
    }
}
