﻿/*
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
    public partial class LoginPage : ContentPage
    {
        int count = 0;

        public LoginPage()
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
