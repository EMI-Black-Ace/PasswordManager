using MVVMFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMTests
{
    public class BuildCommandTests
    {
        public class IntBuilder : BuildCommand<int>
        {
            protected override int Build(object parameter)
            {
                return 0;
            }
        }

        [Test]
        public void BuildCommandExecute_RaisesEvent()
        {
            bool finished = false;
            var b = new IntBuilder();
            b.BuildFinished += i => finished = true;

            Assert.IsTrue(finished);
        }
    }
}
