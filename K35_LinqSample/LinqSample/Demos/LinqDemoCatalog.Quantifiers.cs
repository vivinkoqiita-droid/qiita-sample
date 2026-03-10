using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：量の確認。
        /// </summary>
        private static void AddQuantifierDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "量の確認",
                10,
                "Any",
                """
                var hasInactive = DemoData.Employees.Any(e => !e.IsActive);
                Console.WriteLine(hasInactive);
                """,
                w =>
                {
                    var hasInactive = DemoData.Employees.Any(e => !e.IsActive);
                    w.WriteLine(hasInactive.ToString());
                });

            Add(
                demos,
                "量の確認",
                20,
                "All",
                """
                var allActive = DemoData.Employees.All(e => e.IsActive);
                Console.WriteLine(allActive);
                """,
                w =>
                {
                    var allActive = DemoData.Employees.All(e => e.IsActive);
                    w.WriteLine(allActive.ToString());
                });
        }
    }
}
