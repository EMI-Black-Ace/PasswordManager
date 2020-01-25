using PasswordManagerApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasswordManagerUI
{
    public class PasswordGenerationViewModel
    {
        public ObservableCollection<PasswordManagerUser> Users { get; internal set; }
    }
}
