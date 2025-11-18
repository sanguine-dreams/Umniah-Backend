using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[DisplayName("Order Management")]
public class OrderController(ICrudService<InputOrder, OutputOrder> orderService) : ControllerBase
{
    [HttpGet]
    [DisplayName("Get All Orders")]
    public async Task<IActionResult> GetAllOrders()
    {
        var response = await orderService.GetAll();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost]
    [DisplayName("Create Order")]
    public async Task<IActionResult> CreateOrder([FromBody] InputOrder input)
    {
        var response = await orderService.Create(input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    [DisplayName("Edit Order")]
    public async Task<IActionResult> EditOrder(Guid id, [FromBody] InputOrder input)
    {
        var response = await orderService.Edit(id, input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [DisplayName("Delete Order")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var response = await orderService.Delete(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("{id}")]
    [DisplayName("Get Order By ID")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var response = await orderService.GetById(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}