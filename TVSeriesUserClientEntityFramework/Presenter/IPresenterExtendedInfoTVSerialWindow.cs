namespace TVSeriesUserClientEntityFramework.Presenter
{
    public interface IPresenterExtendedInfoTVSerialWindow
    {
        void SendComment(string text);
        void ListViewMouseDoubleClick(Comment selectedComment);
        void FavouriteButtonClick();
        void ListViewMouseEnter();
        void ListViewMouseLeave();
    }
}