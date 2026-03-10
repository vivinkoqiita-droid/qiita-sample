using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSample
{
    /// <summary>
    /// デモ定義のカタログです。
    /// </summary>
    public static partial class LinqDemoCatalog
    {
        /// <summary>
        /// 全デモを生成します。
        /// </summary>
        public static List<LinqDemo> CreateAll()
        {
            var demos = new List<LinqDemo>();

            // 表示順は、ここで呼んだ順になります。
            AddDeferredExecutionDemos(demos);
            AddFilteringDemos(demos);
            AddProjectionDemos(demos);
            AddOrderingDemos(demos);
            AddSetOperationDemos(demos);
            AddQuantifierDemos(demos);
            AddAggregationDemos(demos);
            AddElementOperatorDemos(demos);
            AddJoinDemos(demos);
            AddConversionAndGenerationDemos(demos);

            return demos;
        }

        /// <summary>
        /// デモを 1 件追加します。
        /// </summary>
        private static void Add(
            List<LinqDemo> demos,
            string category,
            int displayOrder,
            string name,
            string code,
            Action<IConsoleWriter> run)
        {
            demos.Add(new LinqDemo(category, name, code, run, displayOrder));
        }
    }
}
