using System.Windows;

namespace Models
{
    public static class FocusHelper
    {
        public static readonly DependencyProperty NextControlProperty =
            DependencyProperty.RegisterAttached(
                "NextControl",
                typeof(UIElement),
                typeof(FocusHelper),
                new PropertyMetadata(null));

        public static void SetNextControl(UIElement element, UIElement value)
        {
            element.SetValue(NextControlProperty, value);
        }

        public static UIElement GetNextControl(UIElement element)
        {
            return (UIElement)element.GetValue(NextControlProperty);
        }
    }
}
