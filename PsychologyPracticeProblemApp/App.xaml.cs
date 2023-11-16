using System.Diagnostics;
namespace PsychologyPracticeProblemApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new HomePage());
            //MainPage = new AppShell();
            if(DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
                MainPage = new NavigationPage(new LoginPage());
            else
                MainPage = new NavigationPage(new MLoginPage());
        }
    }
}
