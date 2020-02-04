using Ninject;
using Ninject.MockingKernel.Moq;
using NUnit.Framework;
using PasswordManagerApp;
using PasswordManagerUI;
using System.Collections.ObjectModel;
using System.Linq;
using TestCommonLibrary;

namespace PasswordManagerUITests
{
    public class PasswordGenerationViewModelTests
    {
        private readonly MoqMockingKernel _automock = new MoqMockingKernel();
        private PasswordGenerationViewModel _vm;
        private readonly System.Collections.Generic.List<string> _propertiesChanged = new System.Collections.Generic.List<string>();

        [SetUp]
        public void Setup()
        {
            _automock.Reset();
            _propertiesChanged.RemoveAll(x => true);
        }

        [Test]
        public void SelectedUserChanges_NotifyEventRaised_SelectedPasswordUpdates()
        {
            _vm = GetVmInstance();
            _vm.SelectedUser = TestPasswordManagerUsers.Instance.Users.First();

            CollectionAssert.Contains(_vm.SelectedUser.Passwords, _vm.SelectedPassword);
            CollectionAssert.Contains(_propertiesChanged, "SelectedUser");
            CollectionAssert.Contains(_propertiesChanged, "SelectedPassword");
        }

        private PasswordGenerationViewModel GetVmInstance()
        {
            var vm = _automock.Get<PasswordGenerationViewModel>();
            vm.PropertyChanged += (o, e) => _propertiesChanged.Add(e.PropertyName);
            return vm;
        }
    }
}