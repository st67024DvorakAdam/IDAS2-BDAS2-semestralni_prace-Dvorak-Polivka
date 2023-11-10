﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Tools
{
    public interface IDialogService
    {
        Task<bool> ConfirmAsync(string message, string title);
    }
}
