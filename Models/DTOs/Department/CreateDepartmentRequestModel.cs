using MITCRMS.Models.DTOs.Users;

namespace MITCRMS.Models.DTOs.Department
{
    public class CreateDepartmentRequestModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public List<UserDto> Users { get; set; } = new();

    }
}
