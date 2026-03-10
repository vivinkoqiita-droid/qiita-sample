using System.Collections.Generic;
using System.Globalization;

namespace LinqSample
{
    /// <summary>
    /// 表示用の小物
    /// </summary>
    public static class FormatUtil
    {
        /// <summary>
        /// 1行表示（", " 区切り）
        /// </summary>
        public static string J<T>(IEnumerable<T> xs) => string.Join(", ", xs);

        /// <summary>
        /// 小数の表示（. 固定）
        /// </summary>
        public static string ToInvariantString(double x) => x.ToString(CultureInfo.InvariantCulture);
    }
}
