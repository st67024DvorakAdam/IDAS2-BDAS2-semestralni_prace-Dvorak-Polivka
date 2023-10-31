﻿using Database_Hospital_Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class MainWindowViewModel : BaseViewModel
    {
        public BaseViewModel CurrentVM { get; }
        
        public event Action<string, string> ShowMessageRequested;

        public ICommand LoginCommand { get; }

        public MainWindowViewModel()
        {
            LoginCommand = new LoginCommand(this);
        }

        //public void ShowMessage(string message, string caption)
        //{
        //    ShowMessageRequested?.Invoke(message, caption);
        //}
        private string _username;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChange(nameof(Username)); 
            }

        }

        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChange(nameof(Password));
            }

        }

        


    }
}
