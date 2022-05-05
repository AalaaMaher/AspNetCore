using Microsoft.AspNetCore.Mvc;
using lab3.Data;
using lab3.Models;
namespace lab3.Controllers
{
    public class DepartmentController : Controller
    {
       ITIdbcontextcs db =new ITIdbcontextcs();
        public IActionResult dowork()
        {
            department dept =db.Departments.SingleOrDefault(d=>d.ID == 1);
            student student = new student()
            {
                Name = "alaa",
                DeptNoo = dept.ID
            };
            db.Students.Add(student);
            db.SaveChanges();
            dept.Students.Add(student);

            return View();
        }
        public IActionResult Index()
        {
           List<department> dept = db.Departments.ToList();
          
            return View(dept);
        }
        public IActionResult add()
        {
            return View();  
        }
            
        [HttpPost]
        public IActionResult create(department dept)
        {
            db.Departments.Add(dept);
            db.SaveChanges();
            return RedirectToAction("Index");
          //  return View ("Index", db.Departments.ToList());
        }
       
        public IActionResult deptdelete (int id)
        {
            department dept = db.Departments.Find(id);
            db.Departments.Remove(dept);
            db.SaveChanges();
            return RedirectToAction("Index");
            //  return View ("Index", db.Departments.ToList());
        }
        public IActionResult showedit(int id )
        {
            department dept = db.Departments.Find(id);
            ViewData["dept"] = dept;
            return View();
        }
        
        public IActionResult edit (department dept)
        {
            
            department editdept = db.Departments.Find(dept.ID);
            editdept.Name=dept.Name;
            editdept.Capacity = dept.Capacity;
             db.SaveChanges();
            return RedirectToAction("Index");
            //  return View ("Index", db.Departments.ToList());
        }
        public IActionResult details(int id)
        {
            department dept = db.Departments.Find(id);
            ViewData["dept"] = dept;
            return View();

        }

    }
}
