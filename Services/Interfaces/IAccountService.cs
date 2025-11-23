using TodoListMVC.Data.Entities;
using TodoListMVC.Models;
namespace TodoListMVC.Services.Interfaces
{
    public interface IAccountService
    {
        // Trả về UsUser nếu đăng nhập đúng, null nếu sai
        Task<UsUser> ValidateUserAsync(LoginViewModel model);
    }
}
