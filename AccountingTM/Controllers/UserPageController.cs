using Accounting.Data;
using Accounting.Models;
using AccountingTM.Domain;
using AccountingTM.ViewModels.UserPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    [Authorize]
    public class UserPageController : Controller
    {
        private readonly DataContext _context;

        private readonly ICurrentUserManager _currentUserManager;

        public UserPageController(DataContext context, ICurrentUserManager currentUserManager)
        {
            _context = context;
            _currentUserManager = currentUserManager;
        }


        [HttpGet]
        public IActionResult UserPage()
        {
            var login = _currentUserManager.Login;
            User user = _context.Users.Include(x => x.Employee).Include(x => x.Role).First(x => x.Login == login);
            var model = new UserPageViewModel
            {
                UserId = user.Id,
                Login = user.Login,
                Position = user?.Employee?.Position,
                FullName = user.FullName,
                Rights = user.Role.Name
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Login == username);
            if (user == null)
            {
                return NotFound();
            }

            // Здесь можно добавить дополнительную проверку, хотя Compare уже обеспечит валидацию
            if (model.NewPassword != model.ConfirmPassword)
            {
                return BadRequest(new { message = "Пароли не совпадают" });
            }

            // Обновление пароля. В реальном приложении не стоит хранить пароль в открытом виде.
            user.Password = model.NewPassword;
            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok(new { message = "Пароль успешно изменен" });
        }
    }
}