using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TVSeriesUserClientEntityFramework.Presenter;
using TVSeriesUserClientEntityFramework.View;

namespace TVSeriesUserClientEntityFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewMainWindow
    {
        private IPresenterMainWindow _presenter;
        public MainWindow()
        {
            InitializeComponent();
            _presenter = new PresenterMainWindow(this);
        }

        private void TextBlock_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var textBlock = (TextBlock)sender;
            textBlock.TextDecorations = System.Windows.TextDecorations.Underline;
        }

        private void TextBlock_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var textBlock = (TextBlock)sender;
            textBlock.TextDecorations = null;
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBoxEmail.Text.Length > 0 && PasswordBox.Password.Length > 0)
            {
                _presenter.LoginClick();
            }
            else
            {
                MessageBox.Show("Please enter email and password!", "Ooops...", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void TextBlockNotYetRegistered_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _presenter.NotYetRegisteredClick();
        }

        private void TextBlockForgetPassword_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _presenter.ForgetPasswordClick();
        }

        public TextBox Email { get => TextBoxEmail; set => TextBoxEmail = value; }
        public PasswordBox Password { get => PasswordBox; set => PasswordBox = value; }
        public Button Login { get => ButtonLogin; set => ButtonLogin = value; }
        public MainWindow MainWindowLogin { get => this;}
        public TextBlock NotYetRegistered { get => TextBlockNotYetRegistered; set => TextBlockNotYetRegistered = value; }
        public TextBlock ForgotPassword { get => TextBlockForgetPassword; set => TextBlockForgetPassword = value; }
    }
}
