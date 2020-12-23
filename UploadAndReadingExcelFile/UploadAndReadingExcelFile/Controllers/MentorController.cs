using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadAndReadingExcelFile.Models;

namespace UploadAndReadingExcelFile.Controllers
{
    public class MentorController : Controller
    {
        [HttpGet]
        public IActionResult Index(List<Mentor> mentors = null)
        {

            mentors = mentors == null ? new List<Mentor>() : mentors;

            return View(mentors);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var mentors = this.GetMentorList(file.FileName);
            return Index(mentors);
        }

        private List<Mentor> GetMentorList(string fName)
        {
            List<Mentor> mentors = new List<Mentor>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        mentors.Add(new Mentor()
                        {
                            Id = reader.GetValue(0).ToString(),
                            Position = reader.GetValue(1).ToString(),
                            DateHired = reader.GetValue(2).ToString(),
                            OrganisationId = reader.GetValue(3).ToString(),
                            Firstname = reader.GetValue(4).ToString(),
                            Lastname = reader.GetValue(5).ToString(),
                            EmailAddress = reader.GetValue(6).ToString(),
                            ProfessionalNumber = reader.GetValue(7).ToString(),
                      
                        });
                    }
                }
            }

            return mentors;

        }
    }
}
