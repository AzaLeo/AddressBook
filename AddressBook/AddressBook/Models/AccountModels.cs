using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace AddressBook.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required(ErrorMessageResourceName = "UserNameRequired", ErrorMessageResourceType = typeof(Resources.Models))]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Models))]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessageResourceName = "OldPasswordRequired", ErrorMessageResourceType = typeof(Resources.Models))]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", ResourceType = typeof(Resources.Models))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "NewPasswordRequired", ErrorMessageResourceType = typeof(Resources.Models))]
        [StringLength(100, ErrorMessageResourceName = "MinMaxLength", ErrorMessageResourceType = typeof(Resources.Models), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Models))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Models))]
        [Compare("NewPassword", ErrorMessageResourceName = "NewPasswordNotMatch", ErrorMessageResourceType = typeof(Resources.Models))]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "UserNameRequired", ErrorMessageResourceType = typeof(Resources.Models))]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Models))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resources.Models))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Models))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Models))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceName = "UserNameRequired", ErrorMessageResourceType = typeof(Resources.Models))]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Models))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resources.Models))]
        [StringLength(100, ErrorMessageResourceName = "MinMaxLength", ErrorMessageResourceType = typeof(Resources.Models), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Models))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Models))]
        [Compare("NewPassword", ErrorMessageResourceName = "PasswordNotMatch", ErrorMessageResourceType = typeof(Resources.Models))]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
