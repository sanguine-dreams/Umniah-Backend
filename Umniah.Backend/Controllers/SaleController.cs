using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Controllers;

[DisplayName("Sale Management")]
[ApiController]
[Route("api/[controller]")]
public class SaleController(ICrudService<InputSale, OutputSale> saleService)  : ControllerBase
{
    [HttpGet]
    [DisplayName("Get All Sales")]
    public async Task<IActionResult> GetAllSales()
    {
        var response = await saleService.GetAll();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost]
    [DisplayName("Create Sale")]
    public async Task<IActionResult> CreateSale([FromBody] InputSale input)
    {
        var response = await saleService.Create(input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    [DisplayName("Edit Sale")]
    public async Task<IActionResult> EditSale(Guid id, [FromBody] InputSale input)
    {
        var response = await saleService.Edit(id, input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [DisplayName("Delete Sale")]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        var response = await saleService.Delete(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("{id}")]
    [DisplayName("Get Sale By ID")]
    public async Task<IActionResult> GetSaleById(Guid id)
    {
        var response = await saleService.GetById(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}