using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferhah.SyncNetObjects.Test
{
    public class SyncHelper
    {
        public static void AssertNoDiff(Object set_1, Object set_2, bool ignoreIds )
        {

            StringBuilder sb = new StringBuilder();

            CompareLogic basicComparison = new CompareLogic()
            {
                Config = new ComparisonConfig
                {
                    MaxDifferences = 9999999,
                }
            };

            List<Difference> diffs = basicComparison.Compare(set_1, set_2).Differences;

            foreach (Difference diff in diffs)
            {
                if(ignoreIds 
                    && diff.PropertyName.EndsWith("ID", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                sb.AppendLine("Property name:" + diff.PropertyName);
                sb.AppendLine("Old value:" + diff.Object1Value);
                sb.AppendLine("New value:" + diff.Object2Value + "\n");
            }

            var result = sb.ToString();

            Assert.IsTrue(String.IsNullOrWhiteSpace(result));

        }

    }
}
