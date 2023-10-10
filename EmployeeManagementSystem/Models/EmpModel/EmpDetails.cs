using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models.EmpModel
{
    public class EmpDetails
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Dob { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Band { get; set; }
        public string Sex { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int MobileNo { get; set; }
        public string Email { get; set; }
        public string RHead { get; set; }
        public string Role { get; set; }
        public int[] countries { get; set; }
        public string imageData { get; set; }
        public string imagefile { get; set; }

    }
}
