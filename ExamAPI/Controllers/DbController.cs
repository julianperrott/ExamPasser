using ExamAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class AppController<T> : ControllerBase where T : class, IDataAccessObject
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected DBService<T> _dbService;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [HttpGet]
    public virtual async Task<List<T>> Get() => await _dbService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public virtual async Task<ActionResult<T>> Get(string id)
    {
        var item = await _dbService.GetAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public virtual async Task<IActionResult> Post(T newDog)
    {
        newDog.Id = string.Empty;

        await _dbService.CreateAsync(newDog);

        return CreatedAtAction(nameof(Get), new { id = newDog.Id }, newDog);
    }

    [HttpPut("{id:length(24)}")]
    public virtual async Task<IActionResult> Update(string id, T updatedDog)
    {
        var item = await _dbService.GetAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        updatedDog.Id = item.Id;

        await _dbService.UpdateAsync(id, updatedDog);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public virtual async Task<IActionResult> Delete(string id)
    {
        var item = await _dbService.GetAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        await _dbService.RemoveAsync(id);

        return NoContent();
    }
}