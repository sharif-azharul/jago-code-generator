using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using System.Text;
using System.IO;

namespace AmarSomoy.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Basic_Usage_Submit(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    //using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"C:\amazon\sample.xlsx")))
                    using (ExcelPackage xlPackage = new ExcelPackage(file.InputStream))
                    {
                        var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                        var totalRows = myWorksheet.Dimension.End.Row;
                        var totalColumns = myWorksheet.Dimension.End.Column;

                        var sb = new StringBuilder(); //this is your your data
                        for (int rowNum = 1; rowNum <= totalRows; rowNum++) //selet starting row here
                        {
                            var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                            sb.AppendLine(string.Join(",", row));
                        }
                    }
                }
                //TempData["UploadedFiles"] = Basic_Usage_Get_File_Info(files);
            }

            return RedirectToAction("Result");
        }

        private IEnumerable<string> Basic_Usage_Get_File_Info(IEnumerable<HttpPostedFileBase> files)
        {
            return
                from a in files
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        }
    }
}