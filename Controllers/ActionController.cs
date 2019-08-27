using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using c_belt.Models;
using Microsoft.AspNetCore.Http;

namespace c_belt.Controllers
{
    [Route("action")]
    public class ActionController : Controller
    {
        private int? SessionUser
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        MyContext dbContext;
        public ActionController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("{ActId}/Action/{z}/")]
        public IActionResult Action(int ActId, bool z)
        {
            
            Join newGuy = new Join()
            {
                HobbyId = ActId,
                join = z,
                UserId = (int)SessionUser,
               
            };

            dbContext.Joins.Add(newGuy);
            dbContext.SaveChanges();
            return RedirectToAction("dashboard", "Activity");
        }



        [HttpPost("create")]
        public IActionResult Joining(Join newAct)
        {
  

            if(HttpContext.Session.GetInt32("UserId") == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                newAct.UserId = (int)HttpContext.Session.GetInt32("UserId");
                dbContext.Joins.Add(newAct);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }





        [HttpGet("delete/{ActId}")]
        public IActionResult Remove(int ActId)
        {
            // query for thing
            Join toDel = dbContext.Joins.FirstOrDefault(v => v.HobbyId == ActId && v.UserId == (int)SessionUser);
            dbContext.Joins.Remove(toDel);
            dbContext.SaveChanges();
            return RedirectToAction("dashboard", "Activity");
        }
        






    }
}