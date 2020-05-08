using MVVMFramework;
using PasswordManagerApp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PasswordManagerUI
{
    public interface IPasswordGenerationViewModel
    {
        PasswordManagerUser SelectedUser { get; set; }
        PasswordProperties SelectedPassword { get; set; }
        string MasterPassword { get; set; }
        string GeneratedPassword { get; set; }
        ICommand CreateUserCommand { get; set; }
        ICommand CreatePasswordCommand { get; set; }
        FunctionCommand GeneratePasswordCommand { get; set; }
    }
}
