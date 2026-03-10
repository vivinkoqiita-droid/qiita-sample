namespace LinqSample
{
    /// <summary>
    /// Departments 表示行
    /// </summary>
    public sealed class DepartmentRow
    {
        /// <summary>
        /// 部門ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 部門名
        /// </summary>
        public string Name { get; set; } = "";
    }
}
