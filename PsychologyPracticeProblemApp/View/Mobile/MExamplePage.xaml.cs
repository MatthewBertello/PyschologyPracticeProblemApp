/*
 * Matthew Bertello
 * Date: 10/18/2023
*/
using PsychologyPracticeProblemApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public partial class MExamplePage : ContentPage {

    private string[] problemTypes = { "standarddeviationex.png", "onesamplettestex.png", "dependentsameplettestex.png", "independentsamplettestex.png", "zscoreex.png" };
    public String ProblemName { get; set; }
    public String ProblemImage { get; set; }
    public MExamplePage(IProblem problem, DataSet? dataset=null)
    {
        ProblemName = problem.Name + " Example";
        ProblemImage = problemTypes[problem.Id - 1];
        InitializeComponent();
        BindingContext = this;
    }
    public void OnBack(object sender, EventArgs e)
    {
        Navigation?.PopAsync();
    }

}

