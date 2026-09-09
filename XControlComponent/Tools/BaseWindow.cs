using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using XControlHelper;

namespace XControlComponents.Tools
{
    public class BaseWindow : Window
    {
        private bool isResizingLeft = false;
        private const double GripSize = 8;

        private bool _isResize;
        public bool _IsResize
        {
            get => _isResize;
            set
            {
                _isResize = value;
            }
        }

        public BaseWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.CanResize;
            Background = XElementHelper.GetColor(Colors.Transparent);
            AllowsTransparency = true;
            _IsResize = XHeaderDefaults.IsResize;
            Loaded += BaseWindow_Loaded;
            Deactivated += BaseWindow_Deactivated;

            //Set And Change Default Of Controls
            XTextBoxDefaults.Height = 27;
        }

        private void BaseWindow_Deactivated(object sender, EventArgs e)
        {
            XPopupManager.CloseAllPopups();
            //XPopupManager.CloseAllDialogHosts();
        }

        private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isResize)
            {
                if (Content is not FrameworkElement originalContent)
                    return;

                var root = new Grid();
                Content = null;
                root.Children.Add(originalContent);

                var overlay = CreateResizeGrips();
                root.Children.Add(overlay);

                Content = root;
            }
        }

        private UIElement CreateResizeGrips()
        {
            var overlay = new Grid { IsHitTestVisible = true };

            //AddThumb(overlay, HorizontalAlignment.Left, VerticalAlignment.Stretch, Cursors.SizeWE, ResizeLeft);
            AddThumb(overlay, HorizontalAlignment.Right, VerticalAlignment.Stretch, Cursors.SizeWE, ResizeRight);
            AddThumb(overlay, HorizontalAlignment.Stretch, VerticalAlignment.Top, Cursors.SizeNS, ResizeTop);
            AddThumb(overlay, HorizontalAlignment.Stretch, VerticalAlignment.Bottom, Cursors.SizeNS, ResizeBottom);

            //AddThumb(overlay, HorizontalAlignment.Left, VerticalAlignment.Top, Cursors.SizeNWSE, (s, e) => { ResizeLeft(s, e); ResizeTop(s, e); });
            AddThumb(overlay, HorizontalAlignment.Right, VerticalAlignment.Top, Cursors.SizeNESW, (s, e) => { ResizeRight(s, e); ResizeTop(s, e); });
            AddThumb(overlay, HorizontalAlignment.Left, VerticalAlignment.Bottom, Cursors.SizeNESW, (s, e) => { ResizeLeft(s, e); ResizeBottom(s, e); });
            AddThumb(overlay, HorizontalAlignment.Right, VerticalAlignment.Bottom, Cursors.SizeNWSE, (s, e) => { ResizeRight(s, e); ResizeBottom(s, e); });

            return overlay;
        }

        private void AddThumb(Panel parent, HorizontalAlignment hAlign, VerticalAlignment vAlign, Cursor cursor, DragDeltaEventHandler onDrag)
        {
            var thumb = new Thumb
            {
                HorizontalAlignment = hAlign,
                VerticalAlignment = vAlign,
                Width = (hAlign == HorizontalAlignment.Stretch) ? double.NaN : GripSize,
                Height = (vAlign == VerticalAlignment.Stretch) ? double.NaN : GripSize,
                Background = Brushes.Transparent,
                Cursor = cursor
            };
            thumb.DragDelta += onDrag;
            parent.Children.Add(thumb);
        }

        private void ResizeLeft(object sender, DragDeltaEventArgs e)
        {
            if (isResizingLeft)
                return;

            isResizingLeft = true;

            try
            {
                double horizontalChange = e.HorizontalChange;

                // حداقل تغییر لازم برای اعمال resize
                if (Math.Abs(horizontalChange) < 1)
                {
                    isResizingLeft = false;
                    return;
                }

                double newWidth = Width - horizontalChange;
                double newLeft = Left + horizontalChange;

                if (newWidth < MinWidth)
                {
                    horizontalChange = Width - MinWidth;
                    newWidth = MinWidth;
                    newLeft = Left + horizontalChange;
                }

                // اعمال همزمان تغییرات
                Left = newLeft;
                Width = newWidth;
            }
            finally
            {
                isResizingLeft = false;
            }
        }

        private void ResizeRight(object sender, DragDeltaEventArgs e)
        {
            Width = Math.Max(MinWidth, Width + e.HorizontalChange);
        }

        private void ResizeTop(object sender, DragDeltaEventArgs e)
        {
            double newHeight = Height - e.VerticalChange;
            double newTop = Top + e.VerticalChange;

            if (newHeight < MinHeight)
            {
                double maxAllowedChange = Height - MinHeight;
                newTop = Top + maxAllowedChange;
                newHeight = MinHeight;
            }

            Top = newTop;
            Height = newHeight;
        }

        private void ResizeBottom(object sender, DragDeltaEventArgs e)
        {
            Height = Math.Max(MinHeight, Height + e.VerticalChange);
        }
    }
}
