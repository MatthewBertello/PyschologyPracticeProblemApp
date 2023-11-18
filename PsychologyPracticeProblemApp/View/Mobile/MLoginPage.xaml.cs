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
        await Navigation.PushAsync(new MProblemSelectPage());
    }
    public async void OnLogin(object sender, EventArgs e)
    {
        ErrorMessage = "";
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;
        if(string.IsNullOrEmpty(username))
            ErrorMessage = "Username cannot be blank!";
        else if(string.IsNullOrEmpty(password))
            ErrorMessage = "Password cannot be blank!";
        else
        {
            User user = User.Login(username, password);
            if(user != null) await Navigation.PushAsync(new MProblemSelectPage());
            else ErrorMessage = "Invalid Username or Password!";
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorMessage"));
    }

    private void OnCreateAccount(object sender, EventArgs e)
    {

    }
}
