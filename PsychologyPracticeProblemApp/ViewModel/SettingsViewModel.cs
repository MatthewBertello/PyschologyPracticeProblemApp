using PsychologyPracticeProblemApp.Model.Sql;
using PsychologyPracticeProblemApp.Model.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.ViewModel;
public class SettingsViewModel : CoreViewModel, INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;
    public ObservableCollection<UserModel> UsersList { get; set; } = new();
    public String DSCount => PropertiesUtil.DatasetCount.ToString();
    public String DSMin => PropertiesUtil.DatasetMin.ToString();
    public String DSMax => PropertiesUtil.DatasetMax.ToString();
    public String HistCount => PropertiesUtil.HistoryCount.ToString();
    public ContentPage page;

    public SettingsViewModel(ContentPage page)
    {
        this.page = page;
        UpdateUserList();
    }
    public void UpdateUserList()
    {
        UsersList.Clear();
        LinkedList<User> userList = Database.GetUsersAll();
        Boolean isOdd = true;
        foreach(User user in userList)
            UsersList.Add(new UserModel(user.DisplayName, user.Username, isOdd = !isOdd, this));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserList"));
    }
    public void ChangeDSCount(int delta)
    {
        PropertiesUtil.DatasetCount = Math.Clamp(PropertiesUtil.DatasetCount + delta, 3, 20);
        PropertiesUtil.Save();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DSCount"));
    }
    public void ChangeDSMin(int delta)
    {
        PropertiesUtil.DatasetMin = Math.Clamp(PropertiesUtil.DatasetMin + delta, 0, PropertiesUtil.DatasetMax);
        PropertiesUtil.Save();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DSMin"));
    }
    public void ChangeDSMax(int delta)
    {
        PropertiesUtil.DatasetMax = Math.Clamp(PropertiesUtil.DatasetMax + delta, PropertiesUtil.DatasetMin, 100);
        PropertiesUtil.Save();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DSMax"));
    }
    public void ChangeHistoryCount(int delta)
    {
        PropertiesUtil.HistoryCount = Math.Clamp(PropertiesUtil.HistoryCount+delta, 5, 20);
        PropertiesUtil.Save();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HistCount"));
    }

}
public class UserModel {
    public String Name { get; set; }
    public String Username { get; set; }
    public Boolean IsOdd { get; set; }
    public Boolean IsEven => !IsOdd;
    public Command OnDeleteCommand { get; set; }
    private readonly SettingsViewModel parent;
    public UserModel(String name, String username, bool isOdd, SettingsViewModel parent)
    {
        this.Name = name;
        this.Username = username;
        this.IsOdd = isOdd;
        this.parent = parent;
        OnDeleteCommand = new Command(OnDelete);
    }
    public async void OnDelete()
    {
        bool delete = await parent.page.DisplayAlert("Delete User " + Username + "!", "Deleting this user cannot be undone.\nDelete anyways?", "Delete", "Cancel");
        if(delete)
        {
            Database.RemoveUser(Username);
            DatabaseCache.DirtyUsers = true;
            parent.UpdateUserList();
        }
    }
}