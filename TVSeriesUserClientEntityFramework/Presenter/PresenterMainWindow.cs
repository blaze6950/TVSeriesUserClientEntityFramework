﻿using System;
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
                    //create new window...
                    MessageBox.Show("Load data...", "Please wait!", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
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
            throw new NotImplementedException();
        }

        private bool CheckLogin()
        {
            var user = _model.Users.DefaultIfEmpty(null).SingleOrDefault(u => u.Email.Equals(_view.Email.Text));
            if (user == null)
            {
                return false;
            }
            if (user.Password.Equals(_view.Password.Password))
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