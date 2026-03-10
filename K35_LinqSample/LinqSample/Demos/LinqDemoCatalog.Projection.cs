using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：変換。
        /// </summary>
        private static void AddProjectionDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "変換",
                10,
                "Select",
                """
                var names = DemoData.Employees.Select(e => e.Name);
                Console.WriteLine(J(names));
                """,
                w =>
                {
                    var names = DemoData.Employees.Select(e => e.Name);
                    w.WriteLine(FormatUtil.J(names));
                });

            Add(
                demos,
                "変換",
                20,
                "SelectMany",
                """
                var allSkills = DemoData.Employees
                    .SelectMany(e => e.Skills)
                    .Distinct()
                    .OrderBy(s => s);

                foreach (var s in allSkills)
                {
                    Console.WriteLine(s);
                }
                """,
                w =>
                {
                    var allSkills = DemoData.Employees
                        .SelectMany(e => e.Skills)
                        .Distinct()
                        .OrderBy(s => s, StringComparer.Ordinal);

                    foreach (var s in allSkills)
                        w.WriteLine(s);
                });
        }
    }
}
