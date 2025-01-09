namespace AccountingTM.Domain.Models
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
