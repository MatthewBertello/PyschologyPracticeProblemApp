/*
 * Matthew Bertello
 * Date: 10/18/2023
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;

public partial class MLoginPage : ContentPage, INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;

    public string ErrorMessage { get; set; } = string.Empty;
    public MLoginPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
    public async void OnGuest(object sender, EventArgs e)
    {
        User.LoginGuest();
        await Navigation.PushAsync(new HomePage());
    }
    public async void OnLogin(object sender, EventArgs e)
    {
        ErrorMessage = "";
        string username = PasswordEntry.Text;
        string password = PasswordEntry.Text;
        User user = User.Login(username, password);
        //if(user != null) await Navigation.PushAsync(new HomePage());
        //else ErrorMessage = "Invalid Username or Password!";

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorMessage"));
    }

    private void OnCreateAccount(object sender, EventArgs e)
    {

    }
}
