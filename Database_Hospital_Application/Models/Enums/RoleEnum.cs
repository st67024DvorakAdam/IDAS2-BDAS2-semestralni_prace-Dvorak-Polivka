﻿    using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Enums
{
    public enum RoleEnum
    {
        [Description("Administrátor")]
        Admin = 1,

        [Description("Doktor")]
        Doctor = 2,

        [Description("Sestra")]
        Nurse = 3,

        [Description("Asistent")]
        Assistant = 4,

        [Description("Host bez přihlášení")]
        Guest = 5

       
    }

    public static class RoleExtensions
    {
        public static string GetRoleDescription(int roleNumber)
        {
            foreach (RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
            {
                if ((int)role == roleNumber)
                {
                    return GetEnumDescription(role);
                }
            }
            return "Neznámá role";
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static RoleEnum GetRoleEnumFromId(int roleId)
        {
            foreach (RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
            {
                if ((int)role == roleId)
                {
                    return role;
                }
            }

            throw new Exception("Neznámá role, role_id neodpovídá žádnému enumu");
        }

        public static RoleEnum GetRoleEnumFromString(string input)
        {
            if (input.ToLower() == "admin" || input.ToLower() == "administrátor") { return RoleEnum.Admin; }
            else if (input.ToLower() == "doctor" || input.ToLower() == "doktor") { return RoleEnum.Doctor; }
            else if (input.ToLower() == "nurse" || input.ToLower() == "sestra" || input.ToLower() == "sestřička") { return RoleEnum.Nurse; }
            else if (input.ToLower() == "assistant" || input.ToLower() == "asistent") { return RoleEnum.Assistant; }
            else if (input.ToLower() == "host bez přihlášení" || input.ToLower() == "host" || input.ToLower() == "guest") { return RoleEnum.Guest; }
            else throw new Exception("Zadaný textový řetězec nelze převést na žádný z výčtových typů RoleEnum!");
        }


    }
}
