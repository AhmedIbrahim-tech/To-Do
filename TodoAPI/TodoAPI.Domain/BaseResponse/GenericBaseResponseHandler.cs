using Microsoft.AspNetCore.Http;

namespace TodoAPI.Domain.BaseResponse;

public class GenericBaseResponseHandler
{

    public GenericBaseResponse<T> Delete<T>()
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = 200,
            Succeeded = true,
            Message = "Delete Successfully"
        };
    }

    public GenericBaseResponse<T> Success<T>(T entity, object Meta = null)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = 200,
            Succeeded = true,
            Message = "Load Data Successfully",
            Data = entity,
            Meta = Meta
        };
    }

    public GenericBaseResponse<T> Unauthorized<T>()
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = 401,
            Succeeded = true,
            Message = "You Don't Have Authorized"
        };
    }
    public GenericBaseResponse<T> BadRequest<T>(string Message = null)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = 400,
            Succeeded = false,
            Message = Message == null ? "The Function Not Found" : Message
        };
    }

    public GenericBaseResponse<T> UnprocessableEntity<T>(string Message = null)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = 422,
            Succeeded = false,
            Message = Message == null ? "Un-processable Entity" : Message
        };
    }

    public GenericBaseResponse<T> AlreadyExit<T>(string Message = null)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = 200,
            Succeeded = false,
            Message = Message == null ? "This's Item Already Exit" : Message
        };
    }

    public GenericBaseResponse<T> NotFound<T>(string message = null)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = 400,
            Succeeded = false,
            Message = message == null ? "The Function Not Found" : message
        };
    }

    public GenericBaseResponse<T> Created<T>(T entity, object Meta = null)
    {
        return new GenericBaseResponse<T>()
        {
            Data = entity,
            StatusCode = 200,
            Succeeded = true,
            Message = "Created Successfully",
            Meta = Meta
        };
    }

    public GenericBaseResponse<T> Updated<T>(T entity, object Meta = null)
    {
        return new GenericBaseResponse<T>()
        {
            Data = entity,
            StatusCode = 200,
            Succeeded = true,
            Message = "Updated Successfully",
            Meta = Meta
        };
    }

    public GenericBaseResponse<T> InternalServerError<T>(string exceptionMessage)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = 500,
            Succeeded = false,
            Message = exceptionMessage ?? "Some errors occurred"
        };
    }

    public GenericBaseResponse<T> EmailsError<T>(string exceptionMessage)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = 605,
            Succeeded = false,
            Message = exceptionMessage ?? "Some errors occurred"
        };
    }

}
