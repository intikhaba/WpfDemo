using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfDemo.UserControls
{
    /// <summary>
    /// Interaction logic for CustomerEntry.xaml
    /// </summary>
    public partial class CustomerEntry : UserControl
    {
        public static readonly DependencyProperty SaveLabelProperty =
          DependencyProperty.Register(nameof(SaveLabel), typeof(string), typeof(CustomerEntry)
              , new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                  , new PropertyChangedCallback(OnSaveLabelChanged)));

        public static readonly DependencyProperty ControlBackgroundColorProperty =
          DependencyProperty.Register(nameof(ControlBackgroundColor), typeof(SolidColorBrush), typeof(CustomerEntry)
              , new FrameworkPropertyMetadata(new SolidColorBrush(Colors.HotPink), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                  , new PropertyChangedCallback(OnControlBackgroundColorChanged)));

        public CustomerEntry()
        {
            InitializeComponent();
        }

        public SolidColorBrush ControlBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(ControlBackgroundColorProperty); }
            set { SetValue(ControlBackgroundColorProperty, value); }
        }

        public string SaveLabel
        {
            get { return (string)GetValue(SaveLabelProperty); }
            set { SetValue(SaveLabelProperty, value); }
        }

        private static void OnControlBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomerEntry customerEntry = d as CustomerEntry;
            customerEntry.ChangeBackground(e);
        }

        private void ChangeBackground(DependencyPropertyChangedEventArgs e)
        {
            SolidColorBrush oldColor = (SolidColorBrush)e.OldValue;
            customerEntryGrid.Background = (SolidColorBrush)e.NewValue;
        }

        private static void OnSaveLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomerEntry customerEntry = d as CustomerEntry;
            customerEntry.ChangeSaveLabel(e);
        }

        private void ChangeSaveLabel(DependencyPropertyChangedEventArgs e)
        {
            btnSave.Content = (string)e.NewValue;
        }
    }
}
