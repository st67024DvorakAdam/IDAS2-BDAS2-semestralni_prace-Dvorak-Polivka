﻿using System;
using System.Windows.Input;
using Database_Hospital_Application.Commands;

namespace Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM
{
    public class NewPatientVM : BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _identificationNumber;
        private string _gender;
        private string _city;
        private string _street;
        private string _houseNumber;
        private string _postalCode;
        private string _country;
        private string _email;
        private string _phone;
        private string _insuranceCompanyName;
        private string _insuranceCompanyAbbreviation;
        private bool _isSmoker;
        private bool _isAllergic;

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChange(nameof(FirstName)); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; 
                OnPropertyChange(nameof(LastName)); }
        }

        public string IdentificationNumber
        {
            get => _identificationNumber;
            set { _identificationNumber = value;
                OnPropertyChange(nameof(IdentificationNumber)); }
        }

        public string Gender
        {
            get => _gender;
            set { _gender = value;
                OnPropertyChange(nameof(Gender)); }
        }

        public string City
        {
            get => _city;
            set { _city = value;
                OnPropertyChange(nameof(City)); }
        }

        public string Street
        {
            get => _street;
            set { _street = value;
                OnPropertyChange(nameof(Street)); }
        }

        public string HouseNumber
        {
            get => _houseNumber;
            set { _houseNumber = value;
                OnPropertyChange(nameof(HouseNumber)); }
        }

        public string PostalCode
        {
            get => _postalCode;
            set { _postalCode = value;
                OnPropertyChange(nameof(PostalCode)); }
        }

        public string Country
        {
            get => _country;
            set { _country = value;
                OnPropertyChange(nameof(Country)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value;
                OnPropertyChange(nameof(Email)); }
        }

        public string Phone
        {
            get => _phone;
            set { _phone = value;
                OnPropertyChange(nameof(Phone)); }
        }

        public string InsuranceCompanyName
        {
            get => _insuranceCompanyName;
            set { _insuranceCompanyName = value;
                OnPropertyChange(nameof(InsuranceCompanyName)); }
        }

        public string InsuranceCompanyAbbreviation
        {
            get => _insuranceCompanyAbbreviation;
            set { _insuranceCompanyAbbreviation = value;
                OnPropertyChange(nameof(InsuranceCompanyAbbreviation)); }
        }

        public bool IsSmoker
        {
            get => _isSmoker;
            set { _isSmoker = value;
                OnPropertyChange(nameof(IsSmoker)); }
        }

        public bool IsAllergic
        {
            get => _isAllergic;
            set { _isAllergic = value;
                OnPropertyChange(nameof(IsAllergic)); }
        }

        public ICommand AcceptPatientCommand { get; private set; }

        public NewPatientVM()
        {
            AcceptPatientCommand = new RelayCommand(ExecuteAcceptPatient, CanExecuteAcceptPatient);
        }

        private void ExecuteAcceptPatient(object parameter)
        {
            // TODO
        }

        private bool CanExecuteAcceptPatient(object parameter)
        {
            return IsFormValid();
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(IdentificationNumber) &&
                   !string.IsNullOrWhiteSpace(Gender) &&
                   !string.IsNullOrWhiteSpace(City) &&
                   !string.IsNullOrWhiteSpace(Street) &&
                   !string.IsNullOrWhiteSpace(HouseNumber) &&
                   !string.IsNullOrWhiteSpace(PostalCode) &&
                   !string.IsNullOrWhiteSpace(Country) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Phone) &&
                   !string.IsNullOrWhiteSpace(InsuranceCompanyName) &&
                   !string.IsNullOrWhiteSpace(InsuranceCompanyAbbreviation);
        }

        protected override void OnPropertyChange(string propertyName)
        {
            base.OnPropertyChange(propertyName);
            if (propertyName != nameof(AcceptPatientCommand))
            {
                (AcceptPatientCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }
    }
}