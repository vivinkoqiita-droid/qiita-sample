using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DesignerSafeControlSample
{
    public partial class MainForm : Form
    {
        private readonly StringBuilder _logBuilder = new StringBuilder();

        public MainForm()
        {
            InitializeComponent();
            InitializeScreen();
        }

        private void InitializeScreen()
        {
            RuntimeEnvironment.CurrentMode = RuntimeMode.Runtime;

            resultTextBox.Clear();
            brokenPreviewPanel.Controls.Clear();
            safePreviewPanel.Controls.Clear();

            brokenStateLabel.Text = "未実行";
            brokenStateLabel.ForeColor = Color.DimGray;
            brokenStageLabel.Text = "発生段階: -";
            brokenMessageLabel.Text = "Visual Studio で見慣れた例外文面をそのまま表示";

            safeStateLabel.Text = "未実行";
            safeStateLabel.ForeColor = Color.DimGray;
            safeStageLabel.Text = "発生段階: -";
            safeMessageLabel.Text = "設計時は生成だけ通し、重い初期化は後ろへ送る";

            AppendLog("観測用サンプルを起動しました。");
            AppendLog("この画面は、Broken / Safe の差を操作で確認するためのものです。");
            AppendLog(string.Empty);
            AppendLog("確認ボタン:");
            AppendLog("1. Broken を設計時生成");
            AppendLog("2. Safe を設計時生成");
            AppendLog("3. DefaultValue 保存差");
            AppendLog("4. TypeConverter 比較");
        }

        private void runBrokenButton_Click(object sender, EventArgs e)
        {
            brokenPreviewPanel.Controls.Clear();

            brokenStateLabel.Text = "実行中";
            brokenStateLabel.ForeColor = Color.DarkOrange;
            brokenStageLabel.Text = "発生段階: 型初期化を開始";
            brokenMessageLabel.Text = "BrokenCaptionControl を設計時想定で生成";

            AppendSection("Broken を設計時生成");
            AppendLog("設計時想定へ切り替えて BrokenCaptionControl を生成します。");

            try
            {
                RuntimeEnvironment.CurrentMode = RuntimeMode.Design;

                var control = new BrokenCaptionControl();
                control.Location = new Point(12, 12);

                brokenPreviewPanel.Controls.Add(control);

                brokenStateLabel.Text = "想定外に通過";
                brokenStateLabel.ForeColor = Color.DarkOrange;
                brokenStageLabel.Text = "発生段階: なし";
                brokenMessageLabel.Text = "想定では static 初期化で停止する場面";

                AppendLog("Broken 側の生成が通りました。");
                AppendLog("この結果は想定外です。環境条件を再確認してください。");
            }
            catch (Exception ex)
            {
                var stage = ResolveStage(ex);

                brokenStateLabel.Text = "失敗";
                brokenStateLabel.ForeColor = Color.Firebrick;
                brokenStageLabel.Text = "発生段階: " + stage;
                brokenMessageLabel.Text = GetDisplayMessage(ex);

                AppendLog("Broken 側は設計時の早い段階で停止しました。");
                AppendLog("static 初期化または ctor に重い処理を書くと、この形になりやすくなります。");
                AppendLog(string.Empty);
                AppendExceptionDetail("Broken 例外詳細", stage, ex);
            }
            finally
            {
                RuntimeEnvironment.CurrentMode = RuntimeMode.Runtime;
            }
        }

        private void runSafeButton_Click(object sender, EventArgs e)
        {
            safePreviewPanel.Controls.Clear();

            safeStateLabel.Text = "実行中";
            safeStateLabel.ForeColor = Color.DarkOrange;
            safeStageLabel.Text = "発生段階: 生成を開始";
            safeMessageLabel.Text = "SafeCaptionControl を設計時想定で生成";

            AppendSection("Safe を設計時生成");
            AppendLog("設計時想定へ切り替えて SafeCaptionControl を生成します。");

            try
            {
                RuntimeEnvironment.CurrentMode = RuntimeMode.Design;

                var control = new SafeCaptionControl();
                control.Location = new Point(12, 12);
                control.Caption = "設計時でも生成だけは通す";

                safePreviewPanel.Controls.Add(control);

                safeStateLabel.Text = "通過";
                safeStateLabel.ForeColor = Color.DarkGreen;
                safeStageLabel.Text = "発生段階: 生成完了";
                safeMessageLabel.Text = "設計時のため実行時初期化を止め、生成だけ通過";

                AppendLog("Safe 側は生成できました。");
                AppendLog("重い初期化は遅延されるため、画面へ載せるところまでは通ります。");
            }
            catch (Exception ex)
            {
                var stage = ResolveStage(ex);

                safeStateLabel.Text = "失敗";
                safeStateLabel.ForeColor = Color.Firebrick;
                safeStageLabel.Text = "発生段階: " + stage;
                safeMessageLabel.Text = GetDisplayMessage(ex);

                AppendLog("Safe 側の生成で失敗しました。");
                AppendExceptionDetail("Safe 例外詳細", stage, ex);
            }
            finally
            {
                RuntimeEnvironment.CurrentMode = RuntimeMode.Runtime;
            }
        }

        private void showPropertyButton_Click(object sender, EventArgs e)
        {
            AppendSection("DefaultValue 保存差");

            var broken = new BrokenPropertyControl();
            var safe = new SafePropertyControl();

            AppendLog("初期状態:");
            AppendLog("Broken.PaddingEx = " + broken.PaddingEx);
            AppendLog("Safe.PaddingEx   = " + safe.PaddingEx);
            AppendLog(string.Empty);

            var brokenProperty = TypeDescriptor.GetProperties(broken)["PaddingEx"];
            var safeProperty = TypeDescriptor.GetProperties(safe)["PaddingEx"];

            AppendLog("ShouldSerializeValue 初期判定:");
            AppendLog("Broken = " + (brokenProperty != null && brokenProperty.ShouldSerializeValue(broken)));
            AppendLog("Safe   = " + (safeProperty != null && safeProperty.ShouldSerializeValue(safe)));
            AppendLog(string.Empty);

            broken.PaddingEx = 12;
            safe.PaddingEx = 12;

            AppendLog("値変更後:");
            AppendLog("Broken.PaddingEx = " + broken.PaddingEx);
            AppendLog("Safe.PaddingEx   = " + safe.PaddingEx);
            AppendLog(string.Empty);

            AppendLog("想定される .Designer.cs 出力:");
            AppendLog("this.brokenPropertyControl1.PaddingEx = 12;");
            AppendLog("this.safePropertyControl1.PaddingEx = 12;");
            AppendLog(string.Empty);

            broken.PaddingEx = 0;
            safe.PaddingEx = 8;

            AppendLog("既定値へ戻したつもりの状態:");
            AppendLog("Broken.PaddingEx = " + broken.PaddingEx + "  ← 属性上の既定値");
            AppendLog("Safe.PaddingEx   = " + safe.PaddingEx + "  ← 実値と属性が一致");
            AppendLog(string.Empty);

            AppendLog("ShouldSerializeValue 再判定:");
            AppendLog("Broken = " + (brokenProperty != null && brokenProperty.ShouldSerializeValue(broken)));
            AppendLog("Safe   = " + (safeProperty != null && safeProperty.ShouldSerializeValue(safe)));
            AppendLog(string.Empty);

            AppendLog("Broken 側は属性既定値と実初期値がずれているため、保存差が揺れやすくなります。");
        }

        private void showConverterButton_Click(object sender, EventArgs e)
        {
            AppendSection("TypeConverter 比較");

            var brokenConverter = TypeDescriptor.GetConverter(typeof(BrokenPathSetting));
            var safeConverter = TypeDescriptor.GetConverter(typeof(PathSetting));

            RuntimeEnvironment.CurrentMode = RuntimeMode.Design;

            var invalidInput = "   ";
            AppendLog("入力値: [" + invalidInput + "]");
            AppendLog("設計時想定で比較します。");
            AppendLog(string.Empty);

            try
            {
                var broken = brokenConverter.ConvertFrom(null, null, invalidInput);
                AppendLog("Broken 変換結果: " + Convert.ToString(broken));
            }
            catch (Exception ex)
            {
                AppendLog("Broken 側は変換で失敗しました。");
                AppendExceptionDetail("Broken Converter 例外詳細", "TypeConverter.ConvertFrom", ex);
            }

            try
            {
                var safe = safeConverter.ConvertFrom(null, null, invalidInput);
                AppendLog("Safe 変換結果: " + Convert.ToString(safe));
                AppendLog("Safe 側は安全値へ戻して継続します。");
            }
            catch (Exception ex)
            {
                AppendLog("Safe 側の変換で失敗しました。");
                AppendExceptionDetail("Safe Converter 例外詳細", "TypeConverter.ConvertFrom", ex);
            }
            finally
            {
                RuntimeEnvironment.CurrentMode = RuntimeMode.Runtime;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            _logBuilder.Clear();
            InitializeScreen();
        }

        private void AppendExceptionDetail(string title, string stage, Exception ex)
        {
            AppendLog(title);
            AppendLog("発生段階: " + stage);
            AppendLog(string.Empty);

            var root = GetRootException(ex);

            AppendLog("原因例外:");
            AppendLog(root.GetType().FullName);
            AppendLog(root.Message);
            AppendLog(string.Empty);

            AppendLog("表面例外:");
            AppendLog(ex.GetType().FullName);
            AppendLog(ex.Message);
            AppendLog(string.Empty);

            AppendLog("詳細:");
            AppendLog(ex.ToString());
            AppendLog(string.Empty);
        }

        private static string ResolveStage(Exception ex)
        {
            if (ex is TypeInitializationException)
            {
                return "static 初期化";
            }

            if (ex.TargetSite != null && ex.TargetSite.Name == ".ctor")
            {
                return "ctor";
            }

            return "生成中";
        }

        private static string GetDisplayMessage(Exception ex)
        {
            var root = GetRootException(ex);

            return "表面: " + ex.Message + Environment.NewLine
                 + "原因: " + root.Message;
        }

        private static Exception GetRootException(Exception ex)
        {
            var current = ex;

            while (current.InnerException != null)
            {
                current = current.InnerException;
            }

            return current;
        }

        private void AppendSection(string title)
        {
            AppendLog(string.Empty);
            AppendLog("==== " + title + " ====");
        }

        private void AppendLog(string message)
        {
            _logBuilder.AppendLine(message);
            resultTextBox.Text = _logBuilder.ToString();
        }
    }

    public enum RuntimeMode
    {
        Runtime,
        Design
    }

    internal static class RuntimeEnvironment
    {
        public static RuntimeMode CurrentMode { get; set; } = RuntimeMode.Runtime;
    }

    internal static class BrokenEnvironment
    {
        public static void RequireRuntimeOnlyResource()
        {
            if (RuntimeEnvironment.CurrentMode == RuntimeMode.Design)
            {
                throw new InvalidOperationException("Runtime only resource was not available.");
            }
        }
    }

    internal static class RuntimeOnlyInitializer
    {
        public static void Touch()
        {
            BrokenEnvironment.RequireRuntimeOnlyResource();
        }
    }

    internal static class DesignTimeHelper
    {
        public static bool IsInDesignMode(IComponent component)
        {
            if (RuntimeEnvironment.CurrentMode == RuntimeMode.Design)
            {
                return true;
            }

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return true;
            }

            if (component != null && component.Site != null && component.Site.DesignMode)
            {
                return true;
            }

            var control = component as Control;
            if (control != null)
            {
                var parent = control.Parent;
                while (parent != null)
                {
                    if (parent.Site != null && parent.Site.DesignMode)
                    {
                        return true;
                    }

                    parent = parent.Parent;
                }
            }

            return false;
        }
    }

    public class BrokenCaptionControl : Control
    {
        static BrokenCaptionControl()
        {
            BrokenEnvironment.RequireRuntimeOnlyResource();
        }

        public BrokenCaptionControl()
        {
            BrokenEnvironment.RequireRuntimeOnlyResource();
            Size = new Size(260, 44);
            BackColor = Color.MistyRose;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            TextRenderer.DrawText(
                e.Graphics,
                "BrokenCaptionControl",
                Font,
                ClientRectangle,
                Color.DarkRed,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }
    }

    public abstract class DesignSafeControlBase : Control
    {
        private bool _runtimeInitialized;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (DesignTimeHelper.IsInDesignMode(this))
            {
                return;
            }

            if (_runtimeInitialized)
            {
                return;
            }

            _runtimeInitialized = true;
            InitializeRuntime();
        }

        protected abstract void InitializeRuntime();
    }

    public class SafeCaptionControl : DesignSafeControlBase
    {
        private string _caption = string.Empty;

        public SafeCaptionControl()
        {
            Size = new Size(260, 44);
            BackColor = Color.Honeydew;
        }

        [Category("カスタム")]
        [Description("表示用の文字列")]
        [DefaultValue("")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Caption
        {
            get { return _caption; }
            set
            {
                var next = value ?? string.Empty;

                if (_caption == next)
                {
                    return;
                }

                _caption = next;
                Invalidate();
            }
        }

        protected override void InitializeRuntime()
        {
            RuntimeOnlyInitializer.Touch();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            TextRenderer.DrawText(
                e.Graphics,
                string.IsNullOrEmpty(_caption) ? "SafeCaptionControl" : _caption,
                Font,
                ClientRectangle,
                Color.DarkGreen,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }
    }

    public class BrokenPropertyControl : Control
    {
        private int _paddingEx = 8;

        [DefaultValue(0)]
        public int PaddingEx
        {
            get { return _paddingEx; }
            set { _paddingEx = value; }
        }
    }

    public class SafePropertyControl : Control
    {
        private int _paddingEx = 8;

        [DefaultValue(8)]
        public int PaddingEx
        {
            get { return _paddingEx; }
            set { _paddingEx = value; }
        }
    }

    [TypeConverter(typeof(BrokenPathSettingConverter))]
    public struct BrokenPathSetting
    {
        private readonly string _value;

        public BrokenPathSetting(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException("Path value was empty.");
            }

            _value = value.Trim();
        }

        public override string ToString()
        {
            return _value ?? string.Empty;
        }
    }

    public sealed class BrokenPathSettingConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            var text = value as string ?? string.Empty;
            return new BrokenPathSetting(text);
        }
    }

    [TypeConverter(typeof(SafePathSettingConverter))]
    public struct PathSetting
    {
        private readonly string _value;

        private PathSetting(string value)
        {
            _value = value ?? string.Empty;
        }

        public static PathSetting Empty
        {
            get { return new PathSetting(string.Empty); }
        }

        public static PathSetting CreateUnsafe(string value)
        {
            return new PathSetting(value);
        }

        public static PathSetting Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Empty;
            }

            return new PathSetting(value.Trim());
        }

        public override string ToString()
        {
            return _value ?? string.Empty;
        }
    }

    public sealed class SafePathSettingConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            try
            {
                var text = value as string ?? string.Empty;

                var component = context != null ? context.Instance as IComponent : null;
                if (component != null && DesignTimeHelper.IsInDesignMode(component))
                {
                    return PathSetting.CreateUnsafe(text);
                }

                if (RuntimeEnvironment.CurrentMode == RuntimeMode.Design)
                {
                    return PathSetting.CreateUnsafe(text);
                }

                return PathSetting.Parse(text);
            }
            catch
            {
                return PathSetting.Empty;
            }
        }
    }
}