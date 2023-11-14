﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyPracticeProblemApp;
public class User {
    public static Guid Guest => Guid.Empty;
    public static User Current { get; private set; } = null;
    public static string Error { get; private set; } = string.Empty;


    private Guid id;
    private string firstName, lastName, email;

    public String DisplayName => firstName;
    public String Email => email;
    public String FirstName => firstName;
    public String LastName => firstName;
    public Guid Id => id;


    public User() {}
    public User(Guid id, string firstName, string lastName, string email)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
    }
    /// <summary>
    /// Attempts to login user based on username and password.
    /// If login failed, user.ErrorCallback != 0
    /// </summary>
    /// <returns></returns>
    public static User Login(string username, string password)
    {
        User user = Database.GetUser(username, password);
        Current = user;
        Error = user == null ? "Username or password is incorrect!" : string.Empty; // if its null, throw error
        return user;
    }
    /// <summary>
    /// Creates a guest user
    /// </summary>
    /// <returns></returns>
    public static User LoginGuest()
    {
        User user = new User() {
            id = Guest, // default to admin
            firstName = "Guest",
        };
        Current = user;
        return user;
    }
    public async static void Logout(ContentPage currentPage)
    {
        Current = null;
        await currentPage.Navigation.PopToRootAsync();
    }


}
