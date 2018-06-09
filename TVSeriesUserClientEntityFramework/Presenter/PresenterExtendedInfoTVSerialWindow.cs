using TVSeriesUserClientEntityFramework.View;

namespace TVSeriesUserClientEntityFramework.Presenter
{
    public class PresenterExtendedInfoTVSerialWindow : IPresenterExtendedInfoTVSerialWindow
    {
        private TVSeriesModel _model;
        private IViewExtendedInfoTVSerialWindow _View;
        private User _currentUser;
        private TVSeriesTable _currenTvSeriesTable;

        public PresenterExtendedInfoTVSerialWindow(User currentUser, TVSeriesModel model, IViewExtendedInfoTVSerialWindow view, TVSeriesTable currenTvSeriesTable)
        {
            _currentUser = currentUser;
            _model = model;
            _View = view;
            _currenTvSeriesTable = currenTvSeriesTable;

            _View.ExtendedInfoTvSerialWindowProperty.PickerRating.TvSeriesTable = _currenTvSeriesTable;
            _View.ExtendedInfoTvSerialWindowProperty.PickerRating.CurrentUser = _currentUser;

            LoadData();
        }

        private void LoadData()
        {
            //load data from db of current TVSerial
        }
    }
}