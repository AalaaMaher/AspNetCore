using Microsoft.AspNetCore.Mvc;
using lab3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using lab3.Models;
namespace lab3.Controllers
{
    public class StudentController : Controller
    {
        private const string V = "valid";
        ITIdbcontextcs db =new ITIdbcontextcs();
        public IActionResult Index()
        {

         
            return View(db.Students.Include(a => a.department).ToList());
        }
        public IActionResult add ()
        {
            ViewBag.depts = new SelectList(db.Departments.ToList(), "ID", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult add (student st ,IFormFile stuimg)
        {
            if (stuimg == null)
                ModelState.AddModelError("", "profile image is Required");
            if (ModelState.IsValid)
            {
                db.Students.Add(st);
                db.SaveChanges();
                string[] arr = stuimg.FileName.Split('.');
                string filename = st.ID.ToString() + "." + arr[arr.Length - 1];
                using (var sm = new FileStream("./wwwroot/images/" + filename, FileMode.Create))
                {
                    stuimg.CopyTo(sm);
                }
                st.StuImg = filename;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.depts = new SelectList(db.Departments.ToList(), "ID", "Name",st.DeptNoo);
            return View(st);
        }
        public IActionResult checkusername (string username ,int ID)
        {
            student s = db.Students.Where(a=>a.ID !=ID).FirstOrDefault(s => s.UserName == username);
            if (s == null)
                return Json(true);
            else
                return Json(false);
        }
        public IActionResult details(int? id)
        {
            if(id==null)
                return NotFound();
            student std = db.Students.Include(a => a.department).SingleOrDefault(a => a.ID == id);
            if (std == null)
                return NotFound();

            return View (std);
        }
        public IActionResult delete (int? id)
        {
            if (id == null)
                return NotFound();
            student std = db.Students.Include(a => a.department).SingleOrDefault(a => a.ID == id);
            if (std == null)
                return NotFound();

            return View(std);

        }
        [ActionName("delete")]
        [HttpPost]
        public IActionResult confirmdelete(int? id)
        {
            if (id == null)
                return NotFound();
            student std = db.Students.Include(a => a.department).SingleOrDefault(a => a.ID == id);
            if (std == null)
                return NotFound();
            db.Students.Remove(std);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult edit(int?id)
        {
            if (id == null)
                return NotFound();
            student std = db.Students.Include(a => a.department).SingleOrDefault(a => a.ID == id);
            if (std == null)
                return NotFound();
            ViewBag.depts = new SelectList(db.Departments.ToList(), "ID", "Name", std.DeptNoo);
            return View(std);

        }
        
      
      [HttpPost]
        public IActionResult edit (student st, IFormFile stuimg,string username)
        
        {
           // if (st.UserName == username)
                //ModelState["username"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                db.Students.Update(st);
                db.SaveChanges();
            
                
                return RedirectToAction(nameof(Index));
            }
            ViewBag.depts = new SelectList(db.Departments.ToList(), "ID", "Name", st.DeptNoo);
            return View(st);

        }
        public IActionResult editimage(int id)
        {
            if (id == null)
                return NotFound();
            student std = db.Students.Include(a => a.department).SingleOrDefault(a => a.ID == id);
            if (std == null)
                return NotFound();
            ViewBag.depts = new SelectList(db.Departments.ToList(), "ID", "Name", std.DeptNoo);
            return View(std);
        }
        [ActionName("editimage")]
        [HttpPost]
        public IActionResult changeimage(int id , IFormFile stuimg)
        {
            if (id == null)
                return NotFound();
            student std = db.Students.Include(a => a.department).SingleOrDefault(a => a.ID == id);
            if (std == null)
                return NotFound();
           
            if (stuimg == null)
            {
                ModelState.AddModelError("", "profile image is Required");
              
                return View(std);

            }
            string[] arr = stuimg.FileName.Split('.');
            string filename = std.ID.ToString() + "." + arr[arr.Length - 1];
            using (var sm = new FileStream("./wwwroot/images/" + filename, FileMode.Create))
            {
                stuimg.CopyTo(sm);
            }
            std.StuImg = filename;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
       

    }
}
