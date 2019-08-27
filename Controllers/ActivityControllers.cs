using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using c_belt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http; 



namespace c_belt.Controllers
{
    [Route("")]
    // localhost:5000/Activity
    public class ActivityController : Controller
    {
        private int? SessionUser
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        



        
        private MyContext dbContext;
        public ActivityController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var x = dbContext.Hobbys
                .Include(p => p.Creator)
                .Include(p => p.Joiners)
                // ThenInclude() to get user name from user_id
                    .ThenInclude(v => v.xUser)
                .ToList() ;
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");


            return View(x);
        }






        [HttpGet("new")]
        public IActionResult New()
        {

            if(HttpContext.Session.GetInt32("UserId") == null)
                {
                    return RedirectToAction("Index", "Home");
                }
            var users = dbContext.Users.ToList();
            ViewBag.Users = users;
            return View();
            }






        [HttpPost("create")]
        public IActionResult CreatePost(Hobby newAct)
        {
  

            if(ModelState.IsValid)
            {
                bool notUnique = dbContext.Hobbys.Any(a => a.Name == newAct.Name);

                if(notUnique)
                {
                    ModelState.AddModelError("Name", "Name is in use");
                    return View("New");
                }

            if(HttpContext.Session.GetInt32("UserId") == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                newAct.UserId = (int)HttpContext.Session.GetInt32("UserId");
                dbContext.Hobbys.Add(newAct);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View("New");
        }







        [HttpGet("show/{ActId}")]
        public IActionResult Show(int ActId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var x = dbContext.Hobbys
                .Include(p => p.Creator)
                .Include(p => p.Joiners)
                // ThenInclude() to get user name from user_id
                    .ThenInclude(v => v.xUser)
                .FirstOrDefault(u => u.HobbyId == ActId);
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");


            return View(x);
            
            // ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
         
            // return View();
        }



        [HttpGet("edit/{ActId}")]

        public IActionResult Edit(int ActId)
        {
            Hobby somebody = dbContext.Hobbys.FirstOrDefault(u => u.HobbyId == ActId);

            if(somebody == null)

                return RedirectToAction("Index");

            return View(somebody);
        }



        [HttpPost("update/{ActId}")]
        public IActionResult Update(Hobby user, int ActId)
        {
            if(ModelState.IsValid)
            {

                Hobby toUpdate = dbContext.Hobbys.FirstOrDefault(u => u.HobbyId == ActId);

                if(toUpdate == null)
                    return RedirectToAction("Dashboard");

                toUpdate.Name = user.Name;
                toUpdate.Description = user.Description;



                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            Hobby somebody = dbContext.Hobbys.FirstOrDefault(u => u.HobbyId == ActId);
            return View("Edit", somebody);
        }



        [HttpGet("delete/{ActId}")]
        public IActionResult Delete(int ActId)
        {

            // query for thing
            Hobby toDel = dbContext.Hobbys.FirstOrDefault(v => v.HobbyId == ActId && v.UserId == (int)SessionUser);
            dbContext.Hobbys.Remove(toDel);
            dbContext.SaveChanges();
            return RedirectToAction("dashboard", "Activity");
        }




        }
    }


