using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：集合演算。
        /// </summary>
        private static void AddSetOperationDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "集合演算",
                10,
                "Distinct",
                """
                var deptIds = DemoData.Employees.Select(e => e.DeptId).Distinct();
                Console.WriteLine(J(deptIds));
                """,
                w =>
                {
                    var deptIds = DemoData.Employees.Select(e => e.DeptId).Distinct();
                    w.WriteLine(FormatUtil.J(deptIds));
                });

            Add(
                demos,
                "集合演算",
                20,
                "Union",
                """
                var alice = DemoData.Employees.First(e => e.Name == "Alice").Skills;
                var dave  = DemoData.Employees.First(e => e.Name == "Dave").Skills;

                var union = alice.Union(dave).OrderBy(s => s);
                Console.WriteLine(J(union));
                """,
                w =>
                {
                    var alice = DemoData.Employees.First(e => e.Name == "Alice").Skills;
                    var dave = DemoData.Employees.First(e => e.Name == "Dave").Skills;

                    var union = alice.Union(dave).OrderBy(s => s, StringComparer.Ordinal);
                    w.WriteLine(FormatUtil.J(union));
                });

            Add(
                demos,
                "集合演算",
                30,
                "Intersect",
                """
                var alice = DemoData.Employees.First(e => e.Name == "Alice").Skills;
                var dave  = DemoData.Employees.First(e => e.Name == "Dave").Skills;

                var common = alice.Intersect(dave);
                Console.WriteLine(J(common));
                """,
                w =>
                {
                    var alice = DemoData.Employees.First(e => e.Name == "Alice").Skills;
                    var dave = DemoData.Employees.First(e => e.Name == "Dave").Skills;

                    var common = alice.Intersect(dave);
                    w.WriteLine(FormatUtil.J(common));
                });

            Add(
                demos,
                "集合演算",
                40,
                "Except",
                """
                var alice = DemoData.Employees.First(e => e.Name == "Alice").Skills;
                var dave  = DemoData.Employees.First(e => e.Name == "Dave").Skills;

                var onlyAlice = alice.Except(dave);
                Console.WriteLine(J(onlyAlice));
                """,
                w =>
                {
                    var alice = DemoData.Employees.First(e => e.Name == "Alice").Skills;
                    var dave = DemoData.Employees.First(e => e.Name == "Dave").Skills;

                    var onlyAlice = alice.Except(dave);
                    w.WriteLine(FormatUtil.J(onlyAlice));
                });
        }
    }
}
