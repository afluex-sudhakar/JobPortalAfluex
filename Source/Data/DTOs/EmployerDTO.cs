using System.Collections.Generic;
using System;
using System.Linq;
using System.Web;
using Data;
namespace Data.DTOs
{
    public class EmployerDTO 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Mobile2 { get; set; }
        public string Gender { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public string HusbandName { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int PinCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Photo { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string ContactPersonName { get; set; }

        public string OfficialEmailId { get; set; }
        public int? NoOfEmployees { get; set; }
        public string About { get; set; }
        public int CityId { get; set; }
        public string Logo { get; set; }
        public int RoleId { get; set; }
        public User User { get; set; } 
        public string Subject { get; set; }
        public string EmailMessage { get; set; }
        public int? UserId { get; set; }
        public List<Chat> EmpChatList { get; set; }
        public UserDetail UserDetail { get; set; }
        public List<UserDocument> UserDocument { get; set; }
        public string CompanyCertificate { get; set; }
        public string Aadhar { get; set; }
        public string PAN { get; set; }
    } 
    public class EmployerListDTO
    {
        public List<User> Employees { get; set; } 
    }

}
