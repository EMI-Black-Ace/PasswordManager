using MVVMFramework;
using PasswordManagerApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace PasswordManagerUI
{
    public class PasswordGenerationViewModel : ViewModel
    {
        private PasswordManagerUser selectedUser;
        private PasswordProperties selectedPassword;
        private string masterPassword;
        private string generatedPassword;
        private ICommand createUserCommand;  //TODO:  Replace with abstract
        private ICommand createPasswordCommand;  //TODO:  Replace with abstract
        private FunctionCommand generatePasswordCommand;

        public ObservableCollection<PasswordManagerUser> Users { get; internal set; }
        public PasswordManagerUser SelectedUser { get => selectedUser; set => SetField(ref selectedUser, value); }
        public PasswordProperties SelectedPassword { get => selectedPassword; set => SetField(ref selectedPassword, value); }
        public string MasterPassword { get => masterPassword; set => SetField(ref masterPassword, value); }
        public string GeneratedPassword { get => generatedPassword; set => SetField(ref generatedPassword, value); }

        public ICommand CreateUserCommand
        {
            get
            {
                if(createUserCommand == null)
                {
                    createUserCommand = null; //TODO:  User Creation Service
                }
                return createUserCommand;
            }
        }

        public ICommand CreateNewPasswordCommand
        {
            get
            {
                if(createPasswordCommand == null)
                {
                    createPasswordCommand = null; //TODO:  Password Creation Service
                }
                return createPasswordCommand;
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
