using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：要素取得。
        /// </summary>
        private static void AddElementOperatorDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "要素取得",
                10,
                "FirstOrDefault",
                """
                var firstQa = DemoData.Employees
                    .Where(e => e.DeptId == 3)
                    .OrderBy(e => e.Joined)
                    .Select(e => e.Name)
                    .FirstOrDefault();

                Console.WriteLine(firstQa ?? "(空)");
                """,
                w =>
                {
                    var firstQa = DemoData.Employees
                        .Where(e => e.DeptId == 3)
                        .OrderBy(e => e.Joined)
                        .Select(e => e.Name)
                        .FirstOrDefault();

                    w.WriteLine(firstQa ?? "(空)");
                });

            Add(
                demos,
                "要素取得",
                20,
                "SingleOrDefault",
                """
                var bob = DemoData.Employees.SingleOrDefault(e => e.Id == 2);
                var none = DemoData.Employees.SingleOrDefault(e => e.Id == 999);

                Console.WriteLine(bob?.Name ?? "(null)");
                Console.WriteLine(none?.Name ?? "(null)");
                """,
                w =>
                {
                    var bob = DemoData.Employees.SingleOrDefault(e => e.Id == 2);
                    var none = DemoData.Employees.SingleOrDefault(e => e.Id == 999);

                    w.WriteLine(bob?.Name ?? "(null)");
                    w.WriteLine(none?.Name ?? "(null)");
                });

            Add(
                demos,
                "要素取得",
                30,
                "ElementAt",
                """
                var secondHighest = DemoData.Employees
                    .OrderByDescending(e => e.Salary)
                    .Select(e => e.Name)
                    .ElementAt(1);

                Console.WriteLine(secondHighest);
                """,
                w =>
                {
                    var secondHighest = DemoData.Employees
                        .OrderByDescending(e => e.Salary)
                        .Select(e => e.Name)
                        .ElementAt(1);

                    w.WriteLine(secondHighest);
                });

            Add(
                demos,
                "要素取得",
                40,
                "DefaultIfEmpty",
                """
                var dept99Names = DemoData.Employees
                    .Where(e => e.DeptId == 99)
                    .Select(e => e.Name)
                    .DefaultIfEmpty("(なし)");

                Console.WriteLine(J(dept99Names));
                """,
                w =>
                {
                    var dept99Names = DemoData.Employees
                        .Where(e => e.DeptId == 99)
                        .Select(e => e.Name)
                        .DefaultIfEmpty("(なし)");

                    w.WriteLine(FormatUtil.J(dept99Names));
                });
        }
    }
}
