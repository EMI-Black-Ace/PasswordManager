using MVVMFramework;
using NUnit.Framework;
using System;
using System.ComponentModel;

namespace MVVMTests
{
    public class ViewModelTests
    {
        private class TestViewModel : ViewModel
        {
            private string stringMember;
            public string StringProperty
            {
                get => stringMember;
                set { SetField(ref stringMember, value); }
            }

            public void ModifyStringMember(string toSet)
            {
                stringMember = toSet;
                OnPropertyChanged(() => StringProperty);
            }

            public void ModifyStringMemberImproperly()
            {
                OnPropertyChanged(() => StringProperty + "something else");
            }
        }

        private TestViewModel vm;
        private object lastObjectChanged;
        private string lastPropertyChanged;

        [SetUp]
        public void Setup()
        {
            vm = new TestViewModel();
            vm.StringProperty = "FirstValue";
            vm.PropertyChanged += LogLastPropertyChanged;
            lastObjectChanged = null;
            lastPropertyChanged = null;
        }

        private void LogLastPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            lastObjectChanged = sender;
            lastPropertyChanged = e.PropertyName;
        }

        [Test]
        public void ChangeStringProperty_NewValue_EventIsRaised()
        {
            Assert.IsNull(lastObjectChanged);
            Assert.IsNull(lastPropertyChanged);

            vm.StringProperty = "NewValue";

            Assert.AreSame(vm, lastObjectChanged);
            Assert.AreEqual(nameof(vm.StringProperty), lastPropertyChanged);
        }

        [Test]
        public void ChangeStringProperty_IdenticalValue_EventIsNotRaised()
        {
            Assert.IsNull(lastObjectChanged);
            Assert.IsNull(lastPropertyChanged);

            vm.StringProperty = "FirstValue";

            Assert.IsNull(lastObjectChanged);
            Assert.IsNull(lastPropertyChanged);
        }

        [Test]
        public void CallOnPropertyChanged_RaisesCorrectEvent()
        {
            Assert.IsNull(lastObjectChanged);
            Assert.IsNull(lastPropertyChanged);

            vm.ModifyStringMember("NewValue");

            Assert.AreSame(vm, lastObjectChanged);
            Assert.AreEqual(nameof(vm.StringProperty), lastPropertyChanged);
        }

        [Test]
        public void CallOnPropertyChanged_ImproperlyCalled_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => vm.ModifyStringMemberImproperly());
        }
    }
}