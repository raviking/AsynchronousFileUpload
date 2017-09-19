using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsynchronousFileUpload.Models
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IEnumerable<HttpPostedFileBase> files)
        {
            int filecount = 0;
            string msg = "";
            try
            {
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Files"),filename);
                            file.SaveAs(path);
                            filecount++;
                        }
                    }
                    msg = filecount + " Files Uploaded Successfully";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}