using System.Windows;
using System.Windows.Controls;

namespace TVSeriesUserClientEntityFramework.View
{
    public interface IViewMainWindow
    {
        TextBox Email { get; set; }
        PasswordBox Password { get; set; }
        Button Login { get; set; }
        MainWindow MainWindowLogin { get;} 
        TextBlock NotYetRegistered { get; set; }
        TextBlock ForgotPassword { get; set; }
    }
}