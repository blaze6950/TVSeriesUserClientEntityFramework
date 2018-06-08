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
        public PresenterTVSeriesWindow(ITVSeriesWindow view, TVSeriesModel model, User currentUser)
        {
            _model = model;
            _view = view;
            _currentUser = currentUser;
            LoadList();
        }

        public void LoadList()
        {
            _view.AllListTvSeries.ItemsSource = (from tv in _model.TVSeriesTables select tv).ToList();
            _view.
        }

        public void ButtonFind_Click()
        {
            throw new System.NotImplementedException();
        }

        public void ComboBoxFind_TextInput(string findText)
        {
            throw new System.NotImplementedException();
        }

        public void ListViewMouseDoubleClick(TVSeriesTable item)
        {
            throw new System.NotImplementedException();
        }
    }
}