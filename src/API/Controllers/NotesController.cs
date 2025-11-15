using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Exceptions;
using API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INotesService _service;

    public NotesController(INotesService service)
    {
        _service = service;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    [HttpDelete(Endpoints.Notes.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        Guid userId = GetUserId();

        try
        {
            await _service.DeleteAsync(id, userId);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet(Endpoints.Notes.Get)]
    public async Task<IActionResult> Get()
    {
        Guid userId = GetUserId();
        IEnumerable<GetNoteResponse> response = await _service.GetAsync(userId);
        return Ok(response);
    }

    [HttpGet(Endpoints.Notes.GetById)]
    public async Task<IActionResult> Get(Guid id)
    {
        Guid userId = GetUserId();

        try
        {
            return Ok(await _service.GetAsync(id, userId));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost(Endpoints.Notes.Create)]
    public async Task<IActionResult> Post(NoteRequest request)
    {
        Guid userId = GetUserId();
        GetNoteResponse response = await _service.CreateAsync(request, userId);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    [HttpPut(Endpoints.Notes.Update)]
    public async Task<IActionResult> Put(Guid id, NoteRequest request)
    {
        Guid userId = GetUserId();
        try
        {
            await _service.UpdateAsync(id, request, userId);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
