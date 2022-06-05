using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrionMaster.Models;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;

namespace OrionMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment Environment;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment _environment)
        {
            _logger = logger;
            this.Environment = _environment;
        }

        public IActionResult Index()
        {
            string lang = "En";
            if (HttpContext.Session.GetString("lang") != null)
                lang = HttpContext.Session.GetString("lang");
            
            Serializer ser = new Serializer();
            XMLToObject xMLToObject = new XMLToObject();

            ////Test Version
            //string FaqPath = string.Concat(this.Environment.WebRootPath, @"/App_GlobalResources/XmlResources/Test/" + lang + @"/FAQ.xml");

            //Live Version
            string FaqPath = string.Concat(this.Environment.WebRootPath, @"/App_GlobalResources/XmlResources/" + lang + @"/FAQ.xml");

            string FaqString = xMLToObject.ReturnDataNew(FaqPath);
            FAQ faq = ser.Deserialize<FAQ>(FaqString);

            //// Test Version
            //string FeaturesPath = string.Concat(this.Environment.WebRootPath, @"/App_GlobalResources/XmlResources/Test/" + lang + @"/Features.xml");

            //Live Version
            string FeaturesPath = string.Concat(this.Environment.WebRootPath, @"/App_GlobalResources/XmlResources/" + lang + @"/Features.xml");

            string FeaturesString = xMLToObject.ReturnDataNew(FeaturesPath);
            Features feature = ser.Deserialize<Features>(FeaturesString);
            dynamic mymodel = new ExpandoObject();
            mymodel.Faq = faq;
            mymodel.Features = feature;
            return View(mymodel);
        }

        public RedirectResult SetSession(string lang, string returnUrl)
        {
            HttpContext.Session.SetString("lang", lang);
            ////Test Version
            //return Redirect("/" + this.Environment.ApplicationName + "" + returnUrl);

            //Live Version
            return Redirect(returnUrl);
        }

        public IActionResult About()
        {
            string lang = "En";
            if (HttpContext.Session.GetString("lang") != null)
                lang = HttpContext.Session.GetString("lang");
            Serializer ser = new Serializer();
            XMLToObject xMLToObject = new XMLToObject();
            ////Test Version
            //string AboutUsPath = string.Concat(this.Environment.WebRootPath, @"/App_GlobalResources/XmlResources/Test/" + lang + @"/About.xml");

            //Live Version
            string AboutUsPath = string.Concat(this.Environment.WebRootPath, @"/App_GlobalResources/XmlResources/" + lang + @"/About.xml");

            string AboutUsString = xMLToObject.ReturnDataNew(AboutUsPath);
            AboutUs aboutUs = ser.Deserialize<AboutUs>(AboutUsString);
            //dynamic mymodel = new ExpandoObject();
            //mymodel.A = faq;
            //mymodel.Features = feature;
            return View(aboutUs);
        }

        public IActionResult Services()
        {
            string x = this.Environment.ApplicationName;
            string lang = "En";
            if (HttpContext.Session.GetString("lang") != null)
                lang = HttpContext.Session.GetString("lang");
            Serializer ser = new Serializer();
            XMLToObject xMLToObject = new XMLToObject();

            //// Test Version
            //string ServicesPath = string.Concat(this.Environment.WebRootPath, @"/App_GlobalResources/XmlResources/Test/" + lang + @"/Services.xml");

            //Live Version
            string ServicesPath = string.Concat(this.Environment.WebRootPath, @"/App_GlobalResources/XmlResources/" + lang + @"/Services.xml");

            string ServicesString = xMLToObject.ReturnDataNew(ServicesPath);
            OrionServices orionServices = ser.Deserialize<OrionServices>(ServicesString);
            return View(orionServices);
        }

        public IActionResult Partners()
        {
            return View();
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
