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
        public ListView FavouriteListTvSeries { get => FavouriteListTvSeries; set => FavouriteListTvSeries = value; }
    }
}

