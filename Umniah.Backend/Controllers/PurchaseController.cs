using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Controllers;

[DisplayName("Purchase Management")]
[ApiController]
[Route("api/[controller]")]
public class PurchaseController(ICrudService<InputPurchase, OutputPurchase> purchaseService) : ControllerBase
{
    [HttpGet]
    [DisplayName("Get All Purchases")]
    public async Task<IActionResult> GetAllPurchases()
    {
        var response = await purchaseService.GetAll();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }


    [HttpPost]
    [DisplayName("Create Purchase")]
    public async Task<IActionResult>CreatePurchase([FromBody] InputPurchase input)
    {
       var response = await purchaseService.Create(input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
        
    }

    [HttpPut("{id}")]
    [DisplayName("Edit Purchase")] 
    public async Task<IActionResult> EditPurchase(Guid id, [FromBody] InputPurchase input)
    {
        var response = await purchaseService.Edit(id, input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [DisplayName("Delete Purchase")]
    public async Task<IActionResult> DeletePurchase(Guid id)
    {
        var response = await purchaseService.Delete(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("{id}")]
    [DisplayName("Get Purchase By ID")]
    public async Task<IActionResult> GetPurchaseById(Guid id)
    {
        var response = await purchaseService.GetById(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}