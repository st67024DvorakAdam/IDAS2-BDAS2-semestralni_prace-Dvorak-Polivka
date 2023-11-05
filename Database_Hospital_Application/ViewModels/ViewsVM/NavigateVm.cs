﻿using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class NavigateVM : BaseViewModel
    {
        private object _currentView;
        public User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChange(nameof(CurrentUser));
                }
            }
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChange(nameof(CurrentView));
                }
            }
        }

        public ICommand UsersListCommand { get; }
        public ICommand ProfileCommand { get; }
        public ICommand PacientsListCommand { get; }
        public ICommand AddressesListCommand { get; }
        public ICommand HealthInsurancesListCommand { get; }

        private void Profile(object obj) => CurrentView = new CurrUserVM(CurrentUser);
        private void Users(object obj) => CurrentView = new UserVM();
        private void Pacients(object obj) => CurrentView = new UserVM();//PacientsVM();
        private void Addresses(object obj) => CurrentView = new AddressesVM();
        private void HealthInsurances(object obj) => CurrentView = new HealthInsurancesVM();

        public NavigateVM(User user)
        {
            // Nastavit defaultní pohled
            CurrentUser = user;
            CurrentView = new CurrUserVM(CurrentUser); // Měli byste mít vytvořený tento ViewModel


            // Inicializace příkazů
            ProfileCommand = new RelayCommand(Profile);
            UsersListCommand = new RelayCommand(Users);
            PacientsListCommand = new RelayCommand(Pacients);
            AddressesListCommand = new RelayCommand(Addresses);
            HealthInsurancesListCommand = new RelayCommand(HealthInsurances);

            // Další inicializace...
        }
    }
}
