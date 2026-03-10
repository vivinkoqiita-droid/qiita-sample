using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// デモの追加：遅延実行。
        /// </summary>
        private static void AddDeferredExecutionDemos(List<LinqDemo> demos)
        {
            Add(
                demos,
                "遅延実行",
                10,
                "遅延① 列挙で動く",
                """
                var q = DemoData.Employees
                    .Where(e => e.IsActive)
                    .Select(e =>
                    {
                        Console.WriteLine("hit: " + e.Name);
                        return e.Name;
                    });

                Console.WriteLine("created");

                foreach (var _ in q) { }

                Console.WriteLine("done");
                """,
                w =>
                {
                    var q = DemoData.Employees
                        .Where(e => e.IsActive)
                        .Select(e =>
                        {
                            w.WriteLine("hit: " + e.Name);
                            return e.Name;
                        });

                    w.WriteLine("created");

                    foreach (var _ in q) { }

                    w.WriteLine("done");
                });

            Add(
                demos,
                "遅延実行",
                20,
                "遅延② 2回列挙",
                """
                var q = DemoData.Employees
                    .Where(e => e.IsActive)
                    .Select(e =>
                    {
                        Console.WriteLine("hit: " + e.Name);
                        return e.Name;
                    });

                Console.WriteLine("-- first");
                foreach (var _ in q) { }

                Console.WriteLine("-- second");
                foreach (var _ in q) { }
                """,
                w =>
                {
                    var q = DemoData.Employees
                        .Where(e => e.IsActive)
                        .Select(e =>
                        {
                            w.WriteLine("hit: " + e.Name);
                            return e.Name;
                        });

                    w.WriteLine("-- first");
                    foreach (var _ in q) { }

                    w.WriteLine("-- second");
                    foreach (var _ in q) { }
                });

            Add(
                demos,
                "遅延実行",
                30,
                "遅延③ ToListで止める",
                """
                var q = DemoData.Employees
                    .Where(e => e.IsActive)
                    .Select(e =>
                    {
                        Console.WriteLine("hit: " + e.Name);
                        return e.Name;
                    });

                Console.WriteLine("-- materialize");
                var list = q.ToList();

                Console.WriteLine("-- enumerate 1");
                foreach (var _ in list) { }

                Console.WriteLine("-- enumerate 2");
                foreach (var _ in list) { }
                """,
                w =>
                {
                    var q = DemoData.Employees
                        .Where(e => e.IsActive)
                        .Select(e =>
                        {
                            w.WriteLine("hit: " + e.Name);
                            return e.Name;
                        });

                    w.WriteLine("-- materialize");
                    var list = q.ToList();

                    w.WriteLine("-- enumerate 1");
                    foreach (var _ in list) { }

                    w.WriteLine("-- enumerate 2");
                    foreach (var _ in list) { }
                });
        }
    }
}
