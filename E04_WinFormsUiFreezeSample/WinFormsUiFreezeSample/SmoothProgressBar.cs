using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsUiFreezeSample
{
    /// <summary>
    /// 学習画面用カスタムプログレスバー
    /// </summary>
    public sealed class SmoothProgressBar : Control
    {
        private int maximum = 1000;
        private int value;
        private Color trackColor = Color.FromArgb(236, 240, 245);
        private Color startColor = Color.FromArgb(52, 152, 219);
        private Color endColor = Color.FromArgb(46, 204, 113);
        private bool showShine = true;

        /// <summary>
        /// 初期化
        /// </summary>
        public SmoothProgressBar()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            Size = new Size(320, 20);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// 最大値
        /// </summary>
        [DefaultValue(1000)]
        public int Maximum
        {
            get => maximum;
            set
            {
                maximum = Math.Max(1, value);
                this.value = Math.Min(this.value, maximum);
                Invalidate();
            }
        }

        /// <summary>
        /// 現在値
        /// </summary>
        [DefaultValue(0)]
        public int Value
        {
            get => value;
            set
            {
                var normalized = Math.Max(0, Math.Min(Maximum, value));
                if (this.value == normalized)
                {
                    return;
                }

                this.value = normalized;
                Invalidate();
            }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        [Browsable(true)]
        public Color TrackColor
        {
            get => trackColor;
            set
            {
                trackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 塗りつぶし開始色
        /// </summary>
        [Browsable(true)]
        public Color StartColor
        {
            get => startColor;
            set
            {
                startColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 塗りつぶし終了色
        /// </summary>
        [Browsable(true)]
        public Color EndColor
        {
            get => endColor;
            set
            {
                endColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// ハイライト表示有無
        /// </summary>
        [DefaultValue(true)]
        public bool ShowShine
        {
            get => showShine;
            set
            {
                showShine = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e">描画イベント引数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var rect = ClientRectangle;
            // 右端下端の描画欠け回避
            rect.Width -= 1;
            rect.Height -= 1;

            if (rect.Width <= 0 || rect.Height <= 0)
            {
                return;
            }

            using var trackPath = CreateRoundRect(rect, Math.Min(rect.Height, 10));
            using var trackBrush = new SolidBrush(TrackColor);
            using var borderPen = new Pen(Color.FromArgb(210, 218, 230));

            e.Graphics.FillPath(trackBrush, trackPath);
            e.Graphics.DrawPath(borderPen, trackPath);

            if (Value <= 0)
            {
                return;
            }

            // 最小1px確保による進捗視認性維持
            var fillWidth = (int)Math.Round(rect.Width * (Value / (double)Maximum));
            fillWidth = Math.Max(1, Math.Min(rect.Width, fillWidth));

            var fillRect = new Rectangle(rect.X, rect.Y, fillWidth, rect.Height);
            using var fillPath = CreateRoundRect(fillRect, Math.Min(fillRect.Height, 10));
            using var fillBrush = new LinearGradientBrush(fillRect, StartColor, EndColor, LinearGradientMode.Horizontal);
            e.Graphics.FillPath(fillBrush, fillPath);

            if (ShowShine && fillRect.Width > 20)
            {
                var shineLeft = Math.Max(fillRect.Left, fillRect.Right - Math.Max(24, fillRect.Width / 5));
                var shineRect = new Rectangle(shineLeft, fillRect.Top, fillRect.Right - shineLeft, fillRect.Height);
                using var shineBrush = new LinearGradientBrush(
                    shineRect,
                    Color.FromArgb(0, Color.White),
                    Color.FromArgb(90, Color.White),
                    LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(shineBrush, shineRect);
            }
        }

        /// <summary>
        /// 角丸矩形生成
        /// </summary>
        /// <param name="rect">対象矩形</param>
        /// <param name="radius">角丸半径</param>
        /// <returns>描画パス</returns>
        private static GraphicsPath CreateRoundRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            var diameter = radius * 2;

            if (diameter <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
