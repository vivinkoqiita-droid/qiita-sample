using System;

namespace LinqSample
{
    /// <summary>
    /// 社員情報
    /// </summary>
    public sealed class Employee
    {
        /// <summary>
        /// 社員ID（Id）
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 氏名（Name）
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 部門ID（DeptId）
        /// </summary>
        public int DeptId { get; }

        /// <summary>
        /// 年齢（Age）
        /// </summary>
        public int Age { get; }

        /// <summary>
        /// 給与（Salary）
        /// </summary>
        public int Salary { get; }

        /// <summary>
        /// スキル配列（Skills）
        /// </summary>
        public string[] Skills { get; }

        /// <summary>
        /// 入社日（Joined）
        /// </summary>
        public DateTime Joined { get; }

        /// <summary>
        /// 在籍中フラグ（IsActive）
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        /// 値の受け取り
        /// </summary>
        public Employee(int id, string name, int deptId, int age, int salary, string[] skills, DateTime joined, bool isActive)
        {
            Id = id;
            Name = name;
            DeptId = deptId;
            Age = age;
            Salary = salary;
            Skills = skills;
            Joined = joined;
            IsActive = isActive;
        }
    }
}
