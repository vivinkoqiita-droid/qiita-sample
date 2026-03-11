using System.Diagnostics;
using System.Reflection;

namespace ListDictionaryHashSetBench
{
    /// <summary>
    /// ベンチマーク画面本体
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// シナリオ定義一覧
        /// </summary>
        private readonly List<ScenarioDefinition> _scenarios = new();

        /// <summary>
        /// 実行状態一覧
        /// </summary>
        private readonly List<ScenarioExecutionState> _states = new();

        /// <summary>
        /// 実行中アニメーション表示
        /// </summary>
        private readonly string[] _spinnerFrames = new[] { "●○○", "○●○", "○○●" };

        /// <summary>
        /// アニメーション位置
        /// </summary>
        private int _spinnerIndex;

        /// <summary>
        /// 実行中フラグ
        /// </summary>
        private bool _isRunning;

        /// <summary>
        /// 実行中行番号
        /// </summary>
        private int _runningScenarioIndex = -1;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // 描画ちらつき抑止
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            InitializeScenarios();
            BindRows();
            UpdateOverview();
        }

        /// <summary>
        /// 一覧初期化
        /// </summary>
        private void InitializeGrid()
        {
            dgvResults.Columns.Clear();
            dgvResults.Columns.Add(CreateTextColumn("No", "No", 45));
            dgvResults.Columns.Add(CreateTextColumn("処理名", "処理名", 220));
            dgvResults.Columns.Add(CreateTextColumn("比較内容", "どういう処理か", 360));
            dgvResults.Columns.Add(CreateTextColumn("開始時刻", "開始時刻", 110));
            dgvResults.Columns.Add(CreateTextColumn("終了時刻", "終了時刻", 110));
            dgvResults.Columns.Add(CreateTextColumn("List", "List(ms)", 95));
            dgvResults.Columns.Add(CreateTextColumn("Dictionary", "Dictionary(ms)", 120));
            dgvResults.Columns.Add(CreateTextColumn("HashSet", "HashSet(ms)", 105));
            dgvResults.Columns.Add(CreateTextColumn("最速", "最速", 95));
            dgvResults.Columns.Add(CreateTextColumn("状態", "状態", 75));
            dgvResults.Columns.Add(CreateTextColumn("進行", "進行", 70));

            EnableDoubleBuffer(dgvResults);
            dgvResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvResults.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            AdjustRightSideColumnWidths();
        }

        /// <summary>
        /// 右側列幅自動調整
        /// </summary>
        private void AdjustRightSideColumnWidths()
        {
            var rightColumnNames = new[] { "開始時刻", "終了時刻", "List", "Dictionary", "HashSet", "最速", "状態", "進行" };

            foreach (var columnName in rightColumnNames)
            {
                if (!dgvResults.Columns.Contains(columnName))
                {
                    continue;
                }

                var column = dgvResults.Columns[columnName];
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                var measuredWidth = column.Width;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = measuredWidth + 12;
            }
        }

        /// <summary>
        /// DataGridViewダブルバッファ有効化
        /// </summary>
        /// <param name="grid">対象グリッド</param>
        private static void EnableDoubleBuffer(DataGridView grid)
        {
            typeof(DataGridView)
                .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(grid, true, null);
        }

        /// <summary>
        /// 文字列列生成
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="headerText">見出し</param>
        /// <param name="width">列幅</param>
        /// <returns>列定義</returns>
        private static DataGridViewTextBoxColumn CreateTextColumn(string name, string headerText, int width)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                Width = width,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true,
            };
        }

        /// <summary>
        /// シナリオ定義初期化
        /// </summary>
        private void InitializeScenarios()
        {
            _scenarios.Clear();
            _states.Clear();

            _scenarios.Add(new ScenarioDefinition(
                "末尾の会員番号を何度も探す",
                "同じ会員番号があるかを繰り返し確認する。ログイン可否や重複チェックの連打に近い",
                BenchmarkContains,
                "『この番号はあるか』を何回も確認する処理"));
            _scenarios.Add(new ScenarioDefinition(
                "注文番号から明細を引く",
                "ランダムな注文番号を受け取り、その番号の明細を取り出す。詳細画面やAPI参照に近い",
                BenchmarkRandomLookup,
                "『番号を受けて1件取り出す』処理"));
            _scenarios.Add(new ScenarioDefinition(
                "重複行を除いて取り込む",
                "同じIDが混じるデータを順番に読み込み、まだ無い行だけ残す。CSV取込の前処理に近い",
                BenchmarkDuplicateAdds,
                "『同じIDは1件だけ残す』処理"));
            _scenarios.Add(new ScenarioDefinition(
                "退会者IDを除外して残す",
                "除外対象のID一覧を見ながら、残す側だけを新しい一覧へ積む。停止ユーザー除外の前処理に近い",
                BenchmarkRandomRemove,
                "『除外一覧を見ながら残す側を作る』処理"));
            _scenarios.Add(new ScenarioDefinition(
                "2つの名簿で共通IDを探す",
                "左の名簿を1件ずつ見ながら、右の名簿にも同じIDがあるか調べる。突合処理に近い",
                BenchmarkIntersection,
                "『両方にあるIDだけ拾う』処理"));
            _scenarios.Add(new ScenarioDefinition(
                "更新対象IDを探して書き換える",
                "更新対象IDを受け取り、該当データを見つけて点数を書き換える。一括更新の前段に近い",
                BenchmarkUpdateLookup,
                "『探してから値を書き換える』処理"));
            _scenarios.Add(new ScenarioDefinition(
                "明細ごとに会員か判定する",
                "明細を1件ずつ見ながら、そのIDが会員一覧にあるか確認する。集計前の前処理に近い",
                BenchmarkMembershipFilter,
                "『明細ごとに会員判定する』処理"));

            for (var i = 0; i < _scenarios.Count; i++)
            {
                _states.Add(new ScenarioExecutionState(i + 1, _scenarios[i]));
            }
        }

        /// <summary>
        /// 一覧反映
        /// </summary>
        private void BindRows()
        {
            dgvResults.Rows.Clear();

            foreach (var state in _states)
            {
                dgvResults.Rows.Add(
                    state.No,
                    state.Definition.Name,
                    state.Definition.Description,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "未実行",
                    string.Empty);
            }

            AdjustRightSideColumnWidths();

            if (dgvResults.Rows.Count > 0)
            {
                dgvResults.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// 全件実行押下
        /// </summary>
        private async void btnRunAll_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                return;
            }

            await ExecuteScenarioRangeAsync(Enumerable.Range(0, _states.Count).ToArray());
        }

        /// <summary>
        /// 選択実行押下
        /// </summary>
        private async void btnRunSelected_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                return;
            }

            var rowIndex = GetSelectedRowIndex();
            if (rowIndex < 0)
            {
                return;
            }

            await ExecuteScenarioRangeAsync(new[] { rowIndex });
        }

        /// <summary>
        /// 結果クリア押下
        /// </summary>
        private void btnClearResults_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                return;
            }

            foreach (var state in _states)
            {
                state.Result = null;
                state.StartAt = null;
                state.EndAt = null;
                state.StatusText = "未実行";
            }

            BindRows();
            UpdateOverview();
            lblStatus.Text = "待機中";
        }

        /// <summary>
        /// 複数シナリオ実行
        /// </summary>
        /// <param name="rowIndexes">対象行一覧</param>
        private async Task ExecuteScenarioRangeAsync(int[] rowIndexes)
        {
            SetRunningState(true);

            try
            {
                for (var i = 0; i < rowIndexes.Length; i++)
                {
                    var rowIndex = rowIndexes[i];
                    _runningScenarioIndex = rowIndex;
                    dgvResults.ClearSelection();
                    dgvResults.Rows[rowIndex].Selected = true;
                    UpdateOverview();
                    await ExecuteScenarioAsync(rowIndex);
                }
            }
            finally
            {
                _runningScenarioIndex = -1;
                SetRunningState(false);
                UpdateOverview();
            }
        }

        /// <summary>
        /// 単一シナリオ実行
        /// </summary>
        /// <param name="rowIndex">対象行</param>
        private async Task ExecuteScenarioAsync(int rowIndex)
        {
            var state = _states[rowIndex];
            var definition = state.Definition;
            var count = Decimal.ToInt32(numItemCount.Value);
            var repeat = Decimal.ToInt32(numRepeatCount.Value);

            state.StartAt = DateTime.Now;
            state.EndAt = null;
            state.Result = null;
            state.StatusText = "実行中";
            ApplyStateToGrid(rowIndex);
            UpdateOverview();

            // 実行中表示更新待ち
            await Task.Delay(20);

            var result = await Task.Run(() => definition.Run(count, repeat));

            state.Result = result;
            state.EndAt = DateTime.Now;
            state.StatusText = "完了";
            ApplyStateToGrid(rowIndex);
            UpdateOverview();
        }

        /// <summary>
        /// 状態反映
        /// </summary>
        /// <param name="rowIndex">対象行</param>
        private void ApplyStateToGrid(int rowIndex)
        {
            var state = _states[rowIndex];
            var row = dgvResults.Rows[rowIndex];
            var result = state.Result;

            row.Cells[3].Value = state.StartAt?.ToString("HH:mm:ss.fff") ?? string.Empty;
            row.Cells[4].Value = state.EndAt?.ToString("HH:mm:ss.fff") ?? string.Empty;
            row.Cells[5].Value = FormatMs(result?.ListMilliseconds);
            row.Cells[6].Value = FormatMs(result?.DictionaryMilliseconds);
            row.Cells[7].Value = FormatMs(result?.HashSetMilliseconds);
            row.Cells[8].Value = GetFastestName(result);
            row.Cells[9].Value = state.StatusText;
            row.Cells[10].Value = _isRunning && _runningScenarioIndex == rowIndex
                ? _spinnerFrames[_spinnerIndex]
                : state.StatusText == "完了" ? "■■■" : string.Empty;

            row.DefaultCellStyle.BackColor = state.StatusText == "実行中"
                ? Color.FromArgb(255, 250, 220)
                : Color.White;
        }

        /// <summary>
        /// ミリ秒表示文字列生成
        /// </summary>
        /// <param name="value">計測値</param>
        /// <returns>表示文字列</returns>
        private static string FormatMs(double? value)
        {
            return value.HasValue ? value.Value.ToString("0.000") : string.Empty;
        }

        /// <summary>
        /// 最速コレクション名取得
        /// </summary>
        /// <param name="result">計測結果</param>
        /// <returns>最速コレクション名</returns>
        private static string GetFastestName(BenchmarkResult? result)
        {
            if (result == null)
            {
                return string.Empty;
            }

            var items = new List<(string Name, double Value)>
            {
                ("List", result.ListMilliseconds),
                ("Dictionary", result.DictionaryMilliseconds),
                ("HashSet", result.HashSetMilliseconds),
            };

            return items.OrderBy(x => x.Value).First().Name;
        }

        /// <summary>
        /// 下部概要更新
        /// </summary>
        private void UpdateOverview()
        {
            var displayIndex = GetDisplayRowIndex();
            if (displayIndex < 0)
            {
                txtSummary.Text = "行を選ぶと、この処理の意味と結果を表示する";
                return;
            }

            var state = _states[displayIndex];
            var result = state.Result;
            var lines = new List<string>
            {
                $"No: {state.No}",
                $"処理名: {state.Definition.Name}",
                $"処理のイメージ: {state.Definition.SceneText}",
                $"どういう処理か: {state.Definition.Description}",
                $"対象件数: {Decimal.ToInt32(numItemCount.Value):N0}",
                $"反復回数: {Decimal.ToInt32(numRepeatCount.Value)}",
                $"開始時刻: {state.StartAt?.ToString("yyyy/MM/dd HH:mm:ss.fff") ?? "-"}",
                $"終了時刻: {state.EndAt?.ToString("yyyy/MM/dd HH:mm:ss.fff") ?? "-"}",
                $"状態: {state.StatusText}",
            };

            if (result != null)
            {
                var fastest = Math.Min(result.ListMilliseconds, Math.Min(result.DictionaryMilliseconds, result.HashSetMilliseconds));
                lines.Add(string.Empty);
                lines.Add($"List: {result.ListMilliseconds:0.000} ms  (最速比 x{result.ListMilliseconds / fastest:0.0})");
                lines.Add($"Dictionary: {result.DictionaryMilliseconds:0.000} ms  (最速比 x{result.DictionaryMilliseconds / fastest:0.0})");
                lines.Add($"HashSet: {result.HashSetMilliseconds:0.000} ms  (最速比 x{result.HashSetMilliseconds / fastest:0.0})");
                lines.Add($"最速: {GetFastestName(result)}");
                lines.Add(string.Empty);
                lines.Add($"見どころ: {result.Note}");
            }
            else if (_isRunning && _runningScenarioIndex == displayIndex)
            {
                lines.Add(string.Empty);
                lines.Add("現在実行中。完了後に結果を更新する");
            }
            else
            {
                lines.Add(string.Empty);
                lines.Add("まだ結果はない");
            }

            txtSummary.Text = string.Join(Environment.NewLine, lines);
        }

        /// <summary>
        /// 選択行取得
        /// </summary>
        /// <returns>行番号</returns>
        private int GetSelectedRowIndex()
        {
            if (dgvResults.SelectedRows.Count == 0)
            {
                return -1;
            }

            return dgvResults.SelectedRows[0].Index;
        }

        /// <summary>
        /// 概要表示対象行取得
        /// </summary>
        /// <returns>行番号</returns>
        private int GetDisplayRowIndex()
        {
            if (_isRunning && _runningScenarioIndex >= 0)
            {
                return _runningScenarioIndex;
            }

            return GetSelectedRowIndex();
        }

        /// <summary>
        /// 選択変更時処理
        /// </summary>
        private void dgvResults_SelectionChanged(object sender, EventArgs e)
        {
            UpdateOverview();
        }

        /// <summary>
        /// 実行状態切替
        /// </summary>
        /// <param name="isRunning">実行状態</param>
        private void SetRunningState(bool isRunning)
        {
            _isRunning = isRunning;
            btnRunAll.Enabled = !isRunning;
            btnRunSelected.Enabled = !isRunning;
            btnClearResults.Enabled = !isRunning;
            numItemCount.Enabled = !isRunning;
            numRepeatCount.Enabled = !isRunning;
            lblStatus.Text = isRunning ? "実行中" : "待機中";

            if (isRunning)
            {
                _spinnerIndex = 0;
                timerAnimation.Start();
            }
            else
            {
                timerAnimation.Stop();
                for (var i = 0; i < _states.Count; i++)
                {
                    ApplyStateToGrid(i);
                }
            }
        }

        /// <summary>
        /// 実行中アニメーション更新
        /// </summary>
        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            if (!_isRunning || _runningScenarioIndex < 0)
            {
                return;
            }

            _spinnerIndex = (_spinnerIndex + 1) % _spinnerFrames.Length;
            ApplyStateToGrid(_runningScenarioIndex);
        }

        /// <summary>
        /// Contains比較
        /// </summary>
        /// <param name="count">件数</param>
        /// <param name="repeat">反復回数</param>
        /// <returns>計測結果</returns>
        private static BenchmarkResult BenchmarkContains(int count, int repeat)
        {
            var source = Enumerable.Range(0, count).ToArray();
            var target = count - 1;
            var list = source.ToList();
            var dictionary = source.ToDictionary(x => x, x => x);
            var hashSet = source.ToHashSet();

            var listMs = Measure(repeat, () =>
            {
                var found = false;
                for (var i = 0; i < count; i++)
                {
                    found ^= list.Contains(target);
                }

                GC.KeepAlive(found);
            });

            var dictionaryMs = Measure(repeat, () =>
            {
                var found = false;
                for (var i = 0; i < count; i++)
                {
                    found ^= dictionary.ContainsKey(target);
                }

                GC.KeepAlive(found);
            });

            var hashSetMs = Measure(repeat, () =>
            {
                var found = false;
                for (var i = 0; i < count; i++)
                {
                    found ^= hashSet.Contains(target);
                }

                GC.KeepAlive(found);
            });

            return new BenchmarkResult(listMs, dictionaryMs, hashSetMs, "同じ判定を繰り返す処理では、先頭から探し直す回数がそのまま差になる");
        }

        /// <summary>
        /// ランダム参照比較
        /// </summary>
        /// <param name="count">件数</param>
        /// <param name="repeat">反復回数</param>
        /// <returns>計測結果</returns>
        private static BenchmarkResult BenchmarkRandomLookup(int count, int repeat)
        {
            var source = Enumerable.Range(0, count).ToArray();
            var queries = BuildRandomQueries(count, Math.Max(5000, count / 5));
            var list = source.ToList();
            var dictionary = source.ToDictionary(x => x, x => x);
            var hashSet = source.ToHashSet();

            var listMs = Measure(repeat, () =>
            {
                var total = 0;
                foreach (var q in queries)
                {
                    total += list.FirstOrDefault(x => x == q);
                }

                GC.KeepAlive(total);
            });

            var dictionaryMs = Measure(repeat, () =>
            {
                var total = 0;
                foreach (var q in queries)
                {
                    if (dictionary.TryGetValue(q, out var value))
                    {
                        total += value;
                    }
                }

                GC.KeepAlive(total);
            });

            var hashSetMs = Measure(repeat, () =>
            {
                var total = 0;
                foreach (var q in queries)
                {
                    if (hashSet.TryGetValue(q, out var value))
                    {
                        total += value;
                    }
                }

                GC.KeepAlive(total);
            });

            return new BenchmarkResult(listMs, dictionaryMs, hashSetMs, "番号から1件を引く処理では、取り出し方の違いが待ち時間に直結する");
        }

        /// <summary>
        /// 重複追加比較
        /// </summary>
        /// <param name="count">件数</param>
        /// <param name="repeat">反復回数</param>
        /// <returns>計測結果</returns>
        private static BenchmarkResult BenchmarkDuplicateAdds(int count, int repeat)
        {
            var source = Enumerable.Range(0, count).Select(x => x / 2).ToArray();

            var listMs = Measure(repeat, () =>
            {
                var list = new List<int>();
                foreach (var item in source)
                {
                    if (!list.Contains(item))
                    {
                        list.Add(item);
                    }
                }

                GC.KeepAlive(list.Count);
            });

            var dictionaryMs = Measure(repeat, () =>
            {
                var dictionary = new Dictionary<int, int>();
                foreach (var item in source)
                {
                    dictionary.TryAdd(item, item);
                }

                GC.KeepAlive(dictionary.Count);
            });

            var hashSetMs = Measure(repeat, () =>
            {
                var hashSet = new HashSet<int>();
                foreach (var item in source)
                {
                    hashSet.Add(item);
                }

                GC.KeepAlive(hashSet.Count);
            });

            return new BenchmarkResult(listMs, dictionaryMs, hashSetMs, "重複を飛ばしながら追加する処理では、既存確認の持ち方で差が開きやすい");
        }

        /// <summary>
        /// 除外一覧判定比較
        /// </summary>
        /// <param name="count">件数</param>
        /// <param name="repeat">反復回数</param>
        /// <returns>計測結果</returns>
        private static BenchmarkResult BenchmarkRandomRemove(int count, int repeat)
        {
            var source = Enumerable.Range(0, count).ToArray();
            var removeTargets = BuildRandomQueries(count, Math.Max(3000, count / 10));

            var listMs = Measure(repeat, () =>
            {
                var removeTargetList = removeTargets.ToList();
                var active = new List<int>(count);
                foreach (var item in source)
                {
                    if (!removeTargetList.Contains(item))
                    {
                        active.Add(item);
                    }
                }

                GC.KeepAlive(active.Count);
            });

            var dictionaryMs = Measure(repeat, () =>
            {
                var removeTargetDictionary = removeTargets.Distinct().ToDictionary(x => x, x => x);
                var active = new List<int>(count);
                foreach (var item in source)
                {
                    if (!removeTargetDictionary.ContainsKey(item))
                    {
                        active.Add(item);
                    }
                }

                GC.KeepAlive(active.Count);
            });

            var hashSetMs = Measure(repeat, () =>
            {
                var removeTargetHashSet = removeTargets.ToHashSet();
                var active = new List<int>(count);
                foreach (var item in source)
                {
                    if (!removeTargetHashSet.Contains(item))
                    {
                        active.Add(item);
                    }
                }

                GC.KeepAlive(active.Count);
            });

            return new BenchmarkResult(listMs, dictionaryMs, hashSetMs, "除外一覧を見ながら残す側を作る処理では、判定先の持ち方で差が開きやすい");
        }

        /// <summary>
        /// 突合比較
        /// </summary>
        /// <param name="count">件数</param>
        /// <param name="repeat">反復回数</param>
        /// <returns>計測結果</returns>
        private static BenchmarkResult BenchmarkIntersection(int count, int repeat)
        {
            var left = Enumerable.Range(0, count).ToArray();
            var right = Enumerable.Range(count / 2, count).ToArray();
            var listRight = right.ToList();
            var dictionaryRight = right.ToDictionary(x => x, x => x);
            var hashSetRight = right.ToHashSet();

            var listMs = Measure(repeat, () =>
            {
                var hit = 0;
                foreach (var item in left)
                {
                    if (listRight.Contains(item))
                    {
                        hit++;
                    }
                }

                GC.KeepAlive(hit);
            });

            var dictionaryMs = Measure(repeat, () =>
            {
                var hit = 0;
                foreach (var item in left)
                {
                    if (dictionaryRight.ContainsKey(item))
                    {
                        hit++;
                    }
                }

                GC.KeepAlive(hit);
            });

            var hashSetMs = Measure(repeat, () =>
            {
                var hit = 0;
                foreach (var item in left)
                {
                    if (hashSetRight.Contains(item))
                    {
                        hit++;
                    }
                }

                GC.KeepAlive(hit);
            });

            return new BenchmarkResult(listMs, dictionaryMs, hashSetMs, "名簿どうしの突合では、片方を判定用の集合で持つだけで時間が大きく変わる");
        }

        /// <summary>
        /// 更新対象参照比較
        /// </summary>
        /// <param name="count">件数</param>
        /// <param name="repeat">反復回数</param>
        /// <returns>計測結果</returns>
        private static BenchmarkResult BenchmarkUpdateLookup(int count, int repeat)
        {
            var baseItems = Enumerable.Range(0, count).Select(x => new MutableItem { Id = x, Score = x }).ToArray();
            var updates = BuildRandomQueries(count, Math.Max(5000, count / 5));

            var listMs = Measure(repeat, () =>
            {
                var list = baseItems.Select(x => new MutableItem { Id = x.Id, Score = x.Score }).ToList();
                foreach (var id in updates)
                {
                    var item = list.FirstOrDefault(x => x.Id == id);
                    if (item != null)
                    {
                        item.Score++;
                    }
                }

                GC.KeepAlive(list.Count);
            });

            var dictionaryMs = Measure(repeat, () =>
            {
                var dictionary = baseItems.ToDictionary(x => x.Id, x => new MutableItem { Id = x.Id, Score = x.Score });
                foreach (var id in updates)
                {
                    if (dictionary.TryGetValue(id, out var item))
                    {
                        item.Score++;
                    }
                }

                GC.KeepAlive(dictionary.Count);
            });

            var hashSetMs = Measure(repeat, () =>
            {
                var hashSet = new HashSet<MutableItem>(baseItems.Select(x => new MutableItem { Id = x.Id, Score = x.Score }));
                foreach (var id in updates)
                {
                    if (hashSet.TryGetValue(new MutableItem { Id = id }, out var item))
                    {
                        item.Score++;
                    }
                }

                GC.KeepAlive(hashSet.Count);
            });

            return new BenchmarkResult(listMs, dictionaryMs, hashSetMs, "書き換えより前の『探す時間』が長いと、更新全体の待ち時間が伸びやすい");
        }

        /// <summary>
        /// 会員判定比較
        /// </summary>
        /// <param name="count">件数</param>
        /// <param name="repeat">反復回数</param>
        /// <returns>計測結果</returns>
        private static BenchmarkResult BenchmarkMembershipFilter(int count, int repeat)
        {
            var members = Enumerable.Range(0, count).Where(x => x % 3 == 0).ToArray();
            var details = Enumerable.Range(0, count).Select(x => x + (x % 2 == 0 ? 0 : count / 3)).ToArray();
            var memberList = members.ToList();
            var memberDictionary = members.ToDictionary(x => x, x => x);
            var memberHashSet = members.ToHashSet();

            var listMs = Measure(repeat, () =>
            {
                var matched = 0;
                foreach (var detail in details)
                {
                    if (memberList.Contains(detail))
                    {
                        matched++;
                    }
                }

                GC.KeepAlive(matched);
            });

            var dictionaryMs = Measure(repeat, () =>
            {
                var matched = 0;
                foreach (var detail in details)
                {
                    if (memberDictionary.ContainsKey(detail))
                    {
                        matched++;
                    }
                }

                GC.KeepAlive(matched);
            });

            var hashSetMs = Measure(repeat, () =>
            {
                var matched = 0;
                foreach (var detail in details)
                {
                    if (memberHashSet.Contains(detail))
                    {
                        matched++;
                    }
                }

                GC.KeepAlive(matched);
            });

            return new BenchmarkResult(listMs, dictionaryMs, hashSetMs, "明細を全件なめる前処理では、会員判定の持ち方が全体時間を左右しやすい");
        }

        /// <summary>
        /// ランダム問い合わせ生成
        /// </summary>
        /// <param name="count">最大値</param>
        /// <param name="queryCount">生成件数</param>
        /// <returns>問い合わせ一覧</returns>
        private static int[] BuildRandomQueries(int count, int queryCount)
        {
            var random = new Random(12345);
            var queries = new int[queryCount];
            for (var i = 0; i < queryCount; i++)
            {
                queries[i] = random.Next(0, count);
            }

            return queries;
        }

        /// <summary>
        /// 計測処理
        /// </summary>
        /// <param name="repeat">反復回数</param>
        /// <param name="action">計測対象処理</param>
        /// <returns>平均ミリ秒</returns>
        private static double Measure(int repeat, Action action)
        {
            // 初回JIT影響切り分け用ウォームアップ
            action();

            var totalTicks = 0L;
            for (var i = 0; i < repeat; i++)
            {
                // 計測前GC実行
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                var sw = Stopwatch.StartNew();
                action();
                sw.Stop();
                totalTicks += sw.ElapsedTicks;
            }

            return totalTicks * 1000d / Stopwatch.Frequency / repeat;
        }
    }

    /// <summary>
    /// シナリオ定義
    /// </summary>
    public sealed class ScenarioDefinition
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">処理名</param>
        /// <param name="description">説明</param>
        /// <param name="run">実行処理</param>
        /// <param name="sceneText">処理イメージ</param>
        public ScenarioDefinition(string name, string description, Func<int, int, BenchmarkResult> run, string sceneText)
        {
            Name = name;
            Description = description;
            Run = run;
            SceneText = sceneText;
        }

        /// <summary>
        /// 処理名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 実行処理
        /// </summary>
        public Func<int, int, BenchmarkResult> Run { get; }

        /// <summary>
        /// 処理イメージ
        /// </summary>
        public string SceneText { get; }
    }

    /// <summary>
    /// 実行状態
    /// </summary>
    public sealed class ScenarioExecutionState
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="no">表示番号</param>
        /// <param name="definition">シナリオ定義</param>
        public ScenarioExecutionState(int no, ScenarioDefinition definition)
        {
            No = no;
            Definition = definition;
            StatusText = "未実行";
        }

        /// <summary>
        /// 表示番号
        /// </summary>
        public int No { get; }

        /// <summary>
        /// シナリオ定義
        /// </summary>
        public ScenarioDefinition Definition { get; }

        /// <summary>
        /// 実行結果
        /// </summary>
        public BenchmarkResult? Result { get; set; }

        /// <summary>
        /// 開始時刻
        /// </summary>
        public DateTime? StartAt { get; set; }

        /// <summary>
        /// 終了時刻
        /// </summary>
        public DateTime? EndAt { get; set; }

        /// <summary>
        /// 状態表示
        /// </summary>
        public string StatusText { get; set; }
    }

    /// <summary>
    /// ベンチマーク結果
    /// </summary>
    public sealed class BenchmarkResult
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="listMilliseconds">List計測値</param>
        /// <param name="dictionaryMilliseconds">Dictionary計測値</param>
        /// <param name="hashSetMilliseconds">HashSet計測値</param>
        /// <param name="note">見どころ</param>
        public BenchmarkResult(double listMilliseconds, double dictionaryMilliseconds, double hashSetMilliseconds, string note)
        {
            ListMilliseconds = listMilliseconds;
            DictionaryMilliseconds = dictionaryMilliseconds;
            HashSetMilliseconds = hashSetMilliseconds;
            Note = note;
        }

        /// <summary>
        /// List計測値
        /// </summary>
        public double ListMilliseconds { get; }

        /// <summary>
        /// Dictionary計測値
        /// </summary>
        public double DictionaryMilliseconds { get; }

        /// <summary>
        /// HashSet計測値
        /// </summary>
        public double HashSetMilliseconds { get; }

        /// <summary>
        /// 見どころ
        /// </summary>
        public string Note { get; }
    }

    /// <summary>
    /// 更新比較用可変要素
    /// </summary>
    public sealed class MutableItem : IEquatable<MutableItem>
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 点数
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 同値判定
        /// </summary>
        /// <param name="other">比較対象</param>
        /// <returns>判定結果</returns>
        public bool Equals(MutableItem? other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        /// <summary>
        /// 同値判定
        /// </summary>
        /// <param name="obj">比較対象</param>
        /// <returns>判定結果</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as MutableItem);
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
