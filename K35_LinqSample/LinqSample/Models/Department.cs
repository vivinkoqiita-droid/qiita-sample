using System;

namespace LinqSample
{
    /// <summary>
    /// 部門情報
    /// </summary>
    public sealed class Department
    {
        /// <summary>
        /// 部門ID（Id）
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 部門名（Name）
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 値の受け取り
        /// </summary>
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
