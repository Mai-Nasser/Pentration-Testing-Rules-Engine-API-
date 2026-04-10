namespace Decopia.API.Models
{
    public class User
    {
        public int Id { get; set; }                  
        public Guid PublicId { get; set; } = Guid.NewGuid(); 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }     
        public string Role { get; set; }            
        public bool IsActive { get; set; } = true;   
        public bool IsDeleted { get; set; } = false; 
        public DateTime? DeletedAt { get; set; }

         public string? ResetCode { get; set; }
        public DateTime? ResetCodeExpiry { get; set; }
    }
}
