﻿using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Database_Hospital_Application.Views.Doctor.Patient
{
    /// <summary>
    /// Interakční logika pro EditPersonalDetails.xaml
    /// </summary>
    public partial class EditPersonalDetails : Window
    {
        public EditPersonalDetails(PersonalDetailsVM viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}