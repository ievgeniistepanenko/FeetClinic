namespace BE.BE.Identity
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        
    }
}