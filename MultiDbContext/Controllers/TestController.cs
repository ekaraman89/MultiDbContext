using Microsoft.AspNetCore.Mvc;
using MultiDbContext.Data;

namespace MultiDbContext.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    private readonly IProductService _productService;

    public TestController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("Write")]
    public IActionResult Get(string name)
    {
        _productService.AddProduct(name);
        return Ok();
    }

    [HttpGet("Read")]
    public IActionResult Get()
    {
        var t = _productService.GetAll();
        return Ok(t);
    }
}