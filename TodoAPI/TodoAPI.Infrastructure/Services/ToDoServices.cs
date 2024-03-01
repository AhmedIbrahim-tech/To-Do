using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAPI.Domain.BaseResponse;
using TodoAPI.Domain.Interfaces;
using TodoAPI.Domain.Models;
using TodoAPI.DTOS;

namespace TodoAPI.Infrastructure.Services;

public interface IToDoServices
{
    Task<GenericBaseResponse<List<TodoDTO>>> GetAllCreatedTodos(int userId);
    Task<GenericBaseResponse<string>> GetTodosAsync();
    Task<GenericBaseResponse<TodoDTO>> GetTodoById(int id);
    Task<GenericBaseResponse<int>> CreateTodoAsync(TodoDTO dto);
    Task<GenericBaseResponse<int>> UpdateTodo(int id, TodoDTO dto);
    Task<GenericBaseResponse<int>> DeleteTodo(int id);
}

public class ToDoServices : GenericBaseResponseHandler, IToDoServices
{
    #region Constractor (s)
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _accessor;

    public ToDoServices(IUnitOfWork unitOfWork, IMapper mapper, IHttpClientFactory httpClientFactory, IHttpContextAccessor accessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpClientFactory = httpClientFactory;
        _accessor = accessor;
    }
    #endregion


    #region Get All Created Todos
    public async Task<GenericBaseResponse<List<TodoDTO>>> GetAllCreatedTodos(int userId)
    {
        try
        {
            var fetchedtodos = await _unitOfWork.Todo.GetTodosByUserId(userId);
            List<TodoDTO> todos = new List<TodoDTO>();
            if (fetchedtodos == null)
            {
                return NotFound<List<TodoDTO>>();
            }

            foreach (var item in fetchedtodos)
            {
                TodoDTO todo = _mapper.Map<Todo, TodoDTO>(item);
                todos.Add(todo);
            }
            return Success(todos);

        }
        catch (Exception ex)
        {
            return InternalServerError<List<TodoDTO>>(ex.Message);
        }
    }

    #endregion

    #region Get Todos
    public async Task<GenericBaseResponse<string>> GetTodosAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("todos");
            var response = await client.GetStringAsync("todos");
            return Success(response);

        }
        catch (Exception ex)
        {
            return InternalServerError<string>(ex.Message);
        }
    }

    #endregion

    #region Get Todo By Id
    public async Task<GenericBaseResponse<TodoDTO>> GetTodoById(int id)
    {
        try
        {
            if (id == null)
            {
                return InternalServerError<TodoDTO>("You must enter the id");
            }

            Todo _todo = await _unitOfWork.Todo.GetByIdAsync(id);
            if (_todo == null)
            {
                return NotFound<TodoDTO>("The entered id is not exist");
            }
            TodoDTO todo = _mapper.Map<Todo, TodoDTO>(_todo);
            return Success(todo);

        }
        catch (Exception ex)
        {
            return InternalServerError<TodoDTO>(ex.Message);
        }
    }

    #endregion

    #region Create Todos
    public async Task<GenericBaseResponse<int>> CreateTodoAsync(TodoDTO dto)
    {
        try
        {
            if (dto == null)
            {
                return BadRequest<int>("Error creating todo");
            }

            var todo = _mapper.Map<TodoDTO, Todo>(dto);

            // Parse the token and retrieve the userId
            var accessToken = _accessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Call a method to retrieve the decrypted userId from the token
            var userId = ExtractUserIdFromToken(accessToken ?? "");
            todo.CustomUserId = userId;

            var result = await _unitOfWork.Todo.AddAsync(todo);

            await _unitOfWork.CompleteAsync();

            return result > 0 ? Created(1) : BadRequest<int>("Error creating todo, check the data entered");

        }
        catch (Exception ex)
        {
            return InternalServerError<int>($"An error occurred: {ex.Message}");
        }
    }
    private int ExtractUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "userId");

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }

        // Return a default value or throw an exception as needed
        throw new ApplicationException("Invalid token or missing user ID claim.");
    }

    #endregion

    #region Update Todos
    public async Task<GenericBaseResponse<int>> UpdateTodo(int id, TodoDTO dto)
    {
        try
        {
            if (dto == null)
            {
                return BadRequest<int>("Error creating todo");
            }

            var fetchTodo = await _unitOfWork.Todo.GetByIdAsync(id);

            if (fetchTodo == null)
            {
                return BadRequest<int>("The chosen Todo not exist");
            }

            dto.id = fetchTodo.Id;
            dto.userId = fetchTodo.CustomUserId;
            Todo todo = _mapper.Map<TodoDTO, Todo>(dto);

            var result = await _unitOfWork.Todo.UpdateAsync(id, todo);

            await _unitOfWork.CompleteAsync();

            return result > 0 ? Updated(1) : BadRequest<int>("Error creating todo, check the data entered");

        }
        catch (Exception ex)
        {
            return InternalServerError<int>($"An error occurred: {ex.Message}");
        }
    }

    #endregion

    #region Delete Todos
    public async Task<GenericBaseResponse<int>> DeleteTodo(int id)
    {
        var fetchTodo = await GetTodoById(id);

        if (fetchTodo == null)
        {
            return BadRequest<int>("The chosen Todo not exist");
        }

        var result = await _unitOfWork.Todo.DeleteAsync(id);

        await _unitOfWork.CompleteAsync();

        return result > 0 ? Delete<int>() : BadRequest<int>("Error creating todo, check the data entered");

    }

    #endregion

}
