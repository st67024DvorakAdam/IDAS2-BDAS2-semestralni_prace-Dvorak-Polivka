﻿using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Database_Hospital_Application.Models.Repositories
{
    public class RolesRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Role> roles { get; set; }

        public RolesRepo()
        {
            roles = new ObservableCollection<Role>();
        }

        public async Task<ObservableCollection<Role>> GetAllRoleDescriptionsAsync()
        {
            ObservableCollection<Role> roles = new ObservableCollection<Role>();
            string commandText = "get_all_roles";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);


            if (result.Rows.Count > 0)
            {
                if (roles == null)
                {
                    roles = new ObservableCollection<Role>();
                }

                foreach (DataRow row in result.Rows)
                {

                    Role role = new Role
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV"].ToString()
                    };

                    roles.Add(role);
                }
            }
            return roles;
        }

    }
}
