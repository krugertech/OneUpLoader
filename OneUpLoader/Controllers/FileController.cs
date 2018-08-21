using System;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;

namespace OneUpLoader.Controllers
{
    [AllowAnonymous]
    public class FileController : Controller
    {
        public FileController()
        {
            Directory.CreateDirectory(TempUploadPath);            
        }

        private static string TempUploadPath = HostingEnvironment.MapPath("~/Temp/");

        /// <summary>
        /// Note: An issue found with explorer locking media files
        /// as it creates a thumbnail for it. 
        /// Mpeg files will usually fail because of this.
        /// One method to work around this is to rename the file extension to .bak while uploading.
        /// </summary>
        /// <param name="fileName"></param>
        [HttpPost]         
        public virtual void Upload(string name)
        {
            // Read chunk and append to file.               
            using (var fs = new FileStream(System.IO.Path.Combine(TempUploadPath, name), FileMode.Append))
            {
                byte[] bytes = new byte[32768];
                int bytesRead;
                while ((bytesRead = Request.InputStream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    fs.Write(bytes, 0, bytesRead);
                }
            }
        }

        [HttpGet]
        public virtual ActionResult Details(string name, long size)
        {
            var file = new FileInfo(System.IO.Path.Combine(TempUploadPath, name));
            return Json(new { exists = (file.Exists) ? true : false, size = (file.Exists) ? file.Length : 0, hash = (file.Exists) ? FileHash.Get(file.FullName) : string.Empty }, JsonRequestBehavior.AllowGet);
        }

        // GET: File
        public ActionResult Index()
        {
            return View();
        }
    }

    public class FileHash
    {
        public static string Get(string filePath)
        {
            string hash = string.Empty;
            hash = GetHash(filePath);
            return hash;
        }

        private static string GetHash(string filePath)
        {
            try
            {
                byte[] hash;
                using (Stream input = File.OpenRead(filePath))
                {
                    hash = System.Security.Cryptography.MD5.Create().ComputeHash(input);
                }
                return BitConverter.ToString(hash).Replace("-", ""); // Hex
            }
            catch (Exception ex)
            {
                throw new Exception($"Error hashing file {filePath}. {ex}");
            }
        }
    }
}