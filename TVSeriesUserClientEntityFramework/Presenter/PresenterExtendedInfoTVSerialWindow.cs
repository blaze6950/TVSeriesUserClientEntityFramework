using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TVSeriesUserClientEntityFramework.View;
using MessageBox = System.Windows.MessageBox;

namespace TVSeriesUserClientEntityFramework.Presenter
{
    public class PresenterExtendedInfoTVSerialWindow : IPresenterExtendedInfoTVSerialWindow
    {
        private TVSeriesModel _model;
        private IViewExtendedInfoTVSerialWindow _view;
        private User _currentUser;
        private TVSeriesTable _currenTvSeriesTable;
        private bool _isFavourite;

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
            _view.ExtendedInfoTvSerialWindowProperty.GenreTB.ItemsSource = (from g in _currenTvSeriesTable.Genres select g.Name).ToList();
            _view.ExtendedInfoTvSerialWindowProperty.SeasonsTB.Text = _currenTvSeriesTable.Seasons.ToString();
            _view.ExtendedInfoTvSerialWindowProperty.YearTB.Text = _currenTvSeriesTable.YearOfIssue.ToString();
            _view.ExtendedInfoTvSerialWindowProperty.ListViewComments.ItemsSource = _currenTvSeriesTable.Comments.ToList();

            if (_currentUser.TVSeriesTables.Contains(_currenTvSeriesTable))
            {
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Background = Brushes.ForestGreen;
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Content = "Favourite!";
                _isFavourite = true;
            }
            else
            {
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Background = Brushes.Gray;
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Content = "Add to favourite";
                _isFavourite = false;
            }
        }

        public void SendComment(string text)
        {
            _currenTvSeriesTable.Comments.Add(new Comment(){Id_TVSerial = _currenTvSeriesTable.Id, Id_User = _currentUser.id, TextComment = text, User = _currentUser, TVSeriesTable = _currenTvSeriesTable});
            _view.ExtendedInfoTvSerialWindowProperty.ListViewComments.ItemsSource = _currenTvSeriesTable.Comments.ToList();
        }

        public void ListViewMouseDoubleClick(Comment selectedComment)
        {
            var res = MessageBox.Show($"Do u want delete this comment: \n-----\"{selectedComment.TextComment}\" ?",
                "Are u sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                _currenTvSeriesTable.Comments.Remove(selectedComment);
            }
            _view.ExtendedInfoTvSerialWindowProperty.ListViewComments.ItemsSource = _currenTvSeriesTable.Comments.ToList();
            MessageBox.Show("Comment deleted successfully!", "Done!");
        }

        public void FavouriteButtonClick()
        {
            if (!_isFavourite)
            {
                _currentUser.TVSeriesTables.Add(_currenTvSeriesTable);
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Background = Brushes.ForestGreen;
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Content = "Favourite!";
                _isFavourite = true;
            }
            else
            {
                _currentUser.TVSeriesTables.Remove(_currenTvSeriesTable);
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Background = Brushes.Gray;
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Content = "Add to favourite";
                _isFavourite = false;
            }
        }

        public void ListViewMouseEnter()
        {
            if (_isFavourite)
            {
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Background = Brushes.DarkRed;
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Content = "Delete from favourites?";
            }
            else
            {
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Background = Brushes.Aquamarine;
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Content = "Add to favourite?";
            }
        }

        public void ListViewMouseLeave()
        {
            if (_isFavourite)
            {
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Background = Brushes.ForestGreen;
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Content = "Favourite!";
            }
            else
            {
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Background = Brushes.Gray;
                _view.ExtendedInfoTvSerialWindowProperty.ButtonFavourite.Content = "Add to favourite";
            }
        }
    }
}