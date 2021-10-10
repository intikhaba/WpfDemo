using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Prism.Events;
using WpfDemo.Bootstrappers;
using WpfDemo.ViewModels;

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

        public static RoutedCommand CancelCommand = new RoutedCommand();

        public CustomerEntry()
        {
            InitializeComponent();

            var customCommandBinding = new CommandBinding(CancelCommand, ExecutedCancelCommand, CanExecuteCancelCommand);
            CommandBindings.Add(customCommandBinding);

            DataContext = new CustomerEntryViewModel(Bootstrapper.Resolve<IEventAggregator>());
            ControlBackgroundColor = Brushes.GreenYellow;
            SaveLabel = "Store";
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

        private void ExecutedCancelCommand(object sender, ExecutedRoutedEventArgs e)
        {
            CustomerEntryViewModel customerEntryViewModel = DataContext as CustomerEntryViewModel;
            customerEntryViewModel.Customer.Reset();
        }

        private void CanExecuteCancelCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!(e.Source is Control target))
            {
                e.CanExecute = false;
                return;
            }

            if (!(DataContext is CustomerEntryViewModel customerEntryViewModel))
            {
                e.CanExecute = false;
                return;
            }

            CustomerViewModel customer = customerEntryViewModel.Customer;
            e.CanExecute = customer.CanBeCleared();
        }
    }
}
