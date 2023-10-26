using System.ComponentModel;

namespace PsychologyPracticeProblemApp.Model
{
    public enum ProblemType { Dependent_t_test, Independent_t_test }
    public class Problem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int problemID;
        private ProblemType problemType;
        private string xValuesString;
        private string? yValuesString;
        private string? zValuesString;
        private List<double> xValues;
        private List<double> yValues;
        private List<double> zValues;
        private double? solution;

        /// <summary>
        /// Creates a problem object
        /// </summary>
        /// <param name="problemID"></param>
        /// <param name="problemType"></param>
        /// <param name="xValuesString"></param>
        /// <param name="yValuesString"></param>
        /// <param name="zValuesString"></param>
        public Problem(int problemID, ProblemType problemType, string xValuesString, string yValuesString, string zValuesString)
        {
            this.problemID = problemID;
            this.problemType = problemType;
            this.xValuesString = xValuesString;
            this.yValuesString = yValuesString;
            this.zValuesString = zValuesString;
            this.xValues = ParseValues(xValuesString);
            this.yValues = ParseValues(yValuesString);
            this.zValues = ParseValues(zValuesString);
            GetSolution();
        }

        

        /// <summary>
        /// Gets or sets the problem id
        /// </summary>
        public int ProblemId
        {
            get { return problemID;}
            set
            {
                problemID = value;
                OnPropertyChanged(nameof(ProblemId));
            }
        }

        /// <summary>
        /// Gets or sets the problem type
        /// </summary>
        /// <param name="propertyName"></param>
        public ProblemType ProblemType
        {
            get { return problemType; }
            set
            {
                problemType = value;
                OnPropertyChanged(nameof(ProblemType));
            }
        }

        /// <summary>
        /// Gets or sets the x values string
        /// </summary>
        /// <param name="propertyName"></param>
        public string XValuesString
        {
            get { return xValuesString; }
            set
            {
                xValuesString = value;
                OnPropertyChanged(nameof(XValuesString));
            }
        }

        /// <summary>
        /// Gets or sets the y values string
        /// </summary>
        /// <param name="propertyName"></param>
        public string YValuesString
        {
            get { return yValuesString; }
            set
            {
                yValuesString = value;
                OnPropertyChanged(nameof(YValuesString));
            }
        }

        /// <summary>
        /// Gets or sets the z values string
        /// </summary>
        /// <param name="propertyName"></param>
        public string ZValuesString
        {
            get { return zValuesString; }
            set
            {
                zValuesString = value;
                OnPropertyChanged(nameof(ZValuesString));
            }
        }

        /// <summary>
        /// Gets or sets the x values
        /// </summary>
        public List<double> XValues
        {
            get { return xValues; }
            set
            {
                xValues = value;
                OnPropertyChanged(nameof(XValues));
            }
        }

        /// <summary>
        /// Gets or sets the y values
        /// </summary>
        /// <param name="propertyName"></param>
        public List<double> YValues
        {
            get { return yValues; }
            set
            {
                yValues = value;
                OnPropertyChanged(nameof(YValues));
            }
        }

        /// <summary>
        /// Gets or sets the z values
        /// </summary>
        /// <param name="propertyName"></param>
        public List<double> ZValues
        {
            get { return zValues; }
            set
            {
                zValues = value;
                OnPropertyChanged(nameof(ZValues));
            }
        }

        /// <summary>
        /// Gets or sets the solution
        /// </summary>
        /// <param name="propertyName"></param>
        public double? Solution
        {
            get { return solution; }
            set
            {
                solution = value;
                OnPropertyChanged(nameof(Solution));
            }
        }

        /// <summary>
        /// Event handler for when a property is changed
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Parses a string of comma separated values into a list of doubles
        /// </summary>
        /// <param name="valuesString"></param>
        /// <returns>
        /// A list of doubles
        /// </returns>
        public List<double> ParseValues(string valuesString)
        {
            List<double> values = new List<double>();
            string[] valuesStringArray = valuesString.Split(',');
            foreach (string valueString in valuesStringArray)
            {
                values.Add(double.Parse(valueString));
            }
            return values;
        }

        /// <summary>
        /// Gets the solution to the problem
        /// </summary>
        /// <returns>
        /// The solution to the problem
        /// </returns>
        public double? GetSolution()
        {
            switch(ProblemType)
            {
                case ProblemType.Dependent_t_test:
                    solution = SolveDependentTTest();
                    return solution;
                case ProblemType.Independent_t_test:
                    solution = SolveIndependentTTest();
                    return solution;
                default:
                    solution = null;
                    return solution;
            }
        }

        /// <summary>
        /// Solves a dependent t test
        /// </summary>
        /// <returns></returns>
        public double SolveDependentTTest()
        {
            return 0;
        }

        /// <summary>
        /// Solves an independent t test
        /// </summary>
        /// <returns></returns>
        public double SolveIndependentTTest()
        {
            return 0;
        }
    }
}
