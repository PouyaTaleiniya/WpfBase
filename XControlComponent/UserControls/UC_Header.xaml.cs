using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using XControlComponents.Tools;
using XControlComponents.Tools.Controls;


namespace XControlComponents.UserControls
{
    /// <summary>
    /// Interaction logic for UC_Header.xaml
    /// </summary>
    public partial class UC_Header : UserControl
    {
        private Rect _NormalBounds { get; set; }
        private bool IsMaximized { get; set; }

        public UC_Header()
        {
            InitializeComponent();
            _ImageUrl = XHeaderDefaults.ImageUrl;
            _FontSize = XHeaderDefaults.FontSize;
            _ShowMaximumButton = XHeaderDefaults.showMaximumButton;
        }

        private bool? _showMaximumButton;
        public bool? _ShowMaximumButton
        {
            get => _showMaximumButton;
            set
            {
                _showMaximumButton = value;
                if (_showMaximumButton == null)
                    _ShowMaximumButton = XHeaderDefaults.showMaximumButton;
                GR_Buttons_Handle();
            }
        }

        private string _title;
        public string _Title
        {
            get => _title;
            set
            {
                _title = value;
                Lb_Title.Content = _title;
            }
        }

        private double? _fontSize;
        public double? _FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;

                //Default
                if (_fontSize <= 0 || _fontSize == null)
                    _fontSize = XHeaderDefaults.FontSize;

                Lb_Title.FontSize = _fontSize.Value;
            }
        }

        private string _imageUrl;
        public string _ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                Img_Header.SetImageSource(_imageUrl);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var getWindow = Window.GetWindow(this);
            if (getWindow != null)
            {
                getWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                getWindow.MinHeight = getWindow.Height;
                getWindow.MinWidth = getWindow.Width;
                getWindow.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

                //Border Box
                var getBorder = XElementHelper.FindChild<Border>(getWindow);

                //Flow Direction
                getBorder.FlowDirection = FlowDirection.RightToLeft;

                //Style
                getBorder.Style = XAppMethods.BorderWindow();
            }
        }

        private void GR_Close_MouseEnter(object sender, MouseEventArgs e)
        {
            BR_Close.Background = XAppMethods.Color_Red_c42b1c();
            Img_Close.SetImageSource("/Images/CloseWhite.png");
        }

        private void GR_Close_MouseLeave(object sender, MouseEventArgs e)
        {
            BR_Close.Background = XElementHelper.GetBrushColor(Colors.Transparent);
            Img_Close.SetImageSource("/Images/Close.png");
        }

        private void GR_Close_Click(object sender, MouseButtonEventArgs e)
        {
            var getWindow = Window.GetWindow(this);
            getWindow.Close();
        }

        private void GR_Minimize_MouseEnter(object sender, MouseEventArgs e)
        {
            GR_Minimize.Background = XAppMethods.Color_White_f8f9ff();
        }

        private void GR_Minimize_MouseLeave(object sender, MouseEventArgs e)
        {
            GR_Minimize.Background = XElementHelper.GetBrushColor(Colors.Transparent);
        }

        private void GR_Minimize_Click(object sender, MouseButtonEventArgs e)
        {
            var getWindow = Window.GetWindow(this);
            getWindow.WindowState = WindowState.Minimized;
        }

        private void GR_Maximize_MouseEnter(object sender, MouseEventArgs e)
        {
            GR_Maximize.Background = XAppMethods.Color_White_f8f9ff();
        }

        private void GR_Maximize_MouseLeave(object sender, MouseEventArgs e)
        {
            GR_Maximize.Background = XElementHelper.GetBrushColor(Colors.Transparent);
        }

        private void GR_Maximize_Click(object sender, MouseButtonEventArgs e)
        {
            var getWindow = Window.GetWindow(this);
            var getBorder = XElementHelper.FindChild<Border>(getWindow);

            var duration = new Duration(TimeSpan.FromMilliseconds(200));
            if (IsMaximized)
            {
                SmoothMinimize();

                //Border
                getBorder.CornerRadius = new CornerRadius(5);
                BR_Header.CornerRadius = new CornerRadius(5, 5, 0, 0);
                BR_Close.CornerRadius = new CornerRadius(0, 5, 0, 0);
            }
            else
            {
                SmoothMaximize();

                //Border
                getBorder.CornerRadius = new CornerRadius(0);
                BR_Header.CornerRadius = new CornerRadius(0);
                BR_Close.CornerRadius = new CornerRadius(0);
            }
        }

        private void SmoothMaximize()
        {
            var getWindow = Window.GetWindow(this);
            _NormalBounds = new Rect(getWindow.Left, getWindow.Top, getWindow.Width, getWindow.Height);
            var target = SystemParameters.WorkArea;
            var duration = TimeSpan.FromMilliseconds(150);

            AnimateWindow(_NormalBounds, target, duration, () =>
            {
                getWindow.WindowState = WindowState.Maximized;
                IsMaximized = true;
            });
        }

        private void SmoothMinimize()
        {
            var getWindow = Window.GetWindow(this);
            var from = new Rect(getWindow.Left, getWindow.Top, getWindow.Width, getWindow.Height);
            var to = _NormalBounds;
            var duration = TimeSpan.FromMilliseconds(150);

            AnimateWindow(from, to, duration, () =>
            {
                getWindow.WindowState = WindowState.Normal;
                getWindow.Left = _NormalBounds.Left;
                getWindow.Top = _NormalBounds.Top;
                getWindow.Width = _NormalBounds.Width;
                getWindow.Height = _NormalBounds.Height;
                IsMaximized = false;
            });
        }

        private void AnimateWindow(Rect from, Rect to, TimeSpan duration, Action onComplete)
        {
            var getWindow = Window.GetWindow(this);

            // غیر فعال کردن تغییر سایز همزمان توسط کاربر
            //this.ResizeMode = ResizeMode.NoResize;

            var leftAnim = new DoubleAnimation(from.Left, to.Left, duration);
            var topAnim = new DoubleAnimation(from.Top, to.Top, duration);
            var widthAnim = new DoubleAnimation(from.Width, to.Width, duration);
            var heightAnim = new DoubleAnimation(from.Height, to.Height, duration);

            getWindow.BeginAnimation(Window.LeftProperty, leftAnim);
            getWindow.BeginAnimation(Window.TopProperty, topAnim);
            getWindow.BeginAnimation(Window.WidthProperty, widthAnim);
            getWindow.BeginAnimation(Window.HeightProperty, heightAnim);

            var timer = new DispatcherTimer { Interval = duration };
            timer.Tick += (s, e) =>
            {
                timer.Stop();

                // پاک کردن انیمیشن تا اندازه واقعی تنظیم شه
                getWindow.BeginAnimation(Window.LeftProperty, null);
                getWindow.BeginAnimation(Window.TopProperty, null);
                getWindow.BeginAnimation(Window.WidthProperty, null);
                getWindow.BeginAnimation(Window.HeightProperty, null);

                //this.ResizeMode = ResizeMode.CanResize;
                onComplete?.Invoke();
            };
            timer.Start();
        }

        private void UserControl_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                if (e.OriginalSource is DependencyObject source)
                {
                    var getWindow = Window.GetWindow(this);

                    var getOpenedPopup = XPopupManager.GetPopupOpened();
                    if (getOpenedPopup != null && getOpenedPopup.IsMouseOver)
                        return;

                    getWindow.DragMove();
                }
            }
        }

        private void GR_Buttons_Handle()
        {
            var ButtonWidth = 150;
            if (!_ShowMaximumButton.Value)
            {
                ButtonWidth = 100;
                GR_Col_Maximize.Width = new GridLength(0, GridUnitType.Pixel);
            }
            else
                GR_Col_Maximize.Width = new GridLength(1, GridUnitType.Star);
            GR_Col_Buttons.Width = new GridLength(ButtonWidth, GridUnitType.Pixel);
        }
    }
}
