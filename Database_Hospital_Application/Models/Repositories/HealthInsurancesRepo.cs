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
    public class HealthInsurancesRepo
    {
        private DatabaseTools.DatabaseTools dbTools;

        public HealthInsurancesRepo()
        {
            dbTools = new DatabaseTools.DatabaseTools();
            healthInsurances = new ObservableCollection<HealthInsurance>();
        }

        public ObservableCollection<HealthInsurance> healthInsurances { get; set; }

        public async Task<ObservableCollection<HealthInsurance>> GetAllHealthInsurancesAsync()
        {
            string commandText = "get_all_health_insurances"; 
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            if (result.Rows.Count > 0)
            {
                healthInsurances.Clear(); 

                foreach (DataRow row in result.Rows)
                {
                    HealthInsurance healthInsurance = new HealthInsurance
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString(),
                        Code = Convert.ToInt32(row["ZKRATKA"])
                    };
                    healthInsurances.Add(healthInsurance);
                }
            }

            return healthInsurances;
        }

        public async Task AddHealthInsurance(HealthInsurance healthInsurance)
        {
            
        }

        public async Task DeleteHealthInsurance(int id)
        {

        }
        public void UpdateHealthInsurance(HealthInsurance healthInsurance)
        {

        }

        public void DeleteAllHealthInsurances()
        {

        }
    }
}
