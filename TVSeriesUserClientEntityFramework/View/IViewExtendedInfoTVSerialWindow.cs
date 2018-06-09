using System.Windows.Controls;

namespace TVSeriesUserClientEntityFramework.View
{
    public interface IViewExtendedInfoTVSerialWindow
    {
        RatingUC RatingUserControl { get; set; }
        Button Favourite { get; set; }
        ExtendedInfoTVSerialWindow ExtendedInfoTvSerialWindowProperty { get; }
    }
}