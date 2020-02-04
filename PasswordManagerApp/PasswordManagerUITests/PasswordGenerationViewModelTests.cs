using Ninject;
using Ninject.MockingKernel.Moq;
using NUnit.Framework;
using PasswordManagerUI;

namespace PasswordManagerUITests
{
    public class PasswordGenerationViewModelTests
    {
        private readonly MoqMockingKernel _automock = new MoqMockingKernel();
        private PasswordGenerationViewModel _vm;

        [SetUp]
        public void Setup()
        {
            _automock.Reset();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}