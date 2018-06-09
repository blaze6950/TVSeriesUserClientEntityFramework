using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TVSeriesUserClientEntityFramework
{
    /// <summary>
    /// Interaction logic for RatingUC.xaml
    /// </summary>
    public partial class RatingUC : UserControl
    {
        int intRate = 0;
        int intCount = 1;
        int Rate = 0;
        private TVSeriesTable _tvSeriesTable;
        private User _currentUser;

        public RatingUC()
        {
            InitializeComponent();
            LoadImages();
            lblRating.Text = intRate.ToString();
        }

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                if (_tvSeriesTable != null && _currentUser != null)
                {
                    LoadRating();
                }
            }
        }

        public TVSeriesTable TvSeriesTable
        {
            get { return _tvSeriesTable; }
            set
            {
                _tvSeriesTable = value;
                if (_tvSeriesTable != null && _currentUser != null)
                {
                    LoadRating();
                }
            }
        }

        private void LoadRating()
        {
            var oldRating = TvSeriesTable.Ratings.DefaultIfEmpty(null).SingleOrDefault(r => r?.Id_User == CurrentUser.id);
            if (oldRating != null)
            {
                Rate = oldRating.Mark;
                intRate = Rate;
                SetRating();
                lblRating.Text = TvSeriesTable.AverageRating.ToString();
                SetImage(Rate, Visibility.Visible, Visibility.Hidden);
            }
        }

        private void LoadImages()
        {
            for (int i = 1; i <= 5; i++)
            {
                Image img = new Image();
                img.Name = "imgRate" + i;
                img.Stretch = Stretch.UniformToFill;
                img.Height = 25;
                img.Width = 25;
                img.Source = new BitmapImage(new Uri(@"\Images\MinusRate.png", UriKind.Relative));
                img.MouseEnter += imgRateMinus_MouseEnter;
                pnlMinus.Children.Add(img);

                Image img1 = new Image();
                img1.Name = "imgRate" + i + i;
                img1.Stretch = Stretch.UniformToFill;
                img1.Height = 25;
                img1.Width = 25;
                img1.Visibility = Visibility.Hidden;
                img1.Source = new BitmapImage(new Uri(@"\Images\PlusRate.png", UriKind.Relative));
                img1.MouseEnter += imgRatePlus_MouseEnter;
                img1.MouseLeave += imgRatePlus_MouseLeave;
                img1.MouseLeftButtonUp += imgRatePlus_MouseLeftButtonUp;
                pnlPlus.Children.Add(img1);
            }
        }

        private void imgRateMinus_MouseEnter(object sender, MouseEventArgs e)
        {
            GetData(sender, Visibility.Visible, Visibility.Hidden);
        }

        private void imgRatePlus_MouseEnter(object sender, MouseEventArgs e)
        {
            GetData(sender, Visibility.Visible, Visibility.Hidden);
        }

        private void imgRatePlus_MouseLeave(object sender, MouseEventArgs e)
        {
            GetData(sender, Visibility.Hidden, Visibility.Visible);
        }

        private void gdRating_MouseLeave(object sender, MouseEventArgs e)
        {
            SetImage(Rate, Visibility.Visible, Visibility.Hidden);
        }

        private void GetData(object sender, Visibility imgYellowVisibility, Visibility imgGrayVisibility)
        {
            GetRating(sender as Image);
            SetImage(intRate, imgYellowVisibility, imgGrayVisibility);
        }

        private void SetImage(int intRate, Visibility imgYellowVisibility, Visibility imgGrayVisibility)
        {
            foreach (Image imgItem in pnlPlus.Children.OfType<Image>())
            {
                if (intCount <= intRate)
                    imgItem.Visibility = imgYellowVisibility;
                else
                    imgItem.Visibility = imgGrayVisibility;
                intCount++;
            }
            intCount = 1;
        }

        private void imgRatePlus_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetRating(sender as Image);
            Rate = intRate;
            SetRating();
            lblRating.Text = TvSeriesTable.AverageRating.ToString();
        }

        private void GetRating(Image Img)
        {
            string strImgName = Img.Name;
            intRate = Convert.ToInt32(strImgName.Substring(strImgName.Length - 1, 1));
        }

        private void SetRating()
        {
            Rating oldRating = null;
            if (TvSeriesTable.Ratings.Count > 0)
            {
                oldRating = TvSeriesTable.Ratings.DefaultIfEmpty(null).Single(r => r.Id_User == CurrentUser.id);
            }
            if (oldRating == null)
            {
                TvSeriesTable.Ratings.Add(new Rating()
                {
                    Id_TVSerial = TvSeriesTable.Id,
                    Id_User = CurrentUser.id,
                    Mark = Rate,
                    TVSeriesTable = TvSeriesTable,
                    User = CurrentUser
                });
            }
            else
            {
                oldRating.Mark = Rate;
            }
        }
    }
}
