using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrionMaster.Models;


namespace OrionMaster.Controllers
{
    public class UserMessagesController : Controller
    {
        private readonly OrionContext _context;
        private IConfiguration Configuration;

        public UserMessagesController(OrionContext context, IConfiguration _configuration)
        {
            _context = context;
            Configuration = _configuration;
        }

        // GET: UserMessages
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserMessages.ToListAsync());
        }

        // GET: UserMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessages
                .FirstOrDefaultAsync(m => m.MsgId == id);
            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        public async Task<IActionResult> Contact(string name,string email,string phone,string subject,string msg)
        {
             MailSettings mailSettings = new MailSettings();
            mailSettings.Body = @"Hello, I am " + name + "<br/> my email address is : <a href=\"mailto:" + email + "\">" + email + "</a> " +
                "<br/> my Phone no. is : <a href=\"tel: " + phone + "\"> " + phone + " </a><br/>" + msg;
            mailSettings.Subject = name + " - " + subject;
            string fromAddress = this.Configuration.GetValue<string>("Smtp:FromAddress");
            string toAdress = this.Configuration.GetValue<string>("Smtp:ToAddress");
            string password = this.Configuration.GetValue<string>("Smtp:Password");
            mailSettings.Email = fromAddress;
            mailSettings.To = toAdress;
            mailSettings.Password = password;

            UserMessage userMessage = new UserMessage();
            if (SenEmail(mailSettings))
                userMessage.Status = 1;
            else
                userMessage.Status = 0;
            userMessage.Name = name;
            userMessage.Email = email;
            if (phone == null)
                phone = "";
            userMessage.Phone = phone;
            if (subject == null)
                subject = "";
            userMessage.Subject = subject;
            userMessage.MsgDetails = msg;
            string retval = "";
            //HttpContext.Session.SetString("msg", "email already exists");
            //if (newsLetter == null)
            {
                //HttpContext.Session.SetString("msg", "Subscribe");
               
                _context.Add(userMessage);
                await _context.SaveChangesAsync();
                retval = "Sent Successfully";
                //return Redirect(newsLetterSub?.returnUrl ?? "/home");

                //return RedirectToAction(nameof(Index));
            }
            //else
            //{
            //    retval = "email already exists";
            //    //HttpContext.Session.SetString("msg", "email already exists");
            //    ////ModelState.AddModelError("", "Email address is already subscribed");
            //    ////return View();
            //    ////return RedirectToAction(nameof(Index));
            //    //return Redirect(newsLetterSub?.returnUrl ?? "/home");
            //}
            //ModelState.AddModelError("", "Email address is already subscribed");
            //return View();
            return Json(retval);
        }

        public bool SenEmail(MailSettings model)
        {
            using (MailMessage mm = new MailMessage(model.Email, model.To))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                
                mm.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    string host = this.Configuration.GetValue<string>("Smtp:Server");
                    int port = this.Configuration.GetValue<int>("Smtp:Port");
                    smtp.Host = host;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
                    smtp.Credentials = NetworkCred;
                    smtp.Port = port;
                    try
                    {
                        smtp.Send(mm);
                    }
                    catch(Exception ex)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
        }

        // GET: UserMessages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MsgId,Name,Email,Phone,Subject,MsgDetails,Status,Datein,Deleted")] UserMessage userMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userMessage);
        }

        // GET: UserMessages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessages.FindAsync(id);
            if (userMessage == null)
            {
                return NotFound();
            }
            return View(userMessage);
        }

        // POST: UserMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MsgId,Name,Email,Phone,Subject,MsgDetails,Status,Datein,Deleted")] UserMessage userMessage)
        {
            if (id != userMessage.MsgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMessageExists(userMessage.MsgId))
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
            return View(userMessage);
        }

        // GET: UserMessages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMessage = await _context.UserMessages
                .FirstOrDefaultAsync(m => m.MsgId == id);
            if (userMessage == null)
            {
                return NotFound();
            }

            return View(userMessage);
        }

        // POST: UserMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMessage = await _context.UserMessages.FindAsync(id);
            _context.UserMessages.Remove(userMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMessageExists(int id)
        {
            return _context.UserMessages.Any(e => e.MsgId == id);
        }
    }
}
