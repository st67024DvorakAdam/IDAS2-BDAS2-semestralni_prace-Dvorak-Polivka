﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM;
using Database_Hospital_Application.Views.Assistant;
using Database_Hospital_Application.Views.Doctor.Patient;
using Database_Hospital_Application.Views.Nurse;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class DoctorPatientVM : BaseViewModel
    {
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                UpdateCommandStates();
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChange(nameof(CurrentView));
            }
        }

        private Patient _currentPatient;
        public Patient CurrentPatient
        {
            get { return _currentPatient; }
            set
            {
                _currentPatient = value;
                OnPropertyChange(nameof(CurrentPatient));
                UpdateCommandStates();
            }
        }

        private UserControl _currentContent;
        public UserControl CurrentContent
        {
            get => _currentContent;
            set
            {
                _currentContent = value;
                OnPropertyChange(nameof(CurrentContent));
            }
        }

        private User _currentUser;
        

        private ObservableCollection<PersonalMedicalHistory> _medicalHistory;

        public ObservableCollection<PersonalMedicalHistory> MedicalHistory
        {
            get { return _medicalHistory; }
            set
            {
                _medicalHistory = value;
                OnPropertyChange(nameof(MedicalHistory));

            }
        }


        public ICommand SearchPatientCommand { get; private set; }
        public ICommand PersonalDetailsCommand { get; private set; }
        public ICommand AnamnesisCommand { get; private set; }
        public ICommand ProceduresCommand { get; private set; }
        public ICommand ActualIllnessCommand { get; private set; }
        public ICommand HospitalizationCommand { get; private set; }
        public ICommand DownloadReportCommand {  get; private set; }

        public DoctorPatientVM(User user)
        {
            _currentUser = user;
            SearchPatientCommand = new RelayCommand(ExecuteSearchPatient, CanExecuteSearchPatient);
            PersonalDetailsCommand = new RelayCommand(ExecutePersonalDetails, CanExecutePatientRelatedCommands);
            AnamnesisCommand = new RelayCommand(ExecuteAnamnesis, CanExecutePatientRelatedCommands);
            ProceduresCommand = new RelayCommand(ExecuteProcedures, CanExecutePatientRelatedCommands);
            ActualIllnessCommand = new RelayCommand(ExecuteActualIllness, CanExecutePatientRelatedCommands);
            HospitalizationCommand = new RelayCommand(ExecuteHospitalization, CanExecutePatientRelatedCommands);
            DownloadReportCommand = new RelayCommand(DownloadPatientReport, CanExecutePatientRelatedCommands);
        }

        private void DownloadPatientReport(object? obj)
        {
            PdfGenerator.GenerateAndDownloadPdfForPatient(CurrentPatient);
        }

        private async void ExecuteSearchPatient(object parameter)
        {
            
            if(_searchText == "" || _searchText == null)
            {
                MessageBox.Show("Vyplňte prosím pole s rodným číslem", "Pacient nenalezen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(_searchText.Length < 10)
            {
                MessageBox.Show("Špatný formát rodného čísla", "Chybný formát", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            PatientRepo patientRepo = new PatientRepo();
            CurrentPatient = await patientRepo.GetPatientByBirthNumber(SearchText);
            
            if (CurrentPatient == null)
            {
                MessageBox.Show("Pacient s tímto rodným číslem nebyl nalezen.", "Pacient nenalezen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UserControl personalDetailsView;
            switch (_currentUser.RoleID)
            {
                //DOCTOR
                case 2:
                    personalDetailsView = new PersonalDetailsView
                    {
                        DataContext = new PersonalDetailsVM(_currentPatient)
                    };
                    CurrentContent = personalDetailsView;
                    break;
                //NURSE
                case 3:
                    personalDetailsView = new PersonalDetailsNurseView
                    {
                        DataContext = new PersonalDetailsVM(_currentPatient)
                    };
                    CurrentContent = personalDetailsView;
                    break;
                // ASSISTANT
                case 4:
                    personalDetailsView = new PersonalDetailsView
                    {
                        DataContext = new PersonalDetailsVM(_currentPatient)
                    };
                    CurrentContent = personalDetailsView;
                    break;
                default: return;
            }
            await LoadMedicalHistoryAsync();
            
        }

        private async Task LoadMedicalHistoryAsync()
        {
            PersonalMedicalHistoriesRepo repo = new PersonalMedicalHistoriesRepo();
            MedicalHistory = await repo.GetPersonalMedicalHistoryByPatientIdAsync(CurrentPatient.Id);
        }




        private bool CanExecuteSearchPatient(object parameter)
        {
            return true;
        }

        private async void ExecutePersonalDetails(object parameter)
        {
            PatientRepo patientRepo = new PatientRepo();
            CurrentPatient = await patientRepo.GetPatientByBirthNumber(SearchText);
            UserControl personalDetailsView;
            switch (_currentUser.RoleID)
            {
                //DOCTOR
                case 2:
                    personalDetailsView = new PersonalDetailsView
                    {
                        DataContext = new PersonalDetailsVM(_currentPatient)
                    };
                    CurrentContent = personalDetailsView;
                    break;
                //NURSE
                case 3:
                    personalDetailsView = new PersonalDetailsNurseView
                    {
                        DataContext = new PersonalDetailsVM(_currentPatient)
                    };
                    CurrentContent = personalDetailsView;
                    break;
                // ASSISTANT
                case 4:
                    personalDetailsView = new PersonalDetailsView
                    {
                        DataContext = new PersonalDetailsVM(_currentPatient)
                    };
                    CurrentContent = personalDetailsView;
                    break;
                default: return;
            }
            
        }

        private void ExecuteAnamnesis(object parameter)
        {
            
            LoadMedicalHistoryAsync();
            var anamnesisView = new AnamnesisView
            {
                DataContext = this
            };

            CurrentContent = anamnesisView;
        }

        private void ExecuteProcedures(object parameter)
        {
            UserControl performedProcedureView;
            switch (_currentUser.RoleID)
            {
                //DOCTOR
                case 2:
                    performedProcedureView = new PerformedProceduresView
                        {
                            DataContext = new PerformedProcedureVM(_currentPatient)
                        };
                    CurrentContent = performedProcedureView;
                    break;
                //NURSE
                case 3:
                    performedProcedureView = new PerformedProceduresNurseAssistantView
                    {
                        DataContext = new PerformedProcedureVM(_currentPatient)
                    };
                    CurrentContent = performedProcedureView;
                    break;
                // ASSISTANT
                case 4:
                    performedProcedureView = new PerformedProceduresNurseAssistantView
                    {
                        DataContext = new PerformedProcedureVM(_currentPatient)
                    };
                    CurrentContent = performedProcedureView;
                    break;
                default: return;
            }
            

        }

        private void ExecuteActualIllness(object parameter)
        {
            UserControl actualIllnessView;
            switch (_currentUser.RoleID)
            {
                //DOCTOR
                case 2:
                    actualIllnessView = new ActualIllnessView
                    {
                        DataContext = new ActualIllnessVM(CurrentPatient, _currentUser)
                    };

                    CurrentContent = actualIllnessView;
                    break;
                //NURSE
                case 3:
                    actualIllnessView = new ActualIllnessNurseView
                    {
                        DataContext = new ActualIllnessVM(CurrentPatient, _currentUser)
                    };

                    CurrentContent = actualIllnessView;
                    break;
                // ASSISTANT
                case 4:
                    actualIllnessView = new ActualIllnessNurseView
                    {
                        DataContext = new ActualIllnessVM(CurrentPatient, _currentUser)
                    };

                    CurrentContent = actualIllnessView;
                    break;
                default: return;
            }
            
        }

        private void ExecuteHospitalization(object parameter)
        {
            UserControl hospitalizationView;
            switch (_currentUser.RoleID)
            {
                //DOCTOR
                case 2:
                    hospitalizationView = new HospitalizationView
                    {
                        DataContext = new Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM.HospitalizationVM(CurrentPatient, _currentUser)
                    };

                    CurrentContent = hospitalizationView;
                    break;
                //NURSE
                case 3:
                    hospitalizationView = new HospitalizationNurseView
                    {
                        DataContext = new Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM.HospitalizationVM(CurrentPatient, _currentUser)
                    };

                    CurrentContent = hospitalizationView;
                    break;
                // ASSISTANT
                case 4:
                    hospitalizationView = new HospitalizationAssistantView
                    {
                        DataContext = new Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM.HospitalizationVM(CurrentPatient, _currentUser)
                    };

                    CurrentContent = hospitalizationView;
                    break;
                default: return;
            }
           
        }

        private void UpdateCommandStates()
        {
            (PersonalDetailsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (AnamnesisCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (ProceduresCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (ActualIllnessCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (HospitalizationCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DownloadReportCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private bool CanExecutePatientRelatedCommands(object parameter)
        {
            return CurrentPatient != null;
        }
    }
}
