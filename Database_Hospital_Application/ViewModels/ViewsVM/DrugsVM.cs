﻿using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Contact;
using Database_Hospital_Application.Views.Lists.Dialogs.Drug;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class DrugsVM : BaseViewModel
    {
        private ObservableCollection<Drug> _drugsList;

        public ObservableCollection<Drug> DrugsList
        {
            get { return _drugsList; }
            set
            {
                _drugsList = value;
                OnPropertyChange(nameof(DrugsList));
            }
        }


        private ObservableCollection<Employee> _doctorsList;
        public ObservableCollection<Employee> DoctorsList
        {
            get { return _doctorsList; }
            set
            {
                _doctorsList = value;
                OnPropertyChange(nameof(DoctorsList));
            }
        }

        private async Task LoadDoctorsAsync()
        {
            EmployeesRepo repo = new EmployeesRepo();
            DoctorsList = await repo.GetAllEmployeesAsync();
            DoctorsList = new ObservableCollection<Employee>(
            DoctorsList.Where(emp => emp.RoleID == 2 || emp._role.Id == 2));
        }

        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private Drug _selectedDrug;
        public Drug SelectedDrug
        {
            get { return _selectedDrug; }
            set
            {
                if (_selectedDrug != value)
                {
                    _selectedDrug = value;
                    OnPropertyChange(nameof(SelectedDrug));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Drug _newDrug = new Drug();
        public Drug NewDrug
        {
            get { return _newDrug; }
            set
            {
                _newDrug = value;
                OnPropertyChange(nameof(NewDrug));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public DrugsVM()
        {
            LoadDrugsAsync();
            
            InitializeCommands();
            LoadDoctorsAsync();
        }
        private async Task LoadDrugsAsync()
        {
            DrugsRepo repo = new DrugsRepo();
            DrugsList = await repo.GetAllDrugsAsync();
            if (DrugsList != null)
            {
                DrugsView = CollectionViewSource.GetDefaultView(DrugsList);
                DrugsView.Filter = DrugsFilter;
            }
        }


        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewAction);
            DeleteCommand = new RelayCommand(DeleteAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedDrug != null;
        }

        private async void DeleteAction(object parameter)
        {
            if (SelectedDrug == null) return;

            DrugsRepo drugsRepo = new DrugsRepo();
            await drugsRepo.DeleteDrug(SelectedDrug.Id);
            await LoadDrugsAsync();
        }

        private async void AddNewAction(object parameter)
        {
            if(!IsDrugValidAndFilled(NewDrug))
            {
                if(NewDrug.Dosage == 0)
                {
                    MessageBox.Show("Dávkování léku nesmí být 0!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Vyplňte správně všechna pole", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            DrugsRepo drugsRepo = new DrugsRepo();
            await drugsRepo.AddDrug(NewDrug);
            await LoadDrugsAsync();
            NewDrug = new Drug();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedDrug != null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;

            EditDrugVM editVM = new EditDrugVM(SelectedDrug);
            EditDrugDialog editDialog = new EditDrugDialog(editVM);

            editDialog.ShowDialog();

            LoadDrugsAsync();
            
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView DrugsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                DrugsView.Refresh();
            }
        }

        private bool DrugsFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var drug = item as Drug;
            if (drug == null) return false;

            return drug.Id.ToString().Contains(_searchText)
                || drug.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || drug.Dosage.ToString().Contains(_searchText)
                || drug.Employee_id.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////



        private bool IsDrugValidAndFilled(Drug drug)
        {
            return (!string.IsNullOrEmpty(drug.Name) && (drug.Dosage != 0 && drug.Dosage != null) && (drug.Employee_id != 0 && drug.Employee_id != null));
        }
    }
}
