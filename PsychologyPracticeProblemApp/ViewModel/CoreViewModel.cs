using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp.ViewModel;
public class CoreViewModel {
    public Boolean IsGuest => User.Current.Id == User.Guest;
    public Boolean IsNotGuest => !IsGuest;
    public Boolean IsAdmin => User.Current.Id == User.Admin;
    public Boolean IsNotAdmin => !IsAdmin;

}
