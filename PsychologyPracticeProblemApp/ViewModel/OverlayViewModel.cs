/*
 * Michael Hulbert
 * Date: 10/18/2023
*/
using MvvmHelpers;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PsychologyPracticeProblemApp.ViewModel;

public class OverlayViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;

    public String PageName { get; set; }
    public String Username => User.Current?.DisplayName ?? string.Empty;
    public Command LogoutCommand { get; set; }
    public Command HomeCommand { get; set; }

    public Boolean IsGuest => User.Current.Id == User.Guest;
    public Boolean IsNotGuest => !IsGuest;

    private ContentPage parent;
    public OverlayViewModel(ContentPage parent, String pageName)
    {
        this.parent = parent;
        this.PageName = pageName;
        LogoutCommand = new Command(OnLogout);
        HomeCommand = new Command(OnHome);
    }

    public void OnLogout()
    {
        User.Logout(parent);
    }
    public async void OnHome()
    {
        while(parent.Navigation.NavigationStack.Count > 0)
        {
            await parent.Navigation.PopAsync();
        }

    }

}