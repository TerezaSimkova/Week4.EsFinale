using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week4.EsFinale.Core.Interfaces;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IMainBL mainBusinessLayer;

        public CustomerController(IMainBL mainBusinessLayer)
        {
            this.mainBusinessLayer = mainBusinessLayer;
        }


        // GET
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = mainBusinessLayer.FetchCustomers();
            return Ok(customers);
        }
        // GET by id
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customers = mainBusinessLayer.GetCustomerById(id);
            return Ok(customers);
        }

        //POST
        [HttpPost]
        public IActionResult PostCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer non valido!");
            }
            bool isAdded = mainBusinessLayer.CreateCustomer(customer);
            if (!isAdded)
            {
                return StatusCode(500, "Customer non puo essere salvato");
            }

            return CreatedAtAction("PostCustomer", customer);
        }

        //DELETE
        [HttpDelete("{codiceCustomer}")]
        public IActionResult Delete(string codiceCustomer, int id)
        {
            if (codiceCustomer == null)
            {
                return BadRequest("Codice non valido!");
            }
            var getcustomerByCode = mainBusinessLayer.GetCustomerByCodice(codiceCustomer);
            var customerToDelete = true;
            if (getcustomerByCode != null)
            {
                customerToDelete = mainBusinessLayer.DeleteCustomer(getcustomerByCode.Id);

            }
            return Ok(customerToDelete);
        }

        // PUT 
        [HttpPut("{codiceCustomer}")]
        public IActionResult Put(string codiceCustomer, [FromBody] Customer customer)
        {
            var customerCode = mainBusinessLayer.GetCustomerByCodice(codiceCustomer);

            if (customerCode != null)
            {
                mainBusinessLayer.EditCustomer(customer, customerCode);
            }

            return Ok(customer);
        }
    }
}
