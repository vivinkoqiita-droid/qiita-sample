using System;

namespace LinqSample
{
    /// <summary>
    /// 受注情報
    /// </summary>
    public sealed class Order
    {
        /// <summary>
        /// 受注ID（Id）
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 社員ID（EmployeeId）
        /// </summary>
        public int EmployeeId { get; }

        /// <summary>
        /// 金額（Amount）
        /// </summary>
        public int Amount { get; }

        /// <summary>
        /// 受注日（OrderedAt）
        /// </summary>
        public DateTime OrderedAt { get; }

        /// <summary>
        /// 値の受け取り
        /// </summary>
        public Order(int id, int employeeId, int amount, DateTime orderedAt)
        {
            Id = id;
            EmployeeId = employeeId;
            Amount = amount;
            OrderedAt = orderedAt;
        }
    }
}
