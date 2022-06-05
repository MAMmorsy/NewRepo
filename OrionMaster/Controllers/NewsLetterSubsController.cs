using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrionMaster.Models;
using OrionMaster.Models.Infrastrcture;

namespace OrionMaster.Controllers
{
    public class NewsLetterSubsController : Controller
    {
        private readonly OrionContext _context;

        public NewsLetterSubsController(OrionContext context)
        {
            _context = context;
        }

        // GET: NewsLetterSubs
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsLetterSubs.ToListAsync());
        }
        [HttpPost]
        public IActionResult AjaxMethod(string sessionName)
        {
            return Json(HttpContext.Session.GetString(sessionName));
        }

        public async Task<IActionResult> Sub(string email)
        {
            //HttpContext.Session.SetString("section", "news");
            NewsLetterSub newsLetter = _context.NewsLetterSubs.Where(n => n.Email == email).FirstOrDefault();
            string msg = IsValidEmail(email);
            //HttpContext.Session.SetString("msg", "email already exists");
            if (newsLetter == null)
            {
                //HttpContext.Session.SetString("msg", "Subscribe");
                NewsLetterSub newsLetterSub = new NewsLetterSub();
                newsLetterSub.Email = email;
                _context.Add(newsLetterSub);
                await _context.SaveChangesAsync();
                msg = "Subscribed Successfully";
            }
            else
            {
                msg = "Email Already Exists";
                
            }
            return Json(msg);
        }

        // GET: NewsLetterSubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLetterSub = await _context.NewsLetterSubs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsLetterSub == null)
            {
                return NotFound();
            }

            return View(newsLetterSub);
        }

        // GET: NewsLetterSubs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsLetterSubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Email,Datein,Deleted")] NewsLetterSub newsLetterSub)
        public async Task<IActionResult> Create(NewsLetterSub newsLetterSub)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("section", "news");
                NewsLetterSub newsLetter = _context.NewsLetterSubs.Where(n => n.Email == newsLetterSub.Email).FirstOrDefault();
                string emai = newsLetterSub.Email;
                    string msg = IsValidEmail(emai);
                HttpContext.Session.SetString("msg", "email already exists");
                if (newsLetter == null)
                {
                    HttpContext.Session.SetString("msg", "Subscribe");
                    _context.Add(newsLetterSub);
                    await _context.SaveChangesAsync();
                    return Redirect(newsLetterSub?.returnUrl ?? "/home");

                    //return RedirectToAction(nameof(Index));
                }
                else
                {
                    HttpContext.Session.SetString("msg", "email already exists");
                    //ModelState.AddModelError("", "Email address is already subscribed");
                    //return View();
                    //return RedirectToAction(nameof(Index));
                    return Redirect(newsLetterSub?.returnUrl ?? "/home");
                }
            }
            ModelState.AddModelError("", "Email address is already subscribed");
            return View();
            //if (ModelState.IsValid)
            //{
            //    NewsLetterSub newsLetterSub = new NewsLetterSub();
            //    string Email = formCol["Email"];
            //    if (Email.Trim() != "")
            //    {
            //        string msg = IsValidEmail(Email);
            //        if (msg == "")
            //        {
            //            //NewsLetterSub newsLetterSub = new NewsLetterSub();
            //            newsLetterSub.Email = formCol["Email"];
            //            newsLetterSub.returnUrl= formCol["returnUrl"];
            //            //newsLetterSub.Email = Email;
            //            _context.Add(newsLetterSub);
            //            await _context.SaveChangesAsync();
            //            return Redirect(newsLetterSub?.returnUrl ?? "/home");
            //            //return RedirectToAction(nameof(Index));
            //        }
            //        else
            //        {
            //            ViewData["errorMsg"] = msg;
            //            return Redirect(newsLetterSub?.returnUrl ?? "/home");
            //            //return RedirectToAction(nameof(Index));
            //        }
            //    }
            //}
            //return View();
        }

        string IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return "enter your email";
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return "";
            }
            catch
            {
                return "enter a valid email";
            }
        }

        // GET: NewsLetterSubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLetterSub = await _context.NewsLetterSubs.FindAsync(id);
            if (newsLetterSub == null)
            {
                return NotFound();
            }
            return View(newsLetterSub);
        }

        // POST: NewsLetterSubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Datein,Deleted")] NewsLetterSub newsLetterSub)
        {
            if (id != newsLetterSub.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsLetterSub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsLetterSubExists(newsLetterSub.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsLetterSub);
        }

        // GET: NewsLetterSubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLetterSub = await _context.NewsLetterSubs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsLetterSub == null)
            {
                return NotFound();
            }

            return View(newsLetterSub);
        }

        // POST: NewsLetterSubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsLetterSub = await _context.NewsLetterSubs.FindAsync(id);
            _context.NewsLetterSubs.Remove(newsLetterSub);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsLetterSubExists(int id)
        {
            return _context.NewsLetterSubs.Any(e => e.Id == id);
        }
    }
}
