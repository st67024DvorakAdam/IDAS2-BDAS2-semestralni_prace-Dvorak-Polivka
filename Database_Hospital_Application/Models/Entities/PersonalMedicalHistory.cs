﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class PersonalMedicalHistory //osobní anamnéza
    {
        public int Id { get; }
        public string Description {  get; set; } //Záznam
        public int IdOfPatient {  get; set; }
    }
}
