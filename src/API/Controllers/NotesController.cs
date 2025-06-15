using System;
using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Exceptions;
using API.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class NotesController : ControllerBase
{
    private readonly INotesService _service;

    public NotesController(INotesService service)
    {
        _service = service;
    }

    [HttpDelete(Endpoints.Notes.Delete)]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _service.Delete(id);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet(Endpoints.Notes.Get)]
    public IActionResult Get() => Ok(_service.Get());

    [HttpGet(Endpoints.Notes.GetById)]
    public IActionResult Get(Guid id)
    {
        try
        {
            return Ok(_service.Get(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost(Endpoints.Notes.Create)]
    public IActionResult Post(CreateNoteRequest request)
    {
        GetNoteResponse response = _service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    [HttpPut(Endpoints.Notes.Update)]
    public IActionResult Put(Guid id, UpdateNoteRequest request)
    {
        try
        {
            _service.Update(id, request);
            return NoContent();
        }
        catch (BadRequestException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
