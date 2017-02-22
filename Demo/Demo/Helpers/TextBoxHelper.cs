using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Demo.Helpers
{
    public static class TextBoxHelper
    {
        public static readonly DependencyProperty HintProperty = DependencyProperty.RegisterAttached("Hint", typeof(string), typeof(TextBoxHelper), new PropertyMetadata(default(string), OnHintChanged));

        private const string FloatingStateName = "Floating";

        private const string NotFloatingStateName = "NotFloating";

        public static string GetHint(Control obj)
        {
            return (string)obj.GetValue(HintProperty);
        }

        public static void SetHint(Control obj, string value)
        {
            obj.SetValue(HintProperty, value);
        }

        private static void AutoSuggestBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var autoSuggestBox = (AutoSuggestBox)sender;
            if (string.IsNullOrEmpty(autoSuggestBox.Text))
            {
                VisualStateManager.GoToState(autoSuggestBox, NotFloatingStateName, true);
            }
        }

        private static void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(sender.Text) && sender.FocusState == FocusState.Unfocused)
            {
                VisualStateManager.GoToState(sender, NotFloatingStateName, true);
            }
        }

        private static void Control_GotFocus(object sender, RoutedEventArgs e)
        {
            var control = (Control)sender;
            VisualStateManager.GoToState(control, FloatingStateName, true);
        }

        private static void OnHintChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (Control)d;

            var textBox = d as TextBox;
            var passwordBox = d as PasswordBox;
            var autoSuggestBox = d as AutoSuggestBox;
            if (textBox == null && passwordBox == null && autoSuggestBox == null)
            {
                throw new NotSupportedException();
            }

            obj.GotFocus -= Control_GotFocus;
            obj.GotFocus += Control_GotFocus;
            if (textBox != null)
            {
                textBox.LostFocus -= TextBox_LostFocus;
                textBox.LostFocus += TextBox_LostFocus;
                textBox.TextChanged -= TextBox_TextChanged;
                textBox.TextChanged += TextBox_TextChanged;
            }
            if (passwordBox != null)
            {
                passwordBox.LostFocus -= PasswordBox_LostFocus;
                passwordBox.LostFocus += PasswordBox_LostFocus;
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
            if (autoSuggestBox != null)
            {
                autoSuggestBox.LostFocus -= AutoSuggestBox_LostFocus;
                autoSuggestBox.LostFocus += AutoSuggestBox_LostFocus;
                autoSuggestBox.TextChanged -= AutoSuggestBox_TextChanged;
                autoSuggestBox.TextChanged += AutoSuggestBox_TextChanged;
            }
        }

        private static void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                VisualStateManager.GoToState(passwordBox, NotFloatingStateName, true);
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            if (string.IsNullOrEmpty(passwordBox.Password) && passwordBox.FocusState == FocusState.Unfocused)
            {
                VisualStateManager.GoToState(passwordBox, NotFloatingStateName, true);
            }
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                VisualStateManager.GoToState(textBox, NotFloatingStateName, true);
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text) && textBox.FocusState == FocusState.Unfocused)
            {
                VisualStateManager.GoToState(textBox, NotFloatingStateName, true);
            }
        }
    }
}