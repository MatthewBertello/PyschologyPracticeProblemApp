using System.Diagnostics;
namespace PsychologyPracticeProblemApp;

public partial class App : Application
{
    public static App CurrentApp { get; set; }
    public static Boolean IsWindows => (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop || DeviceInfo.Current.Idiom == DeviceIdiom.Tablet);
    public App()
    {
        CurrentApp = this;
        InitializeComponent();

        //MainPage = new NavigationPage(new HomePage());
        //MainPage = new AppShell();
        if(IsWindows)
            MainPage = new NavigationPage(new LoginPage());
        else
            MainPage = new AppShell();
    }
}
