using System;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace Demo.Animations
{
    public static class LayoutAnimation
    {
        public static readonly DependencyProperty DurationProperty = DependencyProperty.RegisterAttached(DurationPropertyName, typeof(Duration), typeof(LayoutAnimation), new PropertyMetadata(default(Duration), OnDurationChanged));

        private const string DurationPropertyName = "Duration";

        public static Duration GetDuration(UIElement obj)
        {
            return (Duration)obj.GetValue(DurationProperty);
        }

        public static void SetDuration(UIElement obj, Duration value)
        {
            obj.SetValue(DurationProperty, value);
        }

        private static void OnDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (UIElement)d;
            var value = (Duration)e.NewValue;

            if (value.HasTimeSpan == false)
            {
                throw new ArgumentException("TODO", DurationPropertyName);
            }

            var elementVisual = ElementCompositionPreview.GetElementVisual(obj);
            if (value.TimeSpan <= TimeSpan.Zero)
            {
                elementVisual.ImplicitAnimations = null;
            }
            else
            {
                var compositor = elementVisual.Compositor;
                var offsetAnimation = compositor.CreateVector3KeyFrameAnimation();
                offsetAnimation.Target = nameof(Visual.Offset);
                offsetAnimation.InsertExpressionKeyFrame(1f, "this.FinalValue");
                offsetAnimation.Duration = value.TimeSpan;
                var implicitAnimations = compositor.CreateImplicitAnimationCollection();
                implicitAnimations[nameof(Visual.Offset)] = offsetAnimation;
                elementVisual.ImplicitAnimations = implicitAnimations;
            }
        }
    }
}