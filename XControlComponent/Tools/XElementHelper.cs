using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace XControlComponents.Tools
{
    public static class XElementHelper
    {
        public static void SetImageSource(this Image ImageElement, string ImageUrl)
        {
            ImageElement.Source = new BitmapImage(new Uri(ImageUrl, UriKind.Relative));
        }

        public static Color GetColorByHex(this string HexColor)
        {
            return (Color)ColorConverter.ConvertFromString(HexColor);
        }

        public static Brush GetBrushColor(this Color color)
        {
            return new SolidColorBrush(color);
        }

        public static Brush GetBrushColorByHex(this string HexColor)
        {
            return new SolidColorBrush(HexColor.GetColorByHex());
        }

        public static string GetHexByColor(this Color Color)
        {
            return $"#{Color.R:X2}{Color.G:X2}{Color.B:X2}";
        }

        public static Color GetColorByBrush(this Brush BrushColor)
        {
            return ((SolidColorBrush)BrushColor).Color;
        }

        public static void _UnregisterName(this FrameworkElement element, string name)
        {
            if (element.FindName(name) != null)
            {
                element.UnregisterName(name);
            }
        }

        public static T? _FindName<T>(this FrameworkElement element, string name) where T : class
        {
            var result = element.FindName(name);

            if (result is T typedResult)
                return typedResult;

            return null;
        }


        public static T FindChild<T>(this DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (parent is Window)
                {
                    var getWindowChild = FindChild<T>(child);
                    if (getWindowChild != null)
                        return getWindowChild;
                }

                if (child is T t)
                    return t;

                var result = FindChild<T>(child);
                if (result != null)
                    return result;
            }

            return null;
        }

        public static T FindChildByTag<T>(this DependencyObject parent, string Tag) where T : DependencyObject
        {
            if (parent == null) return null;

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T t)
                {
                    var tag = (child as FrameworkElement)?.Tag;
                    if (!tag.IsNullOrEmpty() && tag.ToString() == Tag)
                        return t;
                }

                var result = FindChild<T>(child);
            }

            return null;
        }

        public static T FindChildByName<T>(this DependencyObject parent, string Name) where T : DependencyObject
        {
            if (parent == null) return null;

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T t)
                {
                    var getName = (child as FrameworkElement)?.Name;
                    if (!Name.IsNullOrEmpty() && Name.ToString() == Name)
                        return t;
                }

                var result = FindChild<T>(child);
            }

            return null;
        }

        public static List<DependencyObject> FindChilds(this DependencyObject parent)
        {
            var parents = new List<DependencyObject>();
            Find(parent);

            List<DependencyObject> Find(DependencyObject parentElement)
            {
                int count = VisualTreeHelper.GetChildrenCount(parentElement);
                for (int i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(parentElement, i);
                    parents.Add(child);

                    var result = Find(child);
                }

                return parents;
            }

            return parents;
        }

        public static List<DependencyObject> FindChilds<T>(this DependencyObject parent)
        {
            var parents = new List<DependencyObject>();

            Find<T>(parent);

            List<DependencyObject> Find<T>(DependencyObject parentElement)
            {
                int count = VisualTreeHelper.GetChildrenCount(parentElement);
                for (int i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(parentElement, i);

                    if (child is T t)
                        parents.Add(child);

                    var result = Find<T>(child);
                }

                return parents;
            }

            return parents;
        }

        public static T FindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                if (parent is T parentAsT)
                    return parentAsT;

                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

        public static T FindParentByTag<T>(this DependencyObject child, string Tag) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                if (parent is T parentAsT)
                {
                    var tag = (parent as FrameworkElement)?.Tag;
                    if (!tag.IsNullOrEmpty() && tag.ToString() == Tag)
                        return parentAsT;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        public static T FindParentByName<T>(this DependencyObject child, string Name) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                if (parent is T parentAsT)
                {
                    var getName = (parent as FrameworkElement)?.Name;
                    if (!getName.IsNullOrEmpty() && getName.ToString() == Name)
                        return parentAsT;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        public static List<DependencyObject> FindParents(this DependencyObject child)
        {
            var parents = new List<DependencyObject>();
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                parents.Add(parent);
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parents;
        }

        public static List<DependencyObject> FindParents<T>(this DependencyObject child)
        {
            var parents = new List<DependencyObject>();
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                if (parent is T t)
                    parents.Add(parent);
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parents;
        }

        public static bool IsChildOf(this DependencyObject source, DependencyObject potentialParent)
        {
            while (source != null)
            {
                if (source == potentialParent)
                    return true;

                source = VisualTreeHelper.GetParent(source);
            }
            return false;
        }

        public static string GetTag(this DependencyObject source)
        {
            if (source is FrameworkElement fe && fe.Tag != null)
            {
                return fe.Tag.FillStringSafe();
            }

            return string.Empty;
        }
    }
}
