using MVVMFramework;
using PasswordManagerApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PasswordManagerUI
{
    public class PasswordGenerationViewModel : ViewModel, IPasswordGenerationViewModel
    {
        private PasswordManagerUser selectedUser;
        private PasswordProperties selectedPassword;
        private string masterPassword;
        private string generatedPassword;
        private FunctionCommand generatePasswordCommand;

        public ICommand CreateUserCommand { get; private set; }
        public ICommand CreatePasswordCommand { get; private set; }

        public ObservableCollection<PasswordManagerUser> Users { get; internal set; }
        public PasswordManagerUser SelectedUser 
        { 
            get => selectedUser;
            set
            {
                SetField(ref selectedUser, value);
                SelectedPassword = selectedUser.Passwords.First();
            }
        }
        public PasswordProperties SelectedPassword { get => selectedPassword; set => SetField(ref selectedPassword, value); }
        public string MasterPassword { get => masterPassword; set => SetField(ref masterPassword, value); }
        public string GeneratedPassword { get => generatedPassword; set => SetField(ref generatedPassword, value); }
        public bool RequireSpecial 
        { 
            get => SelectedPassword?.MustHaveSpc ?? false;
            set
            {
                SelectedPassword.MustHaveSpc = value;
                OnPropertyChanged(() => RequireSpecial);
                OnPropertyChanged(() => NoSpecialAllowed);
            }
        }
        public bool NoSpecialAllowed
        {
            get => SelectedPassword.MustNotHaveSpc;
            set
            {
                SelectedPassword.MustNotHaveSpc = value;
                OnPropertyChanged(() => RequireSpecial);
                OnPropertyChanged(() => NoSpecialAllowed);
            }
        }

        public ICommand GeneratePasswordCommand
        {
            get
            {
                if(generatePasswordCommand == null)
                {
                    generatePasswordCommand = new FunctionCommand(
                        p => GeneratedPassword = selectedPassword.GeneratePassword(masterPassword),
                        p => selectedPassword != null);
                }
                return generatePasswordCommand;
            }
        }
    }
}
