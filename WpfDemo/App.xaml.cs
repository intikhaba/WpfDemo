using System.Windows;
using WpfDemo.Bootstrappers;
//using Microsoft.Practices.Unity;

//IUnityContainer container = new UnityContainer();
////or
//var container = new UnityContainer();

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Bootstrapper.RegisterTypes();
            base.OnStartup(e);

            //var mainWindowViewModel = container.Resolve<MainWindowViewModel>();
            //var window = new MainWindow { DataContext = mainWindowViewModel };
            //window.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }
    }
}
