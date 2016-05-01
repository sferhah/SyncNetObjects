﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test.NetModels
{
    public class CompositeObjectPartOne
    {
        public String Name { get; set; }
        public Boolean? IsNice { get; set; }

        public CompositeObjectPartTwo CompositeObjectPartTwo { get; set; }

        public override string ToString()
        {
            return nameof(CompositeObjectPartOne) + " " + Name + " " + CompositeObjectPartTwo.ToString();
        }
    }
}
