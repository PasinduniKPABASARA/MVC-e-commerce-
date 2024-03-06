using OnlineBook.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBook.Controllers
{
    public class UserController : Controller
    {
        EBookpvtDBEntities db = new EBookpvtDBEntities();
        // GET: User
        public ActionResult Index(int? page)
        {
            TempData.Keep();
            if (TempData["cart"]!=null)
            {
                double x = 0;
                List<Cart> li2 = TempData["cart"] as  List<Cart>;
                foreach(var item in li2)
                {
                    x += Convert.ToInt32(item.orderBill);
                    TempData["total"] = x;
                }
               
            }
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Categories.Where(x => x.categoryStatus == 1).OrderByDescending(x => x.categoryId).ToList();
            IPagedList<Category> cate = list.ToPagedList(pageindex, pagesize);
            TempData.Keep();
            return View(cate);
        }

        public ActionResult Register()
        {
            TempData.Keep();
            return View();
        }

        [HttpPost]

        public ActionResult Register(User us)
        {
            TempData.Keep();

            {
                User u = new User();
                u.userFirstName = us.userFirstName;
                u.userLastName = us.userLastName;
                u.userNIC = us.userNIC;
                u.userDOB = us.userDOB;
                u.userPhone = us.userPhone;
                u.userEmail = us.userEmail;
                u.userpassword = us.userpassword;
                u.userAddress = us.userAddress;
                u.userRole = "2";
                db.Users.Add(u);
                db.SaveChanges();



                return RedirectToAction("Login");

            }
        }

        public ActionResult Login()
        {
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            TempData.Keep();
            User us = db.Users.Where(x => x.userEmail == user.userEmail && x.userpassword == user.userpassword).SingleOrDefault();
            if (us != null)
            {
                Session["userID"] = us.userId.ToString();
                //ViewBag.error = "Valid User Email or Password";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Invalid User Email or Password";
                return View();
            }

        }


        public ActionResult SignOut()
        {


            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index");
        }


        public ActionResult DisplayAd(int? id, int? page)
        {
            TempData.Keep();
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Books.Where(x => x.categoryId == id).OrderByDescending(x => x.bookId).ToList();
            IPagedList<Book> cate = list.ToPagedList(pageindex, pagesize);

            return View(cate);
        }



        public ActionResult ViewAdds(int ? id, int ? page)
        {

            TempData.Keep();
            Session["BookID"] = "";

            adViewModel adm = new adViewModel();

            Book p = db.Books.Where(x => x.bookId == id).SingleOrDefault();
            adm.bookId = p.bookId;
            adm.bookName = p.bookName;
            adm.bookAuthor = p.bookAuthor;
            adm.bookDescription = p.bookDescription;
            adm.bookPrice = p.bookPrice;
            adm.bookImage = p.bookImage;


            Category cat = db.Categories.Where(x => x.categoryId == p.categoryId).SingleOrDefault();
            adm.categoryName = cat.categoryName;

            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Feedbacks.Where(x => x.bookId == id).OrderByDescending(x => x.feddbackId).ToList();
            IPagedList<Feedback> cate = list.ToPagedList(pageindex, pagesize);


            Session["BookID"] = p.bookId.ToString();

            return View(new ProductFeedback() { aVM = adm, fdb = cate});

            

        }

        [HttpPost]
        public ActionResult ViewAdds(ProductFeedback fb)
        {
            TempData.Keep();

            Feedback f = new Feedback();
            f.feedbackDate = System.DateTime.Now;
            f.feedbackDescription = fb.aVM.feedbackDescription;
            f.bookId = Convert.ToInt32(Session["BookID"].ToString());
            f.userId = Convert.ToInt32(Session["userID"].ToString());
            int idu = Convert.ToInt32(Session["userID"].ToString());
            User u = db.Users.Where(x => x.userId == idu).SingleOrDefault();
            f.userName = u.userFirstName + " " + u.userLastName;

            db.Feedbacks.Add(f);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult DisplayAd(int? id, int? page, string search)
        {
            TempData.Keep();
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Books.Where(x => x.bookName.Contains(search)).OrderByDescending(x => x.bookId).ToList();
            IPagedList<Book> cate = list.ToPagedList(pageindex, pagesize);

            return View(cate);
        }

        public ActionResult AddToCart(int? id)
        {
            TempData.Keep();
            Book b = db.Books.Where(x => x.bookId == id).SingleOrDefault();
            return View(b );
        }

    

        List<Cart> li = new List<Cart>();
        [HttpPost]
        public ActionResult AddToCart(Book bk, string qty, int id)
        {
            Book b = db.Books.Where(x => x.bookId == id).SingleOrDefault();
            Cart c = new Cart();
            c.bookId = b.bookId;
            c.bookName = b.bookName;
            c.bookPrice = b.bookPrice;
            c.orderQty = Convert.ToInt32(qty);
            c.orderBill = b.bookPrice * c.orderQty;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;
            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                int flag = 0;
                foreach(var item in li2)
                {
                    if (item.bookId == c.bookId)
                    {
                        item.orderQty += c.orderQty;
                        item.orderBill += c.orderBill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);
                }
                TempData["cart"] = li2;
            }
            TempData.Keep();

            return RedirectToAction("Index");
        }

      

        public ActionResult Remove(int ? id)
        {
            List<Cart> li2 = TempData["cart"] as List<Cart>;
            Cart c = li2.Where(x => x.bookId == id).SingleOrDefault();
            li2.Remove(c);
            int h = 0;
            foreach(var item in li2)
            {
                h += Convert.ToInt32(item.orderBill);
            }

            TempData["total"] = h;

            return RedirectToAction("CheckOut");
        }

        public ActionResult CheckOut()
        {

            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            TempData.Keep();

            User u = new User();
            u.userId = Convert.ToInt32(Session["userID"].ToString());
            User un = db.Users.Where(x => x.userId == u.userId).SingleOrDefault();


            List<Cart> li = TempData["cart"] as List<Cart>;
            Invoice invoice = new Invoice();
            invoice.invoiceDate = System.DateTime.Now;
            invoice.userId = Convert.ToInt32(Session["userID"].ToString());
            invoice.invoicetotal = (double)TempData["total"];
            invoice.userName = un.userFirstName + " " + un.userLastName;
            db.Invoices.Add(invoice);
            db.SaveChanges();

            foreach (var item in li)
            {
                Book book = new Book();
                var name = db.Books.Select(b => new Book
                { //<--HERE
                    bookId = b.bookId,
                    bookName = b.bookName
                }).Where(x => x.bookId == item.bookId).SingleOrDefault();


                Order or = new Order();
                or.bookId = item.bookId;
                or.invoiceId = invoice.invoiceId;
                or.orderDate = System.DateTime.Now;
                or.orderQty = item.orderQty;
                or.orderUnitPrice = item.bookPrice;
                or.orderBill = item.orderBill;
                or.bookId = item.bookId;
                or.bookName = name.bookName;
                db.Orders.Add(or);
                db.SaveChanges();
            }
            TempData.Remove("total");
            TempData.Remove("cart");

            TempData["msg"] = "Transaction Successfully Completed...";
            TempData.Keep();
            return RedirectToAction("Index");
        }
    }
}