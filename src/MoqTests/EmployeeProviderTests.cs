using System;
using NUnit;
using Moq;
using NUnit.Framework;
using TheProject;

namespace MoqTests
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
            Mock<ISource> source = new Mock<ISource>();
            source.Setup(s => s.GetById(5)).Returns(new Employee(5, "Moq"));

            var provider = new EmployeeProvider(source.Object);
            var employee = provider.Get(5);

            Assert.That(employee, Is.Not.Null);
            Assert.That(employee.Id, Is.EqualTo(5));
            Assert.That(employee.Name, Is.EqualTo("Moq"));
        }

        public void Delete_EnsureThatMethodIsCalled()
        {
            Mock<ISource> source = new Mock<ISource>();

            var provider = new EmployeeProvider(source.Object);
            provider.Delete(5);

            source.Verify(s => s.Delete(5), Times.Once);
        }

        public void Find_MethodNotCalled()
        {
            Mock<ISource> source = new Mock<ISource>();

            var provider = new EmployeeProvider(source.Object);

            Assert.That(() => { provider.Find(null); }, Throws.ArgumentNullException);
            source.Verify(s => s.FindByName(null), Times.Never);
        }
    }
}
