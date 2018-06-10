using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TVSeriesUserClientEntityFramework.View;

namespace TVSeriesUserClientEntityFramework.Presenter
{
    public class PresenterTVSeriesWindow : IPresenterTVSeriesWindow
    {
        private TVSeriesModel _model;
        private ITVSeriesWindow _view;
        private User _currentUser;
        private int _startYear = 0, _endYear = 2050;
        private int _startYearFav = 0, _endYearFav = 2050;

        public PresenterTVSeriesWindow(ITVSeriesWindow view, TVSeriesModel model, User currentUser)
        {
            _model = model;
            _view = view;
            _currentUser = currentUser;
            InitializeLists();
        }

        public void InitializeLists()
        {
            _view.AllListTvSeries.ItemsSource = (from tv in _model.TVSeriesTables select tv).ToList();
            _view.FavouriteListTvSeries.ItemsSource = (from favTv in _currentUser.TVSeriesTables select favTv).DefaultIfEmpty(null).ToList();
            _view.TvSeriesWindow.ListBoxGenres.ItemsSource = (from g in _model.Genres select g).DefaultIfEmpty(null).ToList();
            _view.TvSeriesWindow.ListBoxChannels.ItemsSource =
                (from c in _model.Channels select c).DefaultIfEmpty(null).ToList();
            _view.TvSeriesWindow.ListBoxGenresFav.ItemsSource = (from g in _model.Genres select g).DefaultIfEmpty(null).ToList();
            _view.TvSeriesWindow.ListBoxChannelsFav.ItemsSource =
                (from c in _model.Channels select c).DefaultIfEmpty(null).ToList();
        }

        public void LoadList()
        {
            List<TVSeriesTable> newList = null;

            List<TVSeriesTable> genreList = null;
            foreach (Genre genre in _view.TvSeriesWindow.ListBoxGenres.ItemsSource)
            {
                if (genre.IsChecked)
                {
                    if (genreList == null)
                    {
                        genreList =
                            (from tv in _model.TVSeriesTables from g in tv.Genres where g.Id == genre.Id select tv)
                            .ToList();
                    }
                    else
                    {
                        genreList.AddRange((from tv in _model.TVSeriesTables from g in tv.Genres where g.Id == genre.Id select tv).ToList());
                    }
                }
            }

            List<TVSeriesTable> channelList = null;
            foreach (Channel channel in _view.TvSeriesWindow.ListBoxChannels.ItemsSource)
            {
                if (channel.IsChecked)
                {
                    if (channelList == null)
                    {
                        channelList = (from tv in _model.TVSeriesTables where tv.Channel.Id == channel.Id select tv).ToList();
                    }
                    else
                    {
                        channelList.AddRange((from tv in _model.TVSeriesTables where tv.Channel.Id == channel.Id select tv).ToList());
                    }
                }
            }

            if (genreList != null && channelList != null)
            {
                newList = genreList.Intersect(channelList).ToList();
            }
            else if (genreList == null && channelList == null)
            {
                newList = (from tv in _model.TVSeriesTables select tv).ToList();

            }else if (genreList != null && channelList == null)
            {
                newList = genreList;
            }
            else
            {
                newList = channelList;
            }

            if (newList != null)
            {
                newList = (from tv in newList
                           where tv.YearOfIssue >= _startYear && tv.YearOfIssue <= _endYear
                           select tv).ToList();
            }

            if (newList != null)
            {
                if (_view.AllComboBoxFind.Text.Length > 0)
                {
                    newList = (from tv in newList
                               where tv.Name.Contains(_view.AllComboBoxFind.Text)
                               select tv).ToList();
                }
            }

            _view.AllListTvSeries.ItemsSource = newList;
        }

        public void ButtonFind_Click()
        {
            LoadList();
        }

        public void ComboBoxFind_TextInput(string findText)
        {
            _view.AllComboBoxFind.ItemsSource = (from tv in _model.TVSeriesTables where tv.Name.Contains(_view.AllComboBoxFind.Text) select tv.Name).ToList();
            _view.AllComboBoxFind.IsDropDownOpen = true;
        }
        
        public void ListViewMouseDoubleClick(TVSeriesTable item)
        {
            var extendedInfo = new ExtendedInfoTVSerialWindow(_model, _currentUser, item);
            extendedInfo.ShowDialog();
            _model.SaveChanges();
            InitializeLists();
        }

        public void Year_Changed(int endYear, int startYear)
        {
            _startYear = startYear;
            _endYear = endYear;
            LoadList();
        }

        public void ResetYearFilter()
        {
            _startYear = 0;
            _endYear = 2050;
            LoadList();
        }

        public void LoadListFav()
        {
            List<TVSeriesTable> newList = null;

            List<TVSeriesTable> genreList = null;
            foreach (Genre genre in _view.TvSeriesWindow.ListBoxGenresFav.ItemsSource)
            {
                if (genre.IsChecked)
                {
                    if (genreList == null)
                    {
                        genreList =
                            (from tv in _currentUser.TVSeriesTables from g in tv.Genres where g.Id == genre.Id select tv)
                            .ToList();
                    }
                    else
                    {
                        genreList.AddRange((from tv in _currentUser.TVSeriesTables from g in tv.Genres where g.Id == genre.Id select tv).ToList());
                    }
                }
            }

            List<TVSeriesTable> channelList = null;
            foreach (Channel channel in _view.TvSeriesWindow.ListBoxChannelsFav.ItemsSource)
            {
                if (channel.IsChecked)
                {
                    if (channelList == null)
                    {
                        channelList = (from tv in _currentUser.TVSeriesTables where tv.Channel.Id == channel.Id select tv).ToList();
                    }
                    else
                    {
                        channelList.AddRange((from tv in _currentUser.TVSeriesTables where tv.Channel.Id == channel.Id select tv).ToList());
                    }
                }
            }

            if (genreList != null && channelList != null)
            {
                newList = genreList.Intersect(channelList).ToList();
            }
            else if (genreList == null && channelList == null)
            {
                newList = (from tv in _currentUser.TVSeriesTables select tv).ToList();

            }
            else if (genreList != null && channelList == null)
            {
                newList = genreList;
            }
            else
            {
                newList = channelList;
            }

            if (newList != null)
            {
                newList = (from tv in newList
                           where tv.YearOfIssue >= _startYear && tv.YearOfIssue <= _endYear
                           select tv).ToList();
            }

            if (newList != null)
            {
                if (_view.AllComboBoxFind.Text.Length > 0)
                {
                    newList = (from tv in newList
                               where tv.Name.Contains(_view.AllComboBoxFind.Text)
                               select tv).ToList();
                }
            }

            _view.FavouriteListTvSeries.ItemsSource = newList;
        }

        public void YearFav_Changed(int startYear, int endYear)
        {
            _startYearFav = startYear;
            _endYearFav = endYear;
            LoadList();
        }

        public void FavComboBoxFind_TextInput(string findText)
        {
            _view.FavouriteComboBoxFind.ItemsSource = (from tv in _currentUser.TVSeriesTables where tv.Name.Contains(_view.FavouriteComboBoxFind.Text) select tv.Name).ToList();
            _view.FavouriteComboBoxFind.IsDropDownOpen = true;
        }

        public void ButtonFavFind_Click()
        {
            LoadListFav();
        }

        public void FavListViewMouseDoubleClick(TVSeriesTable item)
        {
            var extendedInfo = new ExtendedInfoTVSerialWindow(_model, _currentUser, item);
            extendedInfo.ShowDialog();
            _model.SaveChanges();
            InitializeLists();
        }
    }
}