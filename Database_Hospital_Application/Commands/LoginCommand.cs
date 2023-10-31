﻿using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.ViewsVM;
using System.Windows;

public class LoginCommand : BaseCommand
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    

    public LoginCommand(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
    }

    public override void Execute(object? parameter)
    {
        UserRepo userRepo = new UserRepo();
        var _username = _mainWindowViewModel.Username;
        var _password = _mainWindowViewModel.Password;

        if(userRepo.LoginUser(_password, _username)) {
        MessageBox.Show("Execute metoda, username: " + _username + " password: " + _password, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}