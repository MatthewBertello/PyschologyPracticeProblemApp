﻿using System.Diagnostics;
namespace PsychologyPracticeProblemApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new HomePage());
            MainPage = new AppShell();
        }
    }
}