using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace lab3.Models
{
    public class student
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Name should be more than 3 character")]
        [StringLength(20,MinimumLength =3)]
        public string Name { get; set; }
        public int Age { get; set; }
        
        public string StuImg { get; set; }
        [RegularExpression(@"[a-zA-Z0-9_]+@[A-Za-z0-9]+.[a-zA-Z]{2,3}",ErrorMessage ="email like :xyz123@gmail.com")]
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password",ErrorMessage ="password not matched ")]
        public string CPassword { get; set; }
        [Remote("checkusername","student",ErrorMessage ="username is used ",HttpMethod ="Post",AdditionalFields ="ID")]
        
        public string UserName { get; set; }
        [ForeignKey("department")]
        public int DeptNoo { get; set; }
        public virtual department department { get; set; }
        public student()
        {

        }
    }
}
