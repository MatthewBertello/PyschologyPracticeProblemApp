/*
 * Michael Meisenburg
 * Date: 10/18/2023
*/
using System.Collections.ObjectModel;

namespace PsychologyPracticeProblemApp;

public partial class Example : ContentPage
{
    public class DataRow
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public String XString { get { return X.ToString(); } }
        public String YString { get { return Y.ToString(); } }
        public String ZString { get { return Z.ToString(); } }

        public DataRow(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    ObservableCollection<DataRow> dataRows = new ObservableCollection<DataRow>();
    public Example()
	{
        for (int i = 0; i < 10; i++)
        {
            dataRows.Add(new DataRow(i, i * 2, i * 3));
        }
        InitializeComponent();
        BindingContext = dataRows;
    }

    private void OnCloseExampleClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}