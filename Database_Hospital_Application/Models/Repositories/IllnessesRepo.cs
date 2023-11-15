﻿using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Models.Repositories
{
    public class IllnessesRepo
    {
        private DatabaseTools.DatabaseTools dbTools;

        public IllnessesRepo()
        {
            dbTools = new DatabaseTools.DatabaseTools();
            illnesses = new ObservableCollection<Illness>();
        }

        public ObservableCollection<Illness> illnesses { get; set; }

        public async Task<ObservableCollection<Illness>> GetIllnessesAsync()
        {
            string commandText = "get_all_illnesses";

            DataTable dataTable = await dbTools.ExecuteCommandAsync(commandText);

            if (dataTable.Rows.Count > 0)
            {
                illnesses.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    Illness illness = new Illness
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString()
                    };
                    illnesses.Add(illness);
                }
            }

            return illnesses;
        }

        public async Task AddIllness(Illness illness)
        {
            string commandText = "add_illness";
            var parameters = new Dictionary<string, object>
            {
                { "p_nazev", illness.Name }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

        }

        public async Task<int> DeleteIllness(int id)
        {
            string commandText = "delete_illness_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> UpdateIllness(Illness illness)
        {
            string commandText = "update_illness";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", illness.Id },
                { "p_nazev", illness.Name }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllContacts()
        {

        }
    }
}
