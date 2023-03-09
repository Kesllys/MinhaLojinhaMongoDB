using LojinhaServer.Collections;
using LojinhaServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LojinhaServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;
    public ProductsController(IProductRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var product = await _repo.GetAllAsync();
        return Ok(product);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);

    }

    [HttpPost]
    public async Task<IActionResult> Post(Product product)
    {
        await _repo.CreateAsync(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut]
    public async Task<IActionResult> Put(Product product)
    {
        var oldProduct = await _repo.GetByIdAsync(product.Id);
        if (oldProduct == null)
        {
            return NotFound();
        }

        await _repo.UpdateAsync(product);
        return NoContent();

    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

    await _repo.DeleteAsync(id);
    return NoContent();
    }
}
