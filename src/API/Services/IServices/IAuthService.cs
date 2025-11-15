using System.Threading.Tasks;
using API.DTOs.Requests;
using API.DTOs.Responses;

namespace API.Services.IServices;

public interface IAuthService
{
    Task RegisterAsync(UserRequest request);
    Task<LoginResponse> LoginAsync(UserRequest request);
}
