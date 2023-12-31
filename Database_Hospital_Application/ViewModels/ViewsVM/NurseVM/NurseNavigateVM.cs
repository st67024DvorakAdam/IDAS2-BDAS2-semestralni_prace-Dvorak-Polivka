﻿using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.ViewModels.ViewsVM.AssistantVM;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM.NurseVM
{
    public class NurseNavigateVM:BaseViewModel
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

        public ICommand ProfileCommand { get; }
        public ICommand DosageForHospitalizatedCommand { get; }
        public ICommand HospitalizaceCommand { get; }
        public ICommand PatientCommand { get; }
        public ICommand SubordinatesCommand { get; }
        public ICommand GeneralInfoCommand { get; }

        private void Profile(object obj) => CurrentView = new CurrUserVM(CurrentUser);
        private void DosageForHospitalizated(object obj) => CurrentView = new DosageForHospitalizatedVM(CurrentUser);
        private void Hospitalizace(object obj) => CurrentView = new HospitalizationVM(CurrentUser.Employee);
        private void Patient(object obj) => CurrentView = new DoctorPatientVM(CurrentUser);
        private void Subordinates(object obj) => CurrentView = new SubordinatesVM(CurrentUser);
        private void GeneralInfo(object obj) => CurrentView = new GeneralInfoVM();


        public NurseNavigateVM(User user)
        {
            //defaultní pohled
            CurrentUser = user;
            CurrentView = new CurrUserVM(CurrentUser);


            //Inicializace příkazů
            ProfileCommand = new RelayCommand(Profile);
            DosageForHospitalizatedCommand = new RelayCommand(DosageForHospitalizated);
            HospitalizaceCommand = new RelayCommand(Hospitalizace);
            PatientCommand = new RelayCommand(Patient);
            SubordinatesCommand = new RelayCommand(Subordinates);
            GeneralInfoCommand = new RelayCommand(GeneralInfo);
        }
    }
}
