/*
 * Matthew Bertello
 * Date: 10/18/2023
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp
{
    public partial class MLoginPage : ContentPage
    {

        public MLoginPage()
        {
            InitializeComponent();
        }
        public async void OnGuest(object sender, EventArgs e)
        {
            User.LoginGuest();
            await Navigation.PushAsync(new HomePage());

        }
    }
}
