using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerDataLayer.WebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            var entities = _customerRepository.ReadAll();
            var customers = entities.Select(entity => (Customer)entity).ToList();

            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var customer = (Customer?)_customerRepository.Read(id);

            return View(customer);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                _customerRepository.Create(customer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = (Customer?)_customerRepository.Read(id);

            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                _customerRepository.Update(customer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = (Customer?)_customerRepository.Read(id);

            return View(customer);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                _customerRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
