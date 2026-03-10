using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：集計。
        /// </summary>
        private static void AddAggregationDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "集計",
                10,
                "Count",
                """
                var activeCount = DemoData.Employees.Count(e => e.IsActive);
                Console.WriteLine(activeCount);
                """,
                w =>
                {
                    var activeCount = DemoData.Employees.Count(e => e.IsActive);
                    w.WriteLine(activeCount.ToString());
                });

            Add(
                demos,
                "集計",
                20,
                "Sum",
                """
                var sumSalary = DemoData.Employees.Sum(e => e.Salary);
                Console.WriteLine(sumSalary);
                """,
                w =>
                {
                    var sumSalary = DemoData.Employees.Sum(e => e.Salary);
                    w.WriteLine(sumSalary.ToString());
                });

            Add(
                demos,
                "集計",
                30,
                "Average",
                """
                var avgSalary = DemoData.Employees.Average(e => e.Salary);
                Console.WriteLine(avgSalary);
                """,
                w =>
                {
                    var avgSalary = DemoData.Employees.Average(e => e.Salary);
                    w.WriteLine(FormatUtil.ToInvariantString(avgSalary));
                });

            Add(
                demos,
                "集計",
                40,
                "Min",
                """
                var minSalary = DemoData.Employees.Min(e => e.Salary);
                Console.WriteLine(minSalary);
                """,
                w =>
                {
                    var minSalary = DemoData.Employees.Min(e => e.Salary);
                    w.WriteLine(minSalary.ToString());
                });

            Add(
                demos,
                "集計",
                50,
                "Max",
                """
                var maxSalary = DemoData.Employees.Max(e => e.Salary);
                Console.WriteLine(maxSalary);
                """,
                w =>
                {
                    var maxSalary = DemoData.Employees.Max(e => e.Salary);
                    w.WriteLine(maxSalary.ToString());
                });

            Add(
                demos,
                "集計",
                60,
                "Aggregate",
                """
                var chain = DemoData.Employees
                    .Select(e => e.Name)
                    .Aggregate((acc, x) => acc + " -> " + x);

                Console.WriteLine(chain);
                """,
                w =>
                {
                    var chain = DemoData.Employees
                        .Select(e => e.Name)
                        .Aggregate((acc, x) => acc + " -> " + x);

                    w.WriteLine(chain);
                });
        }
    }
}
