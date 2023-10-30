using System.Diagnostics;
namespace PsychologyPracticeProblemApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            double[] data = { 8, 7, 2, 5, 9, 4, 8, 6, 5 };
            //double[] data = StatsUtil.GenerateRandomDataForceMean(30, 140);
            Debug.WriteLine(StatsUtil.DataToString(data));
            Debug.WriteLine(StatsUtil.CalcZScore(data, 7, 1.5));




            MainPage = new NavigationPage(new HomePage());
        }
    }
}