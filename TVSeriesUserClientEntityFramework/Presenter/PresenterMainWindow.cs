using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TVSeriesUserClientEntityFramework.View;

namespace TVSeriesUserClientEntityFramework.Presenter
{
    class PresenterMainWindow : IPresenterMainWindow
    {
        private IViewMainWindow _view;
        private TVSeriesModel _model;
        private bool _isLogin = true;
        private User _currentUser;
        public PresenterMainWindow(IViewMainWindow view)
        {
            _view = view;
            _model = new TVSeriesModel();
        }

        public void LoginClick()
        {
            if (_isLogin)
            {
                if (CheckLogin())
                {
                    MessageBox.Show("Load data...", "Please wait!", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    TVSeriesWindow tvSeriesWindow = new TVSeriesWindow(_model, _currentUser);
                    tvSeriesWindow.Show();
                    _view.MainWindowLogin.Close();
                }
                else
                {
                    MessageBox.Show("Please enter correct email or password or restore password or register!", "Ooops...", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else
            {
                CreateNewUser();
            }
        }

        public void NotYetRegisteredClick()
        {
            if (_isLogin)
            {
                SwapToRegistrationWindow();
            }
            else
            {
                SwapToLoginWindow();
            }
        }

        public void ForgetPasswordClick()
        {
            //add code for restore password...
            MessageBox.Show("Unfortunately, this function is not available ... We are working on it! We apologize for the inconvenience!", "Sorry...", MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
        }

        private bool CheckLogin()
        {
            _currentUser = _model.Users.DefaultIfEmpty(null).SingleOrDefault(u => u.Email.Equals(_view.Email.Text));
            if (_currentUser == null)
            {
                return false;
            }
            if (_currentUser.Password.Equals(_view.Password.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateNewUser()
        {
            if (!CheckLogin())
            {
                var newUser = new User();
                newUser.Email = _view.Email.Text;
                newUser.Password = _view.Password.Password;
                newUser.Id_Category = 1;
                try
                {
                    _model.Users.Add(newUser);
                    _model.SaveChanges();
                    MessageBox.Show("You have successfully registered! Now you can enter!", "Great!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    SwapToLoginWindow();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ooops...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("This email is already registered! Try restore password!", "Ooops...", MessageBoxButton.OK, MessageBoxImage.Error);
                SwapToLoginWindow();
            }
        }

        private void SwapToRegistrationWindow()
        {
            _isLogin = false;
            _view.MainWindowLogin.Title = "Registration";
            _view.Login.Content = "Register";
            _view.NotYetRegistered.Text = "Already registered?";
            _view.ForgotPassword.Visibility = Visibility.Collapsed;
        }

        private void SwapToLoginWindow()
        {
            _isLogin = true;
            _view.MainWindowLogin.Title = "Login";
            _view.Login.Content = "Login";
            _view.NotYetRegistered.Text = "Not yet registered?";
            _view.ForgotPassword.Visibility = Visibility.Visible;
        }
    }
}
