using System;
using System.Collections.Generic;
using MITCRMS.Models.DTOs.Users;

namespace MITCRMS.Models.DTOs.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode {  get; set; }
        public List<UserDto> Users { get; set; } = new();

        public IEnumerable<DepartmentDto> Departments { get; set; }

        public string ErrorMessage { get; set; }
    }
}
