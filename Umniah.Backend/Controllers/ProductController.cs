using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[DisplayName("Product Management")]
public class ProductController(ICrudService<InputProduct, OutputProduct> productService)  : ControllerBase
{
   [HttpGet]
    [DisplayName("Get All Products")]
    public async Task<IActionResult> GetAllProducts()
    {
        var response = await productService.GetAll();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost]
    [DisplayName("Create Product")]
    public async Task<IActionResult> CreateProduct([FromBody] InputProduct input)
    {
        var response = await productService.Create(input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    [DisplayName("Edit Product")]
    public async Task<IActionResult> EditProduct(Guid id, [FromBody] InputProduct input)
    {
        var response = await productService.Edit(id, input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    } 

    [HttpDelete("{id}")]
    [DisplayName("Delete Product")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var response = await productService.Delete(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("{id}")]
    [DisplayName("Get Product By ID")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var response = await productService.GetById(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}
