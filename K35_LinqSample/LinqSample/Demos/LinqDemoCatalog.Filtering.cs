using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：絞り込み。
        /// </summary>
        private static void AddFilteringDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "絞り込み",
                10,
                "Where",
                """
                var activeNames = DemoData.Employees
                    .Where(e => e.IsActive)
                    .Select(e => e.Name);

                Console.WriteLine(J(activeNames));
                """,
                w =>
                {
                    var activeNames = DemoData.Employees
                        .Where(e => e.IsActive)
                        .Select(e => e.Name);

                    w.WriteLine(FormatUtil.J(activeNames));
                });

            Add(
                demos,
                "絞り込み",
                20,
                "Take",
                """
                var earliest3 = DemoData.Employees
                    .OrderBy(e => e.Joined)
                    .Take(3)
                    .Select(e => e.Name);

                Console.WriteLine(J(earliest3));
                """,
                w =>
                {
                    var earliest3 = DemoData.Employees
                        .OrderBy(e => e.Joined)
                        .Take(3)
                        .Select(e => e.Name);

                    w.WriteLine(FormatUtil.J(earliest3));
                });

            Add(
                demos,
                "絞り込み",
                30,
                "Skip",
                """
                var rest = DemoData.Employees
                    .OrderBy(e => e.Joined)
                    .Skip(3)
                    .Select(e => e.Name);

                Console.WriteLine(J(rest));
                """,
                w =>
                {
                    var rest = DemoData.Employees
                        .OrderBy(e => e.Joined)
                        .Skip(3)
                        .Select(e => e.Name);

                    w.WriteLine(FormatUtil.J(rest));
                });

            Add(
                demos,
                "絞り込み",
                40,
                "TakeWhile",
                """
                var under35 = DemoData.Employees
                    .OrderBy(e => e.Age)
                    .TakeWhile(e => e.Age < 35)
                    .Select(e => $"{e.Name}({e.Age})");

                Console.WriteLine(J(under35));
                """,
                w =>
                {
                    var under35 = DemoData.Employees
                        .OrderBy(e => e.Age)
                        .TakeWhile(e => e.Age < 35)
                        .Select(e => $"{e.Name}({e.Age})");

                    w.WriteLine(FormatUtil.J(under35));
                });

            Add(
                demos,
                "絞り込み",
                50,
                "SkipWhile",
                """
                var from35 = DemoData.Employees
                    .OrderBy(e => e.Age)
                    .SkipWhile(e => e.Age < 35)
                    .Select(e => $"{e.Name}({e.Age})");

                Console.WriteLine(J(from35));
                """,
                w =>
                {
                    var from35 = DemoData.Employees
                        .OrderBy(e => e.Age)
                        .SkipWhile(e => e.Age < 35)
                        .Select(e => $"{e.Name}({e.Age})");

                    w.WriteLine(FormatUtil.J(from35));
                });
        }
    }
}
