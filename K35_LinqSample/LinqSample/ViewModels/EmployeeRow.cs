using System;

namespace LinqSample
{
    /// <summary>
    /// Employees 表示行
    /// </summary>
    public sealed class EmployeeRow
    {
        /// <summary>
        /// 社員ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 氏名
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 部門ID
        /// </summary>
        public int DeptId { get; set; }

        /// <summary>
        /// 年齢
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 給与
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// スキル表示文字列
        /// </summary>
        public string Skills { get; set; } = "";

        /// <summary>
        /// 入社日
        /// </summary>
        public DateTime Joined { get; set; }

        /// <summary>
        /// 在籍中
        /// </summary>
        public bool IsActive { get; set; }
    }
}
