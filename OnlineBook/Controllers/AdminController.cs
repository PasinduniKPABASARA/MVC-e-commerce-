using OnlineBook.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;

namespace OnlineBook.Controllers
{
    public class AdminController : Controller
    {
        EBookpvtDBEntities db = new EBookpvtDBEntities();
        [HttpGet]
        // GET: Admin
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            Admin ad = db.Admins.Where(x => x.adminEmail == admin.adminEmail && x.adminpassword == admin.adminpassword).SingleOrDefault();
            if (ad != null)
            {
                Session["adminID"] = ad.adminId.ToString();
                Session["adminName"] = ad.adminFirstName.ToString();
                //ViewBag.error = "Valid User Email or Password";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Invalid User Email or Password";
                return View();
            }

        }
        public ActionResult Category()
        {
            if (Session["adminID"] == null)
            {
                return RedirectToAction("Login");

            }

            return View();
        }

        [HttpPost]

        public ActionResult Category(Category cat, HttpPostedFileBase imgfile)
        {

            Admin ad = new Admin();
            string path = uploadimage(imgfile);

            if (path.Equals("-1"))
            {
                ViewBag.error = "image could not be uploaded";

            }
            else
            {
                Category ca = new Category();
                ca.categoryName = cat.categoryName;
                ca.categoryImage = path;
                ca.categoryStatus = 1;
                db.Categories.Add(ca);


                db.SaveChanges();


                return RedirectToAction("Category");
            }


            return View();

        }


        public ActionResult ViewCategory(int? page)
        {
            int pagesize = 7, pageindex = 1;

            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Categories.Where(x => x.categoryStatus == 1).ToList();
            IPagedList<Category> cate = list.ToPagedList(pageindex, pagesize);
            return View(cate);
        }



        [HttpGet]
        public ActionResult CreateAd()
        {
            List<Category> li = db.Categories.ToList();
            ViewBag.categorylist = new SelectList(li, "categoryId", "categoryName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateAd(Book b, HttpPostedFileBase imgfile)
        {
            List<Category> li = db.Categories.ToList();
            ViewBag.categorylist = new SelectList(li, "categoryId", "categoryName");

            string path = uploadimage(imgfile);

            if (path.Equals("-1"))
            {
                ViewBag.error = "image could not be uploaded";

            }
            else

            {


                Book bk = new Book();
                bk.bookName = b.bookName;
                bk.bookAuthor = b.bookAuthor;
                bk.bookDescription = b.bookDescription;
                bk.bookPrice = b.bookPrice;
                bk.bookQuantity = b.bookQuantity;
                bk.bookImage = path;

                bk.categoryId = b.categoryId;

                // bk.categoryId = Convert.ToInt32(Session["u_id"].ToString());
                db.Books.Add(bk);
                db.SaveChanges();

                Response.Redirect("ViewCategory");
            }
            return View();

        }

        public ActionResult DisplayAd(int? id, int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.Books.Where(x => x.categoryId == id).OrderByDescending(x => x.bookId).ToList();
            IPagedList<Book> cate = list.ToPagedList(pageindex, pagesize);

            return View(cate);
        }


        public ActionResult ViewAdds(int? id, int? page)
        {

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

            return View(adm);



        }


        public string uploadimage(HttpPostedFileBase file)

        {

            Random r = new Random();

            string path = "-1";

            int random = r.Next();

            if (file != null && file.ContentLength > 0)

            {

                string extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))

                {

                    try

                    {



                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));

                        file.SaveAs(path);

                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);



                        //    ViewBag.Message = "File uploaded successfully";

                    }

                    catch (Exception ex)

                    {

                        path = "-1";

                    }

                }

                else

                {

                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");

                }

            }



            else

            {

                Response.Write("<script>alert('Please select a file'); </script>");

                path = "-1";

            }







            return path;


        }


        public ActionResult Delete(int? id)
        {
            Book b = db.Books.Where(x => x.bookId == id).SingleOrDefault();
            db.Books.Remove(b);
            db.SaveChanges();
            return View("ViewCategory");
        }

        public ActionResult DeleteCat(int? id)
        {
            Category b = db.Categories.Where(x => x.categoryId == id).SingleOrDefault();
            db.Categories.Remove(b);
            db.SaveChanges();
            return View("ViewCategory");
        }

        public ActionResult Customer()
        {
            User u = new User();
            List<User> list = db.Users.Where(x => x.userRole == "2").ToList();
            return View(list);
        }

        public ActionResult DeleteCust(int? id)
        {
            User u = db.Users.Where(x => x.userId == id).SingleOrDefault();
            db.Users.Remove(u);
            db.SaveChanges();
            return View("Customer");
        }

        public ActionResult Order()
        {
            List<Invoice> i = db.Invoices.ToList();
            return View(i);
        }

        public ActionResult ViewOrder(int id, string name)
        {
            List<Order> o = db.Orders.Where(x => x.invoiceId == id).ToList();

            Book book = new Book();

            foreach (var item in o)
            {
                book.bookId = (int)item.bookId;


            }

            List<Book> bk = new List<Book>();

            List<Invoice> i = db.Invoices.Where(x => x.invoiceId == id).ToList();

            return View(new OrderView { order = o, book = bk, invoice = i });
        }


        [HttpPost]

        public ActionResult Report()
        {

            List<Invoice> invoice = new List<Invoice>();
            invoice = db.Invoices.ToList();

            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            Chunk chunk = new Chunk("Invoice Summmary", FontFactory.GetFont("Arial", 20, Font.BOLD, BaseColor.BLACK));
            pdfDoc.Add(chunk);

            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);

            //Table
            PdfPTable table = new PdfPTable(4);
            PdfPCell cellAM = new PdfPCell();
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            cellAM.Phrase = new Phrase("Invoice ID");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Date");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Customer Name");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Total");
            table.AddCell(cellAM);


            foreach (var item in invoice)
            {
                cellAM.BackgroundColor = BaseColor.GRAY;
                cellAM.Phrase = new Phrase(item.invoiceId.ToString());
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.invoiceDate.ToString());
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.userName);
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.invoicetotal.ToString());
                table.AddCell(cellAM);
            }

            pdfDoc.Add(table);


            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Credit-Card-Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();


            return View();
        }

        [HttpPost]

        public ActionResult ReportBill(int ?id)
        {
            Invoice invoice = new Invoice();
            invoice = db.Invoices.Where(x => x.invoiceId == id).SingleOrDefault();
            List<Order> order = new List<Order>();
            order = db.Orders.Where(x => x.invoiceId == id).ToList();



            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            Chunk chunk = new Chunk("Order Summmary", FontFactory.GetFont("Arial", 20, Font.BOLD, BaseColor.BLACK));
            pdfDoc.Add(chunk);

            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);

            //Table
            PdfPTable table = new PdfPTable(8);
            PdfPCell cellAM = new PdfPCell();
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            cellAM.Phrase = new Phrase("Order ID");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Date");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Customer ID");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Book Name");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Invoice ID");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Order Quantity ");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Order Bill");
            table.AddCell(cellAM);
            cellAM.Phrase = new Phrase("Unit Price");
            table.AddCell(cellAM);

            foreach (var item in order)
            {
                cellAM.BackgroundColor = BaseColor.GRAY;
                cellAM.Phrase = new Phrase(item.orderId.ToString());
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.orderDate.ToString());
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.userId.ToString());
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.bookName.ToString());
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.invoiceId.ToString());
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.orderQty.ToString());
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.orderBill.ToString());
                table.AddCell(cellAM);

                cellAM.Phrase = new Phrase(item.orderUnitPrice.ToString());
                table.AddCell(cellAM);
            }

            pdfDoc.Add(table);


            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Credit-Card-Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();


            return View();
        }

        public ActionResult Index(int? id)
        {

            return View();
        }
        }
}