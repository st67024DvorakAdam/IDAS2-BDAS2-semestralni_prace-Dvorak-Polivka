﻿using Database_Hospital_Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Employee
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long BirthNumber { get; set; }
        public SexEnum Sex { get; set; }
        public Department _department { get; set; }

        public Employee() { }

        public Employee(int id, string firstName, string lastName, int birthNumber, SexEnum sex)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthNumber = birthNumber;
            Sex = sex;
        }
    }

    
}
