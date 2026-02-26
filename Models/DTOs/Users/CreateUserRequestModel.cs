using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MITCRMS.Models.Entities;

namespace MITCRMS.Models.DTOs.Users
{
    public class CreateUserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<SelectListItem> Departments { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = [];
        //public List<SelectListItem> Roles { get; set; } = [];
        public Guid RoleId {  get; set; }
        public Guid DepartmentId { get; set; }
        public List<Guid> RoleIds { get; set; } = [];
    }
}
