namespace AccountingTM.ViewModels.Account
{
	public class UserPAgeViewModel
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string? FatherName { get; set; }
		public string FullName => $"{LastName} {FirstName} {FatherName}";
		/// <summary>Должность</summary>
		public string? Position { get; set; }
		public string? Role { get; set; }
	}
}
