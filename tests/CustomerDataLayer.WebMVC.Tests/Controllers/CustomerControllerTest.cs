using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Integration.Tests.Repositories.Fixtures;
using CustomerDataLayer.Repositories;
using CustomerDataLayer.WebMVC.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CustomerDataLayer.WebMVC.Tests.Controllers
{
    public class CustomerControllerTest
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerController()
        {
            var controller = new CustomerController(new CustomerRepository());

            controller.Should().NotBeNull();
        }

        [Theory]
        [MemberData(nameof(GenerateMultipleCustomers))]
        public void ShouldBeAbleToListCustomers(List<Entity> customers)
        {
            var mockCustomerRepository = new Mock<CustomerRepository>();
            mockCustomerRepository.Setup(repository => repository.ReadAll()).Returns(customers);

            var controller = new CustomerController(mockCustomerRepository.Object);

            var actionResult = controller.Index();

            mockCustomerRepository.Verify(repository => repository.ReadAll(), Times.Once);
            ((ViewResult)actionResult).Model.Should().BeEquivalentTo(customers);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var mockCustomerRepository = new Mock<CustomerRepository>();
            mockCustomerRepository.Setup(repository => repository.Create(It.IsAny<Customer>())).Returns(1);

            var controller = new CustomerController(mockCustomerRepository.Object);

            controller.Create();

            var customer = CustomerRepositoryFixture.GetMinCustomer();
            var actionResult = controller.Create(customer);

            mockCustomerRepository.Verify(repository => repository.Create(It.Is<Customer>(x => x.Equals(customer))), Times.Once);
            ((RedirectToActionResult)actionResult).ActionName.Should().Be("Index");
        }

        [Fact]
        public void ShouldBeAbleToDetailCustomer()
        {
            var customer = CustomerRepositoryFixture.GetMinCustomer();
            customer.Id = 1;

            var mockCustomerRepository = new Mock<CustomerRepository>();
            mockCustomerRepository.Setup(r => r.Read(It.IsAny<int>())).Returns(customer);

            var controller = new CustomerController(mockCustomerRepository.Object);

            var actionResult = controller.Details(1);

            mockCustomerRepository.Verify(r => r.Read(It.Is<int>(i => i.Equals(1))), Times.Once);
            ((ViewResult)actionResult).Model.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public void ShouldBeAbleToEditCustomer()
        {
            var customer = CustomerRepositoryFixture.GetMinCustomer();
            customer.Id = 1;

            var mockCustomerRepository = new Mock<CustomerRepository>();
            mockCustomerRepository.Setup(r => r.Read(It.IsAny<int>())).Returns(customer);
            mockCustomerRepository.Setup(r => r.Update(It.IsAny<Customer>())).Returns(true);

            var controller = new CustomerController(mockCustomerRepository.Object);

            var actionResultGet = controller.Edit(1);

            mockCustomerRepository.Verify(r => r.Read(It.Is<int>(i => i.Equals(1))), Times.Once);
            ((ViewResult)actionResultGet).Model.Should().BeEquivalentTo(customer);

            var actionResultPost = controller.Edit(1, customer);

            mockCustomerRepository.Verify(r => r.Update(It.Is<Customer>(c => c.Equals(customer))), Times.Once);
            ((RedirectToActionResult)actionResultPost).ActionName.Should().Be("Index");
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            var customer = CustomerRepositoryFixture.GetMinCustomer();
            customer.Id = 1;

            var mockCustomerRepository = new Mock<CustomerRepository>();
            mockCustomerRepository.Setup(r => r.Read(It.IsAny<int>())).Returns(customer);
            mockCustomerRepository.Setup(r => r.Delete(It.IsAny<int>())).Returns(true);

            var controller = new CustomerController(mockCustomerRepository.Object);

            var actionResultGet = controller.Delete(1);

            mockCustomerRepository.Verify(r => r.Read(It.Is<int>(i => i.Equals(1))), Times.Once);
            ((ViewResult)actionResultGet).Model.Should().BeEquivalentTo(customer);

            var actionResultPost = controller.Delete(1, customer);

            mockCustomerRepository.Verify(r => r.Delete(It.Is<int>(i => i.Equals(1))), Times.Once);
            ((RedirectToActionResult)actionResultPost).ActionName.Should().Be("Index");
        }

        private static IEnumerable<object[]> GenerateMultipleCustomers()
        {
            yield return new object[] { Enumerable.Range(0, 1).Select(_ => (Entity)CustomerRepositoryFixture.GetMinCustomer()).ToList() };
            yield return new object[] { Enumerable.Range(0, 0).Select(_ => (Entity)CustomerRepositoryFixture.GetMinCustomer()).ToList() };
            yield return new object[] { Enumerable.Range(0, 3).Select(_ => (Entity)CustomerRepositoryFixture.GetMinCustomer()).ToList() };
        }
    }
}
