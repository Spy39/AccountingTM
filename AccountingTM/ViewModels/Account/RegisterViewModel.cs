using Accounting.Models;

namespace AccountingTM.ViewModels.Account
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public Role Role { get; set; }
    }
}
