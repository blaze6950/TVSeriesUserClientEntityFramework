using System;

namespace TVSeriesUserClientEntityFramework.Presenter
{
    public interface IPresenterTVSeriesWindow
    {
        void LoadList();
        void ButtonFind_Click();
        void ComboBoxFind_TextInput(String findText);
        void ListViewMouseDoubleClick(TVSeriesTable item);
        void Year_Changed(int startYear, int endYear);
        void ResetYearFilter();
        void LoadListFav();
        void YearFav_Changed(int startYear, int endYear);
        void FavComboBoxFind_TextInput(String findText);
        void ButtonFavFind_Click();
        void FavListViewMouseDoubleClick(TVSeriesTable item);
    }
}