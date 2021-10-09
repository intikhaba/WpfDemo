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
        public static readonly DependencyProperty ControlBackgroundColorProperty =
          DependencyProperty.Register("ControlBackgroundColor", typeof(SolidColorBrush), typeof(CustomerEntry)
              , new PropertyMetadata(new SolidColorBrush(Colors.HotPink), new PropertyChangedCallback(OnControlBackgroundColorChanged)));

        public CustomerEntry()
        {
            InitializeComponent();
        }

        public SolidColorBrush ControlBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(ControlBackgroundColorProperty); }
            set { SetValue(ControlBackgroundColorProperty, value); }
        }
      
        private static void OnControlBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomerEntry customerEntry = d as CustomerEntry;
            customerEntry.ChangeBackground(e);
        }

        private void ChangeBackground(DependencyPropertyChangedEventArgs e)
        {
            customerEntryGrid.Background = (SolidColorBrush)e.NewValue;
        }
    }
}
