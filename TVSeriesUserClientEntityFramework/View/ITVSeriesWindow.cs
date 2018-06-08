using System.Windows.Controls;

namespace TVSeriesUserClientEntityFramework.View
{
    public interface ITVSeriesWindow
    {
        ComboBox AllComboBoxFind { get; set; }
        ListView AllListTvSeries { get; set; }
    }
}