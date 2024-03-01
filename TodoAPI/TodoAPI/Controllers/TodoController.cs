using TodoAPI.Domain.DTO.CustomResult;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Domain.Interfaces;
using TodoAPI.Domain.Models;
using AutoMapper;
using TodoAPI.DTOS;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using TodoAPI.Infrastructure.Services;

namespace TodoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IToDoServices _toDoServices;

        public TodoController(IToDoServices toDoServices)
        {
            _toDoServices = toDoServices;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetTodos()
        //{
        //    return Ok(await _toDoServices.GetTodosAsync());
        //}
        [HttpGet("search")]
        public async Task<IActionResult> GetAllCreatedTodos([FromQuery] int userId)
        {
            var Result = await _toDoServices.GetAllCreatedTodos(userId);

            return Result.Succeeded ? Ok(Result.Data) : BadRequest(Result.Message);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            return Ok(await _toDoServices.GetTodoById(id));
        }

        [HttpPost("CreateToDo")]
        public async Task<IActionResult> CreateTodoAsync([FromBody] TodoDTO todoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error creating todo: Model state is invalid.");
            }

            var createTodoResult = await _toDoServices.CreateTodoAsync(todoDto);

            return createTodoResult.Succeeded
                ? Ok(createTodoResult)
                : BadRequest(createTodoResult.Message);
        }



        [HttpPut("UpdateTodo/{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoDTO todoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error");
            }
            var updateTodoResult = await _toDoServices.UpdateTodo(id, todoDto);

            return updateTodoResult.Succeeded
                ? Ok(updateTodoResult.Data)
                : BadRequest(updateTodoResult.Message);

        }

        [HttpDelete("DeleteTodo/{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            return Ok(await _toDoServices.DeleteTodo(id));
        }


    }
}
