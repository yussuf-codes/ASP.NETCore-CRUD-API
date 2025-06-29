using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services.IServices;

public interface IAuthService
{
    void Register(UserRequest request);
    LoginResponse Login(UserRequest request);
}
