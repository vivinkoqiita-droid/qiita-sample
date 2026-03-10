using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：並べ替え。
        /// </summary>
        private static void AddOrderingDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "並べ替え",
                10,
                "OrderBy",
                """
                var byDept = DemoData.Employees
                    .OrderBy(e => e.DeptId)
                    .Select(e => $"{e.DeptId}:{e.Name}");

                Console.WriteLine(J(byDept));
                """,
                w =>
                {
                    var byDept = DemoData.Employees
                        .OrderBy(e => e.DeptId)
                        .Select(e => $"{e.DeptId}:{e.Name}");

                    w.WriteLine(FormatUtil.J(byDept));
                });

            Add(
                demos,
                "並べ替え",
                20,
                "ThenBy",
                """
                var byDeptThenSalary = DemoData.Employees
                    .OrderBy(e => e.DeptId)
                    .ThenByDescending(e => e.Salary)
                    .Select(e => $"{e.DeptId}:{e.Name}({e.Salary})");

                Console.WriteLine(J(byDeptThenSalary));
                """,
                w =>
                {
                    var byDeptThenSalary = DemoData.Employees
                        .OrderBy(e => e.DeptId)
                        .ThenByDescending(e => e.Salary)
                        .Select(e => $"{e.DeptId}:{e.Name}({e.Salary})");

                    w.WriteLine(FormatUtil.J(byDeptThenSalary));
                });
        }
    }
}
