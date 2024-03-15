using DocumentsManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DocumentsManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _13_NguyenQuangSu_TranLongKhanh_ProjectContext context = new _13_NguyenQuangSu_TranLongKhanh_ProjectContext();
            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            var docs = context.Documents.Where(d => d.UserId == userId).OrderByDescending(d => d.LastModifier).ToList();
            //ViewBag.Docs = docs;
            return View(docs);
        }
        public IActionResult EditView(int FileId)
        {
            _13_NguyenQuangSu_TranLongKhanh_ProjectContext context = new _13_NguyenQuangSu_TranLongKhanh_ProjectContext();
            //Console.WriteLine(FileId);
            var document = context.Documents.SingleOrDefault(d => d.FileId == FileId);
            return View(document);
        }
        [HttpPost]
        public IActionResult EditView(int FileId, string FileName, string FileContent)
        {
            _13_NguyenQuangSu_TranLongKhanh_ProjectContext context = new _13_NguyenQuangSu_TranLongKhanh_ProjectContext();
            var document = context.Documents.SingleOrDefault(doc => doc.FileId == FileId);
            if (document != null)
            {
                saveOldVersion(document);
                document.FileName = FileName;
                document.FileContent = FileContent;
                document.LastModifier = DateTime.Now;
                context.SaveChanges();

            }
            return View(document);
        }
        public void saveOldVersion(Document d)
        {
            _13_NguyenQuangSu_TranLongKhanh_ProjectContext context = new _13_NguyenQuangSu_TranLongKhanh_ProjectContext();
            Models.Version v = new Models.Version
            {
                DocId = d.FileId,
                UpdatedContent = d.FileContent,
                UpdatedTime = DateTime.Now,
            };
            context.Versions.Add(v);
            context.SaveChanges();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateViewModel create)
        {
            int user = int.Parse(HttpContext.Session.GetString("userId"));
            if (ModelState.IsValid)
            {
                Document d = new Document
                {
                    FileName = create.FileName,
                    FileContent = create.FileContent,
                    FileStatus = false,
                    UserId = user,
                    LastModifier = DateTime.Now,
                };
                _13_NguyenQuangSu_TranLongKhanh_ProjectContext context = new _13_NguyenQuangSu_TranLongKhanh_ProjectContext();
                context.Documents.Add(d);
                context.SaveChanges();
                return RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.CreateFailed = "File name and file content is not null";
            }
            return View();
        }

        public IActionResult Delete(int FileID)
        {
            _13_NguyenQuangSu_TranLongKhanh_ProjectContext context = new _13_NguyenQuangSu_TranLongKhanh_ProjectContext();
            int userid = int.Parse((HttpContext.Session.GetString("userId")));
            var doc = context.Documents.SingleOrDefault(d => d.FileId == FileID && d.UserId == userid);
            if (doc != null)
            {
                context.SaveChanges();
                context.Documents.Remove(doc);
                context.SaveChanges();
                return RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.DeleteFailed = "Delete failed : Docs not exist";
            }
            return View("Index");
        }
        public IActionResult Sort(string sort)
        {
            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            _13_NguyenQuangSu_TranLongKhanh_ProjectContext context = new _13_NguyenQuangSu_TranLongKhanh_ProjectContext();
            if (sort.Equals("dateDesc"))
            {
                var docs = context.Documents.Where(d => d.UserId == userId).OrderByDescending(d => d.LastModifier).ToList();
                return View("Index", docs);
            }
            else if (sort.Equals("dateAsc"))
            {
                var docs = context.Documents.Where(d => d.UserId == userId).OrderBy(d => d.LastModifier).ToList();
                return View("Index", docs);
            }
            else if (sort.Equals("nameAsc"))
            {
                var docs = context.Documents.Where(d => d.UserId == userId).OrderBy(d => d.FileName).ToList();
                return View("Index", docs);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
            
        }
        [HttpPost]
        public IActionResult search(string searchValue)
        {
            _13_NguyenQuangSu_TranLongKhanh_ProjectContext context = new _13_NguyenQuangSu_TranLongKhanh_ProjectContext();
            var doc = context.Documents.Where(d => d.FileName.Contains(searchValue));
            ViewBag.Search = "search";
            return View("Index", doc);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
