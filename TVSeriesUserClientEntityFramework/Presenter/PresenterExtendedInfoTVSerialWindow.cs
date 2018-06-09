using System;
using System.Linq;
using System.Windows.Media.Imaging;
using TVSeriesUserClientEntityFramework.View;

namespace TVSeriesUserClientEntityFramework.Presenter
{
    public class PresenterExtendedInfoTVSerialWindow : IPresenterExtendedInfoTVSerialWindow
    {
        private TVSeriesModel _model;
        private IViewExtendedInfoTVSerialWindow _view;
        private User _currentUser;
        private TVSeriesTable _currenTvSeriesTable;

        public PresenterExtendedInfoTVSerialWindow(User currentUser, TVSeriesModel model, IViewExtendedInfoTVSerialWindow view, TVSeriesTable currenTvSeriesTable)
        {
            _currentUser = currentUser;
            _model = model;
            _view = view;
            _currenTvSeriesTable = currenTvSeriesTable;

            _view.ExtendedInfoTvSerialWindowProperty.PickerRating.TvSeriesTable = _currenTvSeriesTable;
            _view.ExtendedInfoTvSerialWindowProperty.PickerRating.CurrentUser = _currentUser;

            LoadData();
        }

        private void LoadData()
        {
            _view.ExtendedInfoTvSerialWindowProperty.NameTB.Text = _currenTvSeriesTable.Name;
            _view.ExtendedInfoTvSerialWindowProperty.DescriptionTB.Text = _currenTvSeriesTable.Desription;
            _view.ExtendedInfoTvSerialWindowProperty.ChannelTB.Text = _currenTvSeriesTable.Channel.Name;
            _view.ExtendedInfoTvSerialWindowProperty.Image.Source = new BitmapImage(new Uri(_currenTvSeriesTable.Image));
            _view.ExtendedInfoTvSerialWindowProperty.GenreTB.ItemsSource = _currenTvSeriesTable.Genres.ToList();
            _view.ExtendedInfoTvSerialWindowProperty.SeasonsTB.Text = _currenTvSeriesTable.Seasons.ToString();
            _view.ExtendedInfoTvSerialWindowProperty.YearTB.Text = _currenTvSeriesTable.YearOfIssue.ToString();
        }
    }
}