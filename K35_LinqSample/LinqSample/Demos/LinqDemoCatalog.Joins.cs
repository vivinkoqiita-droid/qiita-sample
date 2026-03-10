using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：結合。
        /// </summary>
        private static void AddJoinDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "結合",
                10,
                "Join",
                """
                var joined = DemoData.Employees.Join(
                    DemoData.Departments,
                    e => e.DeptId,
                    d => d.Id,
                    (e, d) => $"{e.Name}-{d.Name}");

                Console.WriteLine(J(joined));
                """,
                w =>
                {
                    var joined = DemoData.Employees.Join(
                        DemoData.Departments,
                        e => e.DeptId,
                        d => d.Id,
                        (e, d) => $"{e.Name}-{d.Name}");

                    w.WriteLine(FormatUtil.J(joined));
                });

            Add(
                demos,
                "結合",
                20,
                "GroupJoin",
                """
                var grouped = DemoData.Departments.GroupJoin(
                    DemoData.Employees,
                    d => d.Id,
                    e => e.DeptId,
                    (d, es) => $"{d.Name}: {J(es.Select(x => x.Name))}");

                foreach (var line in grouped)
                {
                    Console.WriteLine(line);
                }
                """,
                w =>
                {
                    var grouped = DemoData.Departments.GroupJoin(
                        DemoData.Employees,
                        d => d.Id,
                        e => e.DeptId,
                        (d, es) => $"{d.Name}: {FormatUtil.J(es.Select(x => x.Name))}");

                    foreach (var line in grouped)
                        w.WriteLine(line);
                });
        }
    }
}
