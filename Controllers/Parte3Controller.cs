﻿using Microsoft.AspNetCore.Mvc;
using ProvaPub.Data.Models;
using ProvaPub.Services;
using System;
using System.Threading.Tasks;

namespace ProvaPub.Controllers
{
    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// 
    /// Outra parte importante é em relação à data (OrderDate) do objeto Order. Ela deve ser salva no banco como UTC mas deve retornar para o cliente no fuso horário do Brasil. 
    /// Demonstre como você faria isso.
    /// </summary>
    /// 
    [ApiController]
    [Route("[controller]")]
    public class Parte3Controller : ControllerBase
    {
        private readonly OrderService _orderService;

        public Parte3Controller(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders")]
        public async Task<Order> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            var order = await _orderService.PayOrder(paymentMethod, paymentValue, customerId);

            // Converter OrderDate para horário: (UTC-3)
            order.OrderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            return order;
        }
    }
}