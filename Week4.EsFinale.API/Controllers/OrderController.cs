﻿using Microsoft.AspNetCore.Http;
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
    public class OrderController : ControllerBase
    {
        private readonly IMainBL mainBusinessLayer;

        public OrderController(IMainBL mainBusinessLayer)
        {
            this.mainBusinessLayer = mainBusinessLayer;
        }

        //Implementare le action -> chiamano metodi del business layer

        //Esempio nella GetOrders

        // GET: api/Order
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = mainBusinessLayer.FetchOrders();
            return Ok(orders);
        }

        // GET api/Order/5
        [HttpGet("{id}")]
        public IActionResult GetOrderBy(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id non valido!"); // 400 -> bad request
            }
            Order order = mainBusinessLayer.GetOrderById(id);
            if (order == null)
            {
                return NotFound("Non trovato"); // 400 -> not found
            }
            return Ok(order); // 200 -> found ok
        }

        // POST api/order
        [HttpPost]
        public IActionResult PostOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Ordine non valido!");
            }
            bool isAdded = mainBusinessLayer.CreateOrder(order);
            if (!isAdded)
            {
                return StatusCode(500, "Ordine non puo essere salvato");
            }

            return CreatedAtAction("PostOrder", order); // ->201 created
        }

        // PUT api/Order/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order order)
        {
            if (id <= 0 || order == null)
            {
                return BadRequest("Prestito invalido o inesistente!");
            }
            if (id != order.Id)
            {
                return BadRequest("Id non combaciono!");
            }

            var orderToUpdate = mainBusinessLayer.EditOrder(order);
            if (orderToUpdate == false)
            {
                return BadRequest("Prestito non puo essere modificato!");
            }
            return Ok(order);
        }

        // DELETE api/order/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id non valido!");
            }
            var orderToDelete = mainBusinessLayer.DeleteOrder(id);
            return Ok(orderToDelete);
        }

        
    }
}