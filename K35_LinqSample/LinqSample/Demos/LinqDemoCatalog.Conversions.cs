using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：変換と生成。
        /// </summary>
        private static void AddConversionAndGenerationDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "変換と生成",
                10,
                "ToList",
                """
                var list = DemoData.Employees
                    .Where(e => e.IsActive)
                    .Select(e => e.Name)
                    .ToList();

                Console.WriteLine(list.GetType().Name);
                Console.WriteLine(J(list));
                """,
                w =>
                {
                    var list = DemoData.Employees
                        .Where(e => e.IsActive)
                        .Select(e => e.Name)
                        .ToList();

                    w.WriteLine(list.GetType().Name);
                    w.WriteLine(FormatUtil.J(list));
                });

            Add(
                demos,
                "変換と生成",
                20,
                "ToDictionary",
                """
                var dict = DemoData.Employees.ToDictionary(e => e.Id, e => e.Name);
                Console.WriteLine(dict[3]);
                """,
                w =>
                {
                    var dict = DemoData.Employees.ToDictionary(e => e.Id, e => e.Name);
                    w.WriteLine(dict[3]);
                });

            Add(
                demos,
                "変換と生成",
                30,
                "ToLookup",
                """
                var lookup = DemoData.Employees.ToLookup(e => e.DeptId, e => e.Name);
                Console.WriteLine(J(lookup[3]));
                """,
                w =>
                {
                    var lookup = DemoData.Employees.ToLookup(e => e.DeptId, e => e.Name);
                    w.WriteLine(FormatUtil.J(lookup[3]));
                });

            Add(
                demos,
                "変換と生成",
                40,
                "Zip",
                """
                var nameAndSalary = DemoData.Employees
                    .Select(e => e.Name)
                    .Zip(
                        DemoData.Employees.Select(e => e.Salary),
                        (name, salary) => $"{name}({salary})");

                Console.WriteLine(J(nameAndSalary));
                """,
                w =>
                {
                    var nameAndSalary = DemoData.Employees
                        .Select(e => e.Name)
                        .Zip(
                            DemoData.Employees.Select(e => e.Salary),
                            (name, salary) => $"{name}({salary})");

                    w.WriteLine(FormatUtil.J(nameAndSalary));
                });

            Add(
                demos,
                "変換と生成",
                50,
                "Chunk",
                """
                var chunks = DemoData.Employees
                    .Select(e => e.Name)
                    .Chunk(2)
                    .Select(block => $"[{J(block)}]");

                Console.WriteLine(J(chunks));
                """,
                w =>
                {
                    var chunks = DemoData.Employees
                        .Select(e => e.Name)
                        .Chunk(2)
                        .Select(block => $"[{FormatUtil.J(block)}]");

                    w.WriteLine(FormatUtil.J(chunks));
                });

            Add(
                demos,
                "変換と生成",
                60,
                "Append",
                """
                var xs = DemoData.Employees.Select(e => e.Name).Append("Zoe");
                Console.WriteLine(J(xs));
                """,
                w =>
                {
                    var xs = DemoData.Employees.Select(e => e.Name).Append("Zoe");
                    w.WriteLine(FormatUtil.J(xs));
                });

            Add(
                demos,
                "変換と生成",
                70,
                "Prepend",
                """
                var xs = DemoData.Employees.Select(e => e.Name).Prepend("Start");
                Console.WriteLine(J(xs));
                """,
                w =>
                {
                    var xs = DemoData.Employees.Select(e => e.Name).Prepend("Start");
                    w.WriteLine(FormatUtil.J(xs));
                });

            Add(
                demos,
                "変換と生成",
                80,
                "Concat",
                """
                var xs = new[] { "A", "B" }.Concat(new[] { "B", "C" });
                Console.WriteLine(J(xs));
                """,
                w =>
                {
                    var xs = new[] { "A", "B" }.Concat(new[] { "B", "C" });
                    w.WriteLine(FormatUtil.J(xs));
                });
        }
    }
}
