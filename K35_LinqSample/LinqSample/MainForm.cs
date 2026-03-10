using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace LinqSample
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainForm : Form
    {
        // デモ一覧
        private readonly List<LinqDemo> _demos = new();

        // ツールチップ
        private readonly ToolTip _toolTip = new ToolTip();

        // 選択中デモ
        private LinqDemo? _selectedDemo;


        /// <summary>
        /// 画面の初期化
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            ApplyInitialLayout();

            // データ表の準備
            InitializeDataGrids();

            // デモ定義の読み込み
            _demos = LinqDemoCatalog.CreateAll();

            // ツールチップの調整
            _toolTip.AutoPopDelay = 15000;
            _toolTip.InitialDelay = 300;
            _toolTip.ReshowDelay = 100;
            _toolTip.ShowAlways = true;

            // 初期表示
            lblCommand.Text = "-";
            lblElapsed.Text = "-";
            rtbCode.Clear();

            // ボタンの並び作成
            BuildCommandTabs();
        }


        /// <summary>
        /// 初期レイアウトの微調整
        /// </summary>
        private void ApplyInitialLayout()
        {
            // GroupBox 内の余白（右・下の罫線欠け対策）
            gbCommands.Padding = new Padding(8, 22, 8, 8);
            gbCode.Padding = new Padding(8, 22, 8, 8);
            gbConsole.Padding = new Padding(8, 22, 8, 8);

            // 分割位置
            splitMain.SplitterDistance = 800;       // 左ペイン横幅
            splitRight.SplitterDistance = 350;      // 右ペイン1段目縦幅
            splitPreview.SplitterDistance = 430;    // 右ペイン2段目縦幅
        }

        /// <summary>
        /// データ表の初期化
        /// </summary>
        private void InitializeDataGrids()
        {

            // 体裁の固定
            ConfigureDepartmentsGrid();
            ConfigureEmployeesGrid();
            ConfigureOrdersGrid();

            // 表に載せるデータの作成
            var deptRows = DemoData.Departments
                .Select(d => new DepartmentRow { Id = d.Id, Name = d.Name })
                .ToList();

            var employeeRows = DemoData.Employees
                .Select(e => new EmployeeRow
                {
                    Id = e.Id,
                    Name = e.Name,
                    DeptId = e.DeptId,
                    Age = e.Age,
                    Salary = e.Salary,
                    Skills = string.Join(", ", e.Skills),
                    Joined = e.Joined,
                    IsActive = e.IsActive
                })
                .ToList();

            var orderRows = DemoData.Orders
                .Select(o => new OrderRow
                {
                    Id = o.Id,
                    EmployeeId = o.EmployeeId,
                    Amount = o.Amount,
                    OrderedAt = o.OrderedAt
                })
                .ToList();

            // 表への反映
            dgvDepartments.DataSource = deptRows;
            dgvEmployees.DataSource = employeeRows;
            dgvOrders.DataSource = orderRows;
        }


        /// <summary>
        /// 表の見出し設定
        /// </summary>
        private void SetGridHeaders()
        {
            // 列名・列幅・書式は ConfigureXXXGrid に集約
        }


        /// <summary>
        /// 表の共通設定
        /// </summary>
        private static void ConfigureGridCommon(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        /// <summary>
        /// 文字列列の作成
        /// </summary>
        private static DataGridViewTextBoxColumn CreateTextColumn(
            string name,
            string headerText,
            string dataPropertyName,
            DataGridViewAutoSizeColumnMode autoSizeMode,
            DataGridViewContentAlignment alignment,
            string? format = null,
            int width = 80,
            int minimumWidth = 0)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                AutoSizeMode = autoSizeMode,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };

            col.DefaultCellStyle.Alignment = alignment;

            if (!string.IsNullOrEmpty(format))
            {
                col.DefaultCellStyle.Format = format;
            }

            if (autoSizeMode == DataGridViewAutoSizeColumnMode.None)
            {
                col.Width = width;
            }
            else if (minimumWidth > 0)
            {
                col.MinimumWidth = minimumWidth;
            }

            return col;
        }

        /// <summary>
        /// チェック列の作成
        /// </summary>
        private static DataGridViewCheckBoxColumn CreateCheckColumn(
            string name,
            string headerText,
            string dataPropertyName,
            DataGridViewAutoSizeColumnMode autoSizeMode,
            int width = 60)
        {
            var col = new DataGridViewCheckBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                AutoSizeMode = autoSizeMode,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ThreeState = false
            };

            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (autoSizeMode == DataGridViewAutoSizeColumnMode.None)
            {
                col.Width = width;
            }

            return col;
        }

        /// <summary>
        /// 部門表の列定義
        /// </summary>
        private void ConfigureDepartmentsGrid()
        {
            ConfigureGridCommon(dgvDepartments);

            dgvDepartments.Columns.Add(CreateTextColumn(
                name: nameof(DepartmentRow.Id),
                headerText: "部門ID",
                dataPropertyName: nameof(DepartmentRow.Id),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleRight));

            // 値が短い列。余白は列ではなく表の右側に逃がす
            dgvDepartments.Columns.Add(CreateTextColumn(
                name: nameof(DepartmentRow.Name),
                headerText: "部門名",
                dataPropertyName: nameof(DepartmentRow.Name),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleLeft,
                minimumWidth: 90));
        }

        /// <summary>
        /// 社員表の列定義
        /// </summary>
        private void ConfigureEmployeesGrid()
        {
            ConfigureGridCommon(dgvEmployees);

            dgvEmployees.Columns.Add(CreateTextColumn(
                name: nameof(EmployeeRow.Id),
                headerText: "社員ID",
                dataPropertyName: nameof(EmployeeRow.Id),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleRight));

            dgvEmployees.Columns.Add(CreateTextColumn(
                name: nameof(EmployeeRow.Name),
                headerText: "氏名",
                dataPropertyName: nameof(EmployeeRow.Name),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleLeft));

            dgvEmployees.Columns.Add(CreateTextColumn(
                name: nameof(EmployeeRow.DeptId),
                headerText: "部門ID",
                dataPropertyName: nameof(EmployeeRow.DeptId),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleRight));

            dgvEmployees.Columns.Add(CreateTextColumn(
                name: nameof(EmployeeRow.Age),
                headerText: "年齢",
                dataPropertyName: nameof(EmployeeRow.Age),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleRight));

            dgvEmployees.Columns.Add(CreateTextColumn(
                name: nameof(EmployeeRow.Salary),
                headerText: "給与",
                dataPropertyName: nameof(EmployeeRow.Salary),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleRight));

            dgvEmployees.Columns.Add(CreateTextColumn(
                name: nameof(EmployeeRow.Skills),
                headerText: "スキル",
                dataPropertyName: nameof(EmployeeRow.Skills),
                // 文字量が一番ブレる列です。全列を "AllCells" にすると、
                // ここが広がりすぎて右端（入社日/在籍中）が見切れやすくなります。
                // そのため、この列だけ "Fill" にして余った幅を受け持たせます。
                autoSizeMode: DataGridViewAutoSizeColumnMode.Fill,
                alignment: DataGridViewContentAlignment.MiddleLeft,
                minimumWidth: 120));

            dgvEmployees.Columns.Add(CreateTextColumn(
                name: nameof(EmployeeRow.Joined),
                headerText: "入社日",
                dataPropertyName: nameof(EmployeeRow.Joined),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleCenter,
                format: "yyyy-MM-dd"));

            dgvEmployees.Columns.Add(CreateCheckColumn(
                name: nameof(EmployeeRow.IsActive),
                headerText: "在籍中",
                dataPropertyName: nameof(EmployeeRow.IsActive),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells));
        }

        /// <summary>
        /// 受注表の列定義
        /// </summary>
        private void ConfigureOrdersGrid()
        {
            ConfigureGridCommon(dgvOrders);

            dgvOrders.Columns.Add(CreateTextColumn(
                name: nameof(OrderRow.Id),
                headerText: "受注ID",
                dataPropertyName: nameof(OrderRow.Id),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleRight));

            dgvOrders.Columns.Add(CreateTextColumn(
                name: nameof(OrderRow.EmployeeId),
                headerText: "社員ID",
                dataPropertyName: nameof(OrderRow.EmployeeId),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleRight));

            dgvOrders.Columns.Add(CreateTextColumn(
                name: nameof(OrderRow.Amount),
                headerText: "金額",
                dataPropertyName: nameof(OrderRow.Amount),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleRight));

            dgvOrders.Columns.Add(CreateTextColumn(
                name: nameof(OrderRow.OrderedAt),
                headerText: "受注日",
                dataPropertyName: nameof(OrderRow.OrderedAt),
                autoSizeMode: DataGridViewAutoSizeColumnMode.AllCells,
                alignment: DataGridViewContentAlignment.MiddleCenter,
                format: "yyyy-MM-dd"));
        }



        private void BuildCommandTabs()
        {
            // 既存タブのクリア
            tabCommands.TabPages.Clear();

            // カテゴリごとにタブ作成
            foreach (var category in _demos.Select(x => x.Category).Distinct())
            {
                var page = new TabPage(category);

                // ボタンを縦に並べるためのパネル
                var panel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    AutoScroll = true,
                    Padding = new Padding(8)
                };

                // このカテゴリのデモ
                var items = _demos
                    .Where(x => x.Category == category)
                    .OrderBy(x => x.DisplayOrder)
                    .ThenBy(x => x.Name)
                    .ToList();

                foreach (var demo in items)
                {
                    var btn = new Button
                    {
                        Text = demo.Name,
                        AutoSize = false,
                        Width = 120,
                        Height = 30,
                        Margin = new Padding(4)
                    };

                    // マウスオーバーでコード表示
                    btn.MouseEnter += (_, __) => PreviewCode(demo);
                    btn.MouseLeave += (_, __) => RestoreCode();

                    // ツールチップでも表示
                    _toolTip.SetToolTip(btn, demo.Code);


                    btn.Click += (_, __) => RunDemo(demo);

                    panel.Controls.Add(btn);
                }

                page.Controls.Add(panel);
                tabCommands.TabPages.Add(page);
            }
        }

        /// <summary>
        /// デモの実行
        /// </summary>
        private void RunDemo(LinqDemo demo)
        {
            // 選択の確定
            _selectedDemo = demo;
            ShowCode(demo.Code);

            // 画面のクリア
            // 1回の実行につき結果は1つ
            rtbConsole.Clear();
            lblElapsed.Text = "-";
            lblCommand.Text = demo.Name;

            // 画面への出力口
            var writer = new UiConsoleWriter(AppendConsoleLine);

            // 実行時間の計測
            var sw = Stopwatch.StartNew();
            try
            {
                demo.Run(writer);
            }
            catch (Exception ex)
            {
                // 例外の表示
                // 落ちた場所の見える化
                AppendConsoleLine("【例外】" + ex.GetType().Name);
                AppendConsoleLine(ex.Message);
            }
            finally
            {
                sw.Stop();
                lblElapsed.Text = $"{sw.Elapsed.TotalMilliseconds:0.###} ms";
            }
        }


        /// <summary>
        /// コード欄の更新
        /// </summary>
        private void ShowCode(string code)
        {
            rtbCode.Text = code ?? string.Empty;
            rtbCode.SelectionStart = 0;
            rtbCode.SelectionLength = 0;
            rtbCode.ScrollToCaret();
        }

        /// <summary>
        /// マウスオーバー中のコード表示
        /// </summary>
        private void PreviewCode(LinqDemo demo)
        {
            ShowCode(demo.Code);
        }

        /// <summary>
        /// マウスオーバー解除後の復帰
        /// </summary>
        private void RestoreCode()
        {
            if (_selectedDemo != null)
                ShowCode(_selectedDemo.Code);
            else
                ShowCode(string.Empty);
        }

        /// <summary>
        /// コンソール欄へ 1行追加
        /// </summary>
        private void AppendConsoleLine(string line)
        {
            if (rtbConsole.TextLength > 0)
                rtbConsole.AppendText(Environment.NewLine);

            rtbConsole.AppendText(line);
        }

        /// <summary>
        /// クリアボタンの処理
        /// </summary>
        private void btnClear_Click(object? sender, EventArgs e)
        {
            rtbConsole.Clear();
            lblElapsed.Text = "-";
            lblCommand.Text = "-";
        }
    }
}