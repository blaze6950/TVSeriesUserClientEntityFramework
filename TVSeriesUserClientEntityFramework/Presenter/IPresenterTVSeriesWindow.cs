using System;

namespace TVSeriesUserClientEntityFramework.Presenter
{
    public interface IPresenterTVSeriesWindow
    {
        void LoadList();
        void ButtonFind_Click();
        void ComboBoxFind_TextInput(String findText);
        void ListViewMouseDoubleClick(TVSeriesTable item);
    }
}