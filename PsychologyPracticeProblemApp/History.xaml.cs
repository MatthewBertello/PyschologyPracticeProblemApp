using System.Collections.ObjectModel;

namespace PsychologyPracticeProblemApp;

public partial class History : ContentPage
{
    public class Data
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public String XString { get { return X.ToString(); } }
        public String YString { get { return Y.ToString(); } }
        public String ZString { get { return Z.ToString(); } }

        public Data(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    ObservableCollection<Data> dataR = new ObservableCollection<Data>();
    public History()
    {
        for (int i = 0; i < 15; i++)
        {
            dataR.Add(new Data(i, i * 2, i * 3));
        }
        InitializeComponent();
        BindingContext = dataR;
    }
}