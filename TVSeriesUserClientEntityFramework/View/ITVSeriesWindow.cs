using System.Windows.Controls;

namespace TVSeriesUserClientEntityFramework.View
{
    public interface ITVSeriesWindow
    {
        ComboBox AllComboBoxFind { get; set; }
        ListView AllListTvSeries { get; set; }
        ComboBox FavouriteComboBoxFind { get; set; }
        ListView FavouriteListTvSeries { get; set; }
    }
}