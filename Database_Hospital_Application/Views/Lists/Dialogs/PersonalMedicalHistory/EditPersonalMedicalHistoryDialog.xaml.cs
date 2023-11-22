﻿using Database_Hospital_Application.ViewModels.Dialogs.Edit;
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

namespace Database_Hospital_Application.Views.Lists.Dialogs.PersonalMedicalHistory
{
    /// <summary>
    /// Interakční logika pro EditPersonalMedicalHistoryDialog.xaml
    /// </summary>
    public partial class EditPersonalMedicalHistoryDialog : Window
    {
        public EditPersonalMedicalHistoryDialog(EditPersonalMedicalHistoryVM viewModel)
        {
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => this.Close();
            InitializeComponent();
        }
    }
}
