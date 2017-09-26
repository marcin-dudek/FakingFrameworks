using NUnit.Framework;
using System;
using TheProject;
using NSubstitute;

namespace NSubstituteTests
{
    [TestFixture]
    public sealed class EmployeeProviderTests
    {
        public void Constructor_ThrowsExceptionIfSourceIsNull()
        {
            Assert.That(() => { new EmployeeProvider(null); }, Throws.ArgumentNullException);
        }

        public void Get_ReturnsSpecifiedEmployee()
        {
            ISource source = Substitute.For<ISource>();
            source.GetById(5).Returns(new Employee(5, "NSubstitute"));

            var provider = new EmployeeProvider(source);
            var employee = provider.Get(5);

            Assert.That(employee, Is.Not.Null);
            Assert.That(employee.Id, Is.EqualTo(5));
            Assert.That(employee.Name, Is.EqualTo("NSubsitute"));
        }

        public void Delete_EnsureThatMethodIsCalled()
        {
            ISource source = Substitute.For<ISource>();

            var provider = new EmployeeProvider(source);
            provider.Delete(5);

            source.Received(1).Delete(5);
        }

        public void Find_MethodNotCalled()
        {
            ISource source = Substitute.For<ISource>();

            var provider = new EmployeeProvider(source);

            Assert.That(() => { provider.Find(null); }, Throws.ArgumentNullException);
            source.DidNotReceiveWithAnyArgs().FindByName(null);
        }
    }
}
