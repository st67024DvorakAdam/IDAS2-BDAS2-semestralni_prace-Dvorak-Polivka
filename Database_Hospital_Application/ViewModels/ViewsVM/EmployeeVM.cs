﻿using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Contact;
using Database_Hospital_Application.Views.Lists.Dialogs.Employee;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class EmployeeVM : BaseViewModel
    {
        private ObservableCollection<Employee> _employeesList;
        
        public ObservableCollection<Employee> EmployeesList
        {
            get { return _employeesList; }
            set
            {
                _employeesList = value;
                OnPropertyChange(nameof(EmployeesList));
            }
        }

        private ObservableCollection<Foto> _photosList;
        public ObservableCollection<Foto> PhotosList
        {
            get { return _photosList; }
            set
            {
                _photosList = value;
                OnPropertyChange(nameof(PhotosList));
            }
        }

        private ObservableCollection<Department> _departmentList;
        public ObservableCollection<Department> DepartmentList
        {
            get { return _departmentList; }
            set
            {
                _departmentList = value;
                OnPropertyChange(nameof(DepartmentList));
            }
        }

        private ObservableCollection<Role> _roleList;
        public ObservableCollection<Role> RoleList
        {
            get { return _roleList; }
            set
            {
                _roleList = value;
                OnPropertyChange(nameof(RoleList));
            }
        }

        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChange(nameof(SelectedEmployee));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Employee _newEmployee = new Employee();
        public Employee NewEmployee
        {
            get { return _newEmployee; }
            set
            {
                _newEmployee = value;
                OnPropertyChange(nameof(NewEmployee));
            }
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public EmployeeVM()
        {
            LoadEmployeesAsync();
            InitializeCommands();

            LoadPhotosFromPhotoVM();
            LoadDepartmentsFromDepartmentVM();
            LoadRolesFromRoleVM();
        }

        private void LoadPhotosFromPhotoVM()
        {
            PhotoVM p = new PhotoVM();
            _photosList = p.PhotosList;
        }

        private void LoadDepartmentsFromDepartmentVM()
        {
            DepartmentVM d = new DepartmentVM();
            _departmentList = d.DepartmentsList;
        }

        private void LoadRolesFromRoleVM()
        {
            RolesVM r = new RolesVM();
            _roleList = r.RolesList;
        }

        private async Task LoadEmployeesAsync()
        {
            EmployeesRepo repo = new EmployeesRepo();
            EmployeesList = await repo.GetAllEmployeesAsync();
            if (EmployeesList != null)
            {
                EmployeesView = CollectionViewSource.GetDefaultView(EmployeesList);
                EmployeesView.Filter = EmployeesFilter;
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
            return SelectedEmployee != null;
        }

        private async void DeleteAction(object parameter)
        {
            if (SelectedEmployee == null) return;

            EmployeesRepo employeesRepo = new EmployeesRepo();
            await employeesRepo.DeleteEmployee(SelectedEmployee.Id);
            await LoadEmployeesAsync();
        }

        private async void AddNewAction(object parameter)
        {
            if (!isNewEmployeeFilled(NewEmployee))
            {
                MessageBox.Show("Vyplňte všechna pole!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (NewEmployee.BirthNumber.Length < 10)
            {
                MessageBox.Show("Rodné číslo má méně než 10 znaků!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EmployeesRepo employeesRepo = new EmployeesRepo();
                NewEmployee.Salt = PasswordHasher.GenerateSalt();
                
                string hashedPassword = PasswordHasher.HashPassword(NewEmployee.Password, NewEmployee.Salt);
                NewEmployee.Password = hashedPassword;
                
                await employeesRepo.AddEmployee(NewEmployee);
                await LoadEmployeesAsync();
                NewEmployee = new Employee();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedEmployee!= null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;

            EditEmployeeVM editVM = new EditEmployeeVM(SelectedEmployee);
            EditEmployeeDialog editDialog = new EditEmployeeDialog(editVM);

            editDialog.ShowDialog();

            LoadEmployeesAsync();
            
        }
        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView EmployeesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                EmployeesView.Refresh();
            }
        }

        private bool EmployeesFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var employee = item as Employee;
            if (employee == null) return false;

            return employee.FirstName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || employee.LastName.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || employee.Id.ToString().Contains(_searchText)
                || employee.BirthNumber.ToString().Contains(_searchText)
                || SexEnumParser.GetStringFromEnumEnglish(employee.Sex).StartsWith(_searchText, StringComparison.OrdinalIgnoreCase)
                || employee.RoleID.ToString().Contains(_searchText)
                || employee.IdOfSuperiorEmployee.ToString().Contains(_searchText)
                || employee._foto.Id.ToString().Contains(_searchText);
        }
        //FILTER/////////////////////////////////////////////////////////////////////

        private bool isNewEmployeeFilled(Employee e)
        {
            return e != null
                && !string.IsNullOrEmpty(e.FirstName) && !string.IsNullOrWhiteSpace(e.FirstName)
                && !string.IsNullOrEmpty(e.LastName) && !string.IsNullOrWhiteSpace(e.LastName)
                && !string.IsNullOrEmpty(e.UserName) && !string.IsNullOrWhiteSpace(e.UserName)
                && !string.IsNullOrEmpty(e.BirthNumber) && !string.IsNullOrWhiteSpace(e.BirthNumber)
                && e.Sex != null
                && e._department != null && e._department.Id != null
                && e._foto != null && e._foto.Id != 0
                && !string.IsNullOrEmpty(e.Password) && !string.IsNullOrWhiteSpace(e.Password)
                && !string.IsNullOrEmpty(e.RoleID.ToString()) && !string.IsNullOrWhiteSpace(e.RoleID.ToString());
        }
    }
}
