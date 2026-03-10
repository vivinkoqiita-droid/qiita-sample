using System;
using System.Collections.Generic;

namespace LinqSample
{
    /// <summary>
    /// デモ用の固定データ
    /// </summary>
    public static class DemoData
    {
        /// <summary>
        /// 部門一覧（Departments）
        /// </summary>
        public static readonly List<Department> Departments = new()
        {
            new Department(1, "Platform"),
            new Department(2, "Sales"),
            new Department(3, "QA"),
        };

        /// <summary>
        /// 社員一覧（Employees）
        /// </summary>
        public static readonly List<Employee> Employees = new()
        {
            new Employee(1, "Alice", 1, 34, 800, new[] { "C#", "LINQ", "SQL" },        new DateTime(2018, 4, 1),  true),
            new Employee(2, "Bob",   1, 28, 650, new[] { "C#", "WPF" },               new DateTime(2021,10,15),  true),
            new Employee(3, "Carol", 2, 41, 900, new[] { "Excel", "Negotiation" },    new DateTime(2015, 1,20),  true),
            new Employee(4, "Dave",  3, 25, 550, new[] { "C#", "xUnit", "Selenium" }, new DateTime(2023, 7, 1),  true),
            new Employee(5, "Erin",  3, 37, 750, new[] { "Python", "Playwright" },    new DateTime(2019,11, 5),  false),
            new Employee(6, "Frank", 2, 30, 620, new[] { "SQL", "PowerBI" },          new DateTime(2022, 2,10),  true),
        };

        /// <summary>
        /// 受注一覧（Orders）
        /// </summary>
        public static readonly List<Order> Orders = new()
        {
            new Order(1, 1, 12000, new DateTime(2025, 1,10)),
            new Order(2, 1,  8000, new DateTime(2025, 2, 5)),
            new Order(3, 3, 30000, new DateTime(2025, 1,12)),
            new Order(4, 6, 15000, new DateTime(2025, 3, 3)),
            new Order(5, 2,  5000, new DateTime(2025, 2,20)),
            new Order(6, 4,  7000, new DateTime(2025, 3,15)),
        };
    }
}
