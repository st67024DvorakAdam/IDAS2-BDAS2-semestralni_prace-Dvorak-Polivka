﻿using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.Dialogs.Edit.Doctor
{
    public class AddNewIllnessVM : BaseViewModel
    {
        private string _newIllness;
        public string NewIllness
        {
            get => _newIllness;
            set
            {
                _newIllness = value;
                OnPropertyChange(nameof(NewIllness));
            }
        }

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChange(nameof(Patient));
            }
        }

        public event Action CloseRequested;

        public ICommand AddNewIllnessCommand { get; }
        public ICommand CancelCommand { get; }

        public AddNewIllnessVM(Patient patient)
        {
            _patient = patient;
            AddNewIllnessCommand = new RelayCommand(_ => AddIllness());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void AddIllness()
        {
            IllnessesRepo repo = new IllnessesRepo();
            
            repo.AddIllness(_newIllness, _patient.Id);
            CloseRequested?.Invoke();
        }

        private void Cancel()
        {
            
            CloseRequested?.Invoke();
        }
    }
}