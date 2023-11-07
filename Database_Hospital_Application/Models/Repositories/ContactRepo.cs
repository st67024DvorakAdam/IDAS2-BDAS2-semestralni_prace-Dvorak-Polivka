﻿using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class ContactRepo
    {

        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Contact> contacts { get; set; }

        public ContactRepo()
        {
            contacts = new ObservableCollection<Contact>();
        }

        public async Task<ObservableCollection<Contact>> GetAllContactsAsync()
        {
            string commandText = "get_all_contacts";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            contacts.Clear(); // Vyčistíme stávající kolekci před načtením nových dat

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Contact contact = new Contact
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Email = (row["EMAIL"]).ToString(),
                        PhoneNumber = Convert.ToInt32(row["TELEFON"]),
                        IdsOfPatients = new ObservableCollection<int>(),
                        IdsOfEmployees = new ObservableCollection<int>()
                    };

                    string idsOfPatientsData = row["pacient_ids"].ToString();
                    if (!string.IsNullOrEmpty(idsOfPatientsData))
                    {
                        string[] idsOfPatientsArray = idsOfPatientsData.Split(',');
                        foreach(string id in idsOfPatientsArray)
                        {
                            contact.IdsOfPatients.Add(Convert.ToInt32(id));
                        }
                    }
                    contact.MakeStringVersionOfIdsOfPacients();


                    string idsOfEmployeesData = row["zamestnanec_ids"].ToString();
                    if (!string.IsNullOrEmpty(idsOfEmployeesData))
                    {
                        string[] idsOfEmployeesArray = idsOfEmployeesData.Split(',');
                        foreach (string id in idsOfEmployeesArray)
                        {
                            contact.IdsOfEmployees.Add(Convert.ToInt32(id));
                        }
                    }
                    contact.MakeStringVersionOfIdsOfEmployees();


                    contacts.Add(contact);
                }
            }
            return contacts;
        }

        public void AddContact(Contact contact)
        {

        }

        public void DeleteContact(int id)
        {

        }
        public void UpdateContact(Contact contact)
        {

        }

        public void DeleteAllContacts()
        {

        }
    }
}