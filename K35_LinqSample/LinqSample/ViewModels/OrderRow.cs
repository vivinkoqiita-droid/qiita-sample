using System;

namespace LinqSample
{
    /// <summary>
    /// Orders 表示行
    /// </summary>
    public sealed class OrderRow
    {
        /// <summary>
        /// 受注ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 社員ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 受注日
        /// </summary>
        public DateTime OrderedAt { get; set; }
    }
}
