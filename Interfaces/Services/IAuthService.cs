using ItiFinalProject.View_Model.User;

namespace ItiFinalProject.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterViewModel model);
        Task<AuthResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
