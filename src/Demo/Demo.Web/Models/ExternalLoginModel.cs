using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Demo.Web.Models
{
    public class ExternalLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? ProviderDisplayName { get; set; }

        public string? ReturnUrl { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }
    }
}
