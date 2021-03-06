﻿using System;
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
using System.Windows.Shapes;
using TVSeriesUserClientEntityFramework.Presenter;
using TVSeriesUserClientEntityFramework.View;

namespace TVSeriesUserClientEntityFramework
{
    /// <summary>
    /// Interaction logic for ExtendedInfoTVSerialWindow.xaml
    /// </summary>
    public partial class ExtendedInfoTVSerialWindow : Window, IViewExtendedInfoTVSerialWindow
    {
        private IPresenterExtendedInfoTVSerialWindow _presenter;

        public ExtendedInfoTVSerialWindow(TVSeriesModel model, User currentUser, TVSeriesTable item)
        {
            InitializeComponent();
            _presenter = new PresenterExtendedInfoTVSerialWindow(currentUser, model, this, item);
        }

        public RatingUC RatingUserControl { get => PickerRating; set => PickerRating = value; }
        public Button Favourite { get => ButtonFavourite; set => ButtonFavourite = value; }
        public ExtendedInfoTVSerialWindow ExtendedInfoTvSerialWindowProperty { get => this; }

        private void SendCommentButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBoxTextComment.Text.Length > 0 && TextBoxTextComment.Text.Length < 1000)
            {
                _presenter.SendComment(TextBoxTextComment.Text);
                TextBoxTextComment.Text = "";
            }
        }

        private void ListViewComments_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _presenter.ListViewMouseDoubleClick((Comment)ListViewComments.SelectedItem);
        }

        private void ButtonFavourite_OnMouseEnter(object sender, MouseEventArgs e)
        {
            _presenter.ListViewMouseEnter();
        }

        private void ButtonFavourite_OnMouseLeave(object sender, MouseEventArgs e)
        {
            _presenter.ListViewMouseLeave();
        }

        private void ButtonFavourite_OnClick(object sender, RoutedEventArgs e)
        {
            _presenter.FavouriteButtonClick();
        }
    }
}
