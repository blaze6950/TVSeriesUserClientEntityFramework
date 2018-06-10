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
using System.Windows.Shapes;
using TVSeriesUserClientEntityFramework.Presenter;
using TVSeriesUserClientEntityFramework.View;

namespace TVSeriesUserClientEntityFramework
{
    /// <summary>
    /// Interaction logic for TVSeriesWindow.xaml
    /// </summary>
    public partial class TVSeriesWindow : Window, ITVSeriesWindow
    {
        private IPresenterTVSeriesWindow _presenter;
        public TVSeriesWindow(TVSeriesModel model, User currentUser)
        {
            InitializeComponent();
            _presenter = new PresenterTVSeriesWindow(this, model, currentUser);
        }

        private void AllComboBoxFind_TextInput(object sender, KeyEventArgs e)
        {
            if (ComboBoxFind.Text.Length > 0)
            {
                _presenter.ComboBoxFind_TextInput(ComboBoxFind.Text);
            }
        }

        private void AllButtonFind_Click(object sender, RoutedEventArgs e)
        {
            _presenter.ButtonFind_Click();
        }

        private void AllMyListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _presenter.ListViewMouseDoubleClick((TVSeriesTable)ListTvSeries.SelectedItem);
        }

        public ComboBox AllComboBoxFind { get => ComboBoxFind; set => ComboBoxFind = value; }
        public ListView AllListTvSeries { get => ListTvSeries; set => ListTvSeries = value; }
        public ComboBox FavouriteComboBoxFind { get => FavComboBoxFind; set => FavComboBoxFind = value; }
        public ListView FavouriteListTvSeries { get => FavListTvSeries; set => FavListTvSeries = value; }
        public TVSeriesWindow TvSeriesWindow { get => this; }

        private void CheckBoxFilter_StateChanged(object sender, RoutedEventArgs e)
        {
            _presenter.LoadList();
        }

        private void MenuItemFilters_Click(object sender, RoutedEventArgs e)
        {
            bool res = ((MenuItem) sender).IsChecked;
            if (res)
            {
                FiltersPanel.Visibility = Visibility.Visible;
                FiltersPanelFav.Visibility = Visibility.Visible;
            }
            else
            {
                FiltersPanel.Visibility = Visibility.Collapsed;
                FiltersPanelFav.Visibility = Visibility.Collapsed;
            }
            MenuItemFilters.IsChecked = res;
            MenuItemFiltersFav.IsChecked = res;
        }

        private void TextBoxStartYear_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(TextBoxStartYear.Text, out var startYear))
            {
                if (Int32.TryParse(TextBoxEndYear.Text, out var endYear))
                {
                    if (startYear > endYear)
                    {
                        endYear = startYear;
                        TextBoxEndYear.Text = endYear.ToString();
                    }
                }
                else
                {
                    endYear = startYear;
                    TextBoxEndYear.Text = endYear.ToString();
                }
                if (endYear > 1800 && startYear > 1800)
                {
                    _presenter.Year_Changed(endYear, startYear);
                }
            }
        }

        private void TextBoxEndYear_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(TextBoxEndYear.Text, out var endYear))
            {
                if (Int32.TryParse(TextBoxStartYear.Text, out var startYear))
                {
                    if (startYear > endYear)
                    {
                        startYear = endYear;
                        TextBoxStartYear.Text = startYear.ToString();
                    }
                }
                else
                {
                    startYear = endYear;
                    TextBoxStartYear.Text = startYear.ToString();
                }
                if (endYear > 1800 && startYear > 1800)
                {
                    _presenter.Year_Changed(startYear, endYear);
                }
            }
        }

        private void ButtonResetYearFilter_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxStartYear.Text = "";
            TextBoxEndYear.Text = "";
            _presenter.ResetYearFilter();
        }

        private void CheckBoxFilterFav_StateChanged(object sender, RoutedEventArgs e)
        {
            _presenter.LoadListFav();
        }

        private void TextBoxStartYearFav_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(TextBoxStartYearFav.Text, out var startYear))
            {
                if (Int32.TryParse(TextBoxEndYearFav.Text, out var endYear))
                {
                    if (startYear > endYear)
                    {
                        endYear = startYear;
                        TextBoxEndYearFav.Text = endYear.ToString();
                    }
                }
                else
                {
                    endYear = startYear;
                    TextBoxEndYearFav.Text = endYear.ToString();
                }
                if (endYear > 1800 && startYear > 1800)
                {
                    _presenter.YearFav_Changed(endYear, startYear);
                }
            }
        }

        private void TextBoxEndYearFav_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(TextBoxEndYearFav.Text, out var endYear))
            {
                if (Int32.TryParse(TextBoxStartYearFav.Text, out var startYear))
                {
                    if (startYear > endYear)
                    {
                        startYear = endYear;
                        TextBoxStartYearFav.Text = startYear.ToString();
                    }
                }
                else
                {
                    startYear = endYear;
                    TextBoxStartYearFav.Text = startYear.ToString();
                }
                if (endYear > 1800 && startYear > 1800)
                {
                    _presenter.YearFav_Changed(startYear, endYear);
                }
            }
        }

        private void FavComboBoxFind_TextInput(object sender, KeyEventArgs e)
        {
            if (FavComboBoxFind.Text.Length > 0)
            {
                _presenter.FavComboBoxFind_TextInput(FavComboBoxFind.Text);
            }
        }

        private void ButtonFindFav_Click(object sender, RoutedEventArgs e)
        {
            _presenter.ButtonFavFind_Click();
        }

        private void FavMyListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _presenter.FavListViewMouseDoubleClick((TVSeriesTable)FavListTvSeries.SelectedItem);
        }
    }
}

