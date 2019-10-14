using System;
using System.ComponentModel.DataAnnotations;

namespace betauia.Models
{
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; }
        
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        
        [Required]
        public string Token { get; set; }
        
        public string UserId { get; set; }
    }
}