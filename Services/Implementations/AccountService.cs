using Microsoft.EntityFrameworkCore;
using TodoListMVC.Data;
using TodoListMVC.Data.Entities;
using TodoListMVC.Models;
using TodoListMVC.Services.Implementations;
using TodoListMVC.Services.Interfaces;
using BCrypt.Net;
namespace TodoListMVC.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly TodoDbContext _context;

        // Hàm khởi tạo
        public AccountService(TodoDbContext context)
        {
            _context = context;
        }
        public async Task<UsUser?> ValidateUserAsync(LoginViewModel model)
        {
            // 1. Tìm user theo UserName và phải đang hoạt động (RowStatus = true)
            var user = await _context.UsUsers
                .FirstOrDefaultAsync(u => u.UserName == model.UserName && u.RowStatus == true);

            if (user == null) return null;

            // 2. Kiểm tra Password

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if (!isPasswordValid)
            {
                return null;
            }

            return user;
        }
    }
}
