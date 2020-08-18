using Ninject;
using Ninject.MockingKernel.Moq;
using NUnit.Framework;
using PasswordManagerApp;
using PasswordManagerUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestCommonLibrary;

namespace PasswordManagerUITests
{
    public class PasswordGenerationViewModelTests
    {
        private readonly MoqMockingKernel _automock = new MoqMockingKernel();
        private PasswordGenerationViewModel _vm;
        private readonly List<string> _propertiesChanged = new List<string>();

        [SetUp]
        public void Setup()
        {
            _automock.Reset();
        }

        #region property change notifications
        [Test]
        public void SelectedUserChanges_NotifyEventRaised_SelectedPasswordUpdates()
        {
            _vm = GetVmInstance();
            _vm.SelectedUser = TestPasswordManagerUsers.Instance.Users.First();

            CollectionAssert.Contains(_vm.SelectedUser.Passwords, _vm.SelectedPassword);
            CollectionAssert.Contains(_propertiesChanged, "SelectedUser");
            CollectionAssert.Contains(_propertiesChanged, "SelectedPassword");
        }

        [Test]
        public void SelectedPasswordChanges_NotifyEventRaised()
        {
            _vm = GetVmInstance();
            _vm.SelectedUser = TestPasswordManagerUsers.Instance.Users.First();
            _vm.SelectedPassword = _vm.SelectedUser.Passwords.First(p => p != _vm.SelectedPassword);

            CollectionAssert.Contains(_propertiesChanged, "SelectedPassword");
        }

        [Test]
        public void RequireSpecialChanged_ChangesNoSpc_EventsRaised()
        {
            _vm = GetVmInstance();
            _vm.SelectedUser = TestPasswordManagerUsers.Instance.Users.First();
            _vm.NoSpecialAllowed = true;
            _propertiesChanged.Clear();
            _vm.RequireSpecial = true;

            Assert.IsFalse(_vm.NoSpecialAllowed);
            CollectionAssert.Contains(_propertiesChanged, "RequireSpecial");
            CollectionAssert.Contains(_propertiesChanged, "NoSpecialAllowed");
        }

        [Test]
        public void NoSpecialChanged_ChangesReqSpc_EventsRaised()
        {
            _vm = GetVmInstance();
            _vm.SelectedUser = TestPasswordManagerUsers.Instance.Users.First();
            _vm.RequireSpecial = true;
            _propertiesChanged.Clear();
            _vm.NoSpecialAllowed = true;

            Assert.IsFalse(_vm.RequireSpecial);
            CollectionAssert.Contains(_propertiesChanged, "RequireSpecial");
            CollectionAssert.Contains(_propertiesChanged, "NoSpecialAllowed");
        }
        #endregion
        
        [Test]
        public void ClickCreatePassword_ValidUser_OpensService()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ClickCreatePassword_NoUserSelected_NothingHappens()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ClickCreateUser_OpensService()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ClickGeneratePassword_ValidPasswordSelected_GeneratesPassword()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ClickGeneratePassword_NoValidPasswordSelected_NothingHappens()
        {
            throw new NotImplementedException();
        }

        private PasswordGenerationViewModel GetVmInstance()
        {
            var vm = _automock.Get<PasswordGenerationViewModel>();
            vm.PropertyChanged += (o, e) => _propertiesChanged.Add(e.PropertyName);
            return vm;
        }
    }
}