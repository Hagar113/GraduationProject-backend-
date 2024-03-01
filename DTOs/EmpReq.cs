using GraduationProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.DTOs
{
    public class EmpReq
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Nationality { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Contractdate { get; set; }
        public TimeSpan AttandaceTime { get; set; }
        public TimeSpan LeaveTime { get; set; }
        public int? deptid { get; set; }
        
        public int? GId { get; set; }
        public int? Sal_ID { get; set; }  
        public int? user_Id { get; set; }
        


    }
}
