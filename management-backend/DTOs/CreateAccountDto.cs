using System.ComponentModel.DataAnnotations;

namespace AccountManager.API.DTOs
{
    public class CreateAccountDto
    {
        [Required]
        public string AccountName { get; set; }

        [Range(0, double.MaxValue)]
        public decimal InitialBalance { get; set; }
    }
}
