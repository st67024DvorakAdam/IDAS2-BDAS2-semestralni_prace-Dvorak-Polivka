﻿using Database_Hospital_Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Database_Hospital_Application.Views.Doctor
{
    /// <summary>
    /// Interakční logika pro NewPatientView.xaml
    /// </summary>
    public partial class NewPatientView : UserControl
    {
        public NewPatientView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInputForBirthNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 10 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForHouseNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 7 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForPostalCode(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 5 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForInsuranceCode(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 3 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void TextBox_PreviewTextInputForPhoneNumber(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string newText = textBox.Text + e.Text;
            e.Handled = newText.Length > 9 || !Regex.IsMatch(newText, "^[0-9]*$");
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is TextBox))
            {
                Keyboard.ClearFocus(); 
            } 
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
