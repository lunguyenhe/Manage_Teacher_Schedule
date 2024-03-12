using Microsoft.AspNetCore.Mvc;

namespace Manage_Teacher_Schedule.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Upload()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(IFormFile upload)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        if (upload != null && upload.Length > 0)
        //        {

        //            if (upload.FileName.EndsWith(".csv"))
        //            {
        //                Stream stream = upload.InputStream;
        //                DataTable csvTable = new DataTable();
        //                using (CsvReader csvReader =
        //                    new CsvReader(new StreamReader(stream), true))
        //                {
        //                    csvTable.Load(csvReader);
        //                }
        //                return View(csvTable);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("File", "This file format is not supported");
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("File", "Please Upload Your file");
        //        }
        //    }
        //    return View();
        //}
    }
}
