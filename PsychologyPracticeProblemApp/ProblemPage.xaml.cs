using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp
{
    
    public partial class ProblemPage : ContentPage
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

        public ProblemPage()
        {
            for (int i = 0; i < 15; i++)
            {
                dataRows.Add(new DataRow(i, i * 2, i * 3));
            }

            InitializeComponent();
            //Create a bunch of datarows


            BindingContext = dataRows;
        }

    }
}
