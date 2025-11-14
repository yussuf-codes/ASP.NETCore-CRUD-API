using System;
using System.Collections.Generic;
using System.Security.Claims;
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
    public IActionResult Delete(Guid id)
    {
        Guid userId = GetUserId();

        try
        {
            _service.Delete(id, userId);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet(Endpoints.Notes.Get)]
    public IActionResult Get()
    {
        Guid userId = GetUserId();
        IEnumerable<GetNoteResponse> response = _service.Get(userId);
        return Ok(response);
    }

    [HttpGet(Endpoints.Notes.GetById)]
    public IActionResult Get(Guid id)
    {
        Guid userId = GetUserId();

        try
        {
            return Ok(_service.Get(id, userId));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost(Endpoints.Notes.Create)]
    public IActionResult Post(NoteRequest request)
    {
        Guid userId = GetUserId();
        GetNoteResponse response = _service.Create(request, userId);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    [HttpPut(Endpoints.Notes.Update)]
    public IActionResult Put(Guid id, NoteRequest request)
    {
        throw new NotImplementedException();
    }
}
