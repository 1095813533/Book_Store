using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Store.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;

namespace Book_Store.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public static void CreateJson(string path)//创建购物车Json
        {

            //found the file exist 
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            fs.Close();
        }
        public ActionResult Index()
        {
            book_store_db bookdb = new book_store_db();
            var books = bookdb.book.Select(m => m).ToList<book>();
            books = books.OrderByDescending(m => m.salenum).ToList();
            return View(books);
        }//主页
        public ActionResult search()
        {
            string search_text = Request["search_text"];
            book_store_db bookdb = new book_store_db();
            List<book> book = bookdb.book.Where(s => s.bookname.Contains(search_text)).Select(m => m).ToList();
            book = book.OrderByDescending(m => m.bookid).ToList();
            return View(book);
        }//搜索页
        public ActionResult search_type()
        {
            string search_text = Request["search_text"];
            book_store_db bookdb = new book_store_db();
            List<book> book = bookdb.book.Where(s => s.type.Contains(search_text)).Select(m => m).ToList();
            book = book.OrderByDescending(m => m.bookid).ToList();
            return View(book);
        }//搜索页
        public ActionResult login()
        {
            return View();
        }
        public string userlogin()//登陆
        {
            string type = Request["user-type"];
            if (type == "user")
            {
                string name = Request["name"];
                string password = Request["password"];
                book_store_db bookdb = new book_store_db();
                var user = bookdb.user.Where(u => u.username == name && u.password == password).SingleOrDefault();
                if (user == null)
                {
                    return "0";

                }
                else
                {
                    List<user> usercookie = bookdb.user.Where(u => u.username == name).Select(m => m).ToList();
                    HttpCookie cookieid = new HttpCookie("userid");
                    cookieid.Value = usercookie[0].userid.ToString();
                    Response.AppendCookie(cookieid);
                    HttpCookie cookiename = new HttpCookie("username");
                    cookiename.Value = usercookie[0].username;
                    Response.AppendCookie(cookiename);
                    HttpCookie cookiepassword = new HttpCookie("password");
                    cookiepassword.Value = usercookie[0].password;
                    Response.AppendCookie(cookiepassword);

                    return "1";
                    //登录成功跳转
                }
            }
            else
            {
                string name = Request["name"];
                string password = Request["password"];
                book_store_db bookdb = new book_store_db();
                var user = bookdb.admin.Where(u => u.adminname == name && u.password == password).SingleOrDefault();
                if (user == null)
                {
                    return "0";

                }
                else
                {
                    List<admin> usercookie = bookdb.admin.Where(u => u.adminname == name).Select(m => m).ToList();
                    HttpCookie cookieid = new HttpCookie("userid");
                    cookieid.Value = usercookie[0].adminid.ToString();
                    Response.AppendCookie(cookieid);
                    HttpCookie cookiename = new HttpCookie("username");
                    cookiename.Value = usercookie[0].adminname;
                    Response.AppendCookie(cookiename);
                    HttpCookie cookiepassword = new HttpCookie("password");
                    cookiepassword.Value = usercookie[0].password;
                    Response.AppendCookie(cookiepassword);
                    return "2";
                    //登录成功跳转
                }
            }
        }
        public string userreg()//注册帐号
        {
            string name = Request["name"];
            string password = Request["password"];
            string email = Request["email"];
            string phone = Request["phone"];
            book_store_db bookdb = new book_store_db();
            user users = new user();
            users.username = name;
            users.password = password;
            users.email = email;
            users.phone = phone;
            var user = bookdb.user.Where(u => u.username == name && u.password == password).SingleOrDefault();
            if (user == null)
            {
                bookdb.user.Add(users);
                try
                {
                    bookdb.SaveChanges();
                }
                catch (Exception ex)
                {
                    return name;
                }
            }
            else
            {
                return name;
            }
            
            List<user> usercookie = bookdb.user.Where(u => u.username == name).Select(m => m).ToList();
            string path = Server.MapPath("../shoppingcar/") + usercookie[0].userid.ToString() + ".txt";
            CreateJson(path);
            string path2 = Server.MapPath("../order/") + usercookie[0].userid.ToString();
            if (!Directory.Exists(path2))//如果不存在就创建 dir 文件夹  
                Directory.CreateDirectory(path2);
            HttpCookie cookieemail = new HttpCookie("email");
            cookieemail.Value = usercookie[0].email;
            Response.AppendCookie(cookieemail);
            HttpCookie cookieid = new HttpCookie("userid");
            cookieid.Value = usercookie[0].userid.ToString();
            Response.AppendCookie(cookieid);
            HttpCookie cookiename = new HttpCookie("username");
            cookiename.Value = usercookie[0].username;
            Response.AppendCookie(cookiename);
            HttpCookie cookiepassword = new HttpCookie("password");
            cookiepassword.Value = usercookie[0].password;
            Response.AppendCookie(cookiepassword);
            HttpCookie cookiephone = new HttpCookie("phone");
            cookiepassword.Value = usercookie[0].phone;
            Response.AppendCookie(cookiephone);
            return "1";
        }
        public string userreset()//重置密码
        {
            string email = Request["email"];
            string username = Request["name"];
            book_store_db bookdb = new book_store_db();
            var users = bookdb.user.Where(u => u.email == email && u.username == username).SingleOrDefault();

            if (users == null)
            {
                return "0";

            }
            else
            {
                users.password = "123456";
                bookdb.SaveChanges();
                return "1";
                //重置成功
            }
        }
        public ActionResult logout()
        {
            HttpCookie cok = Request.Cookies["userid"];
            HttpCookie cok2 = Request.Cookies["username"];
            if (cok != null)
            {
                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                cok.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在
                Response.AppendCookie(cok);
            }
            if (cok2 != null)
            {
                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                cok2.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在
                Response.AppendCookie(cok2);
            }
            return Content("<script>location.href='Index'</script>");
        }
        public JsonResult loadshoppingcar()
        {
            book_store_db bookdb = new book_store_db();
            int userid = Convert.ToInt32(Request["userid"]);
            string path = Server.MapPath("../shoppingcar/") + userid + ".txt";
            List<shoppingcaritem> js = new List<shoppingcaritem>();
            StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            string id = null;
            string counts = null;
            while (!sr.EndOfStream)
            {
                id = sr.ReadLine().ToString();
                var car = new shoppingcaritem();
                int bookid = Convert.ToInt32(id);
                counts = sr.ReadLine().ToString();
                car.id = bookid;
                car.goodsCount = Convert.ToInt32(counts);
                List<book> books = bookdb.book.Where(u => u.bookid == bookid).Select(m => m).ToList();
                car.imgUrl = books[0].image_url;
                car.price = books[0].price;
                car.singleGoodsMoney = car.price * car.goodsCount;
                car.goodsInfo = books[0].bookname;
                js.Add(car);
            }
            sr.Close();
            return Json(js, JsonRequestBehavior.AllowGet);
        }
        public string shoppingcardelete()
        {
            string userid = Request["userid"];
            string goodsid = Request["goods_id"];
            string path = Server.MapPath("../shoppingcar/") + userid + ".txt";
            StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            int i = 0;
            string id = null;
            while (!sr.EndOfStream)
            {
                id = sr.ReadLine().ToString();
                if (id == goodsid) break;
                string temp = sr.ReadLine().ToString();
                i = i + 2;
            }
            sr.Close();
            List<string> lines = new List<string>(System.IO.File.ReadAllLines(path));
            lines.RemoveAt(i+1);
            lines.RemoveAt(i);
            System.IO.File.WriteAllLines(path, lines.ToArray());
            return "1";
        }
        public ActionResult shoppingcar()
        {
            return View();
        }
        public string addshoppingcar()
        {
            string userid = "";
            try { userid = Request.Cookies["userid"].Value; }
            catch(Exception e) { return "0"; }
            string goodsid = Request["good_id"];
            string counts = Request["good_count"];
            string path = Server.MapPath("../shoppingcar/") + userid + ".txt";
            StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            int i = 0;
            string id = null;
            while (!sr.EndOfStream)
            {
                id = sr.ReadLine().ToString();
                if (id == goodsid) { sr.Close(); return "1"; }
                i++;
            }
            sr.Close();
            List<string> lines = new List<string>(System.IO.File.ReadAllLines(path));
            System.IO.File.AppendAllText(path, goodsid + '\n');
            System.IO.File.AppendAllText(path, counts + '\n');
            return "1";
        }
        public string settlement()
        {
            string userid = Request["userid"];
            string datet = DateTime.Now.ToLocalTime().ToString();
            string dt = null;
            for(int i = 0; i < datet.Length; i++)
            {
                if (datet[i] >= '0' && datet[i] <= '9')
                {
                    dt = dt + datet[i];
                }
            }
            string orderid = userid + dt;
            string orderurl = Server.MapPath("../order/") + userid + "\\" + orderid + ".txt";
            string consignee = Request["consignee"];
            string phone = Request["phone"];
            string address = Request["address"];
            double totalprice = Convert.ToDouble(Request["total_money"]);
            FileStream fs = new FileStream(orderurl, FileMode.OpenOrCreate);
            fs.Close();
            book_store_db bookdb = new book_store_db();
            order od = new order();
            od.address = address;
            od.name = consignee;
            od.orderid = orderid;
            od.orderurl = orderurl;
            od.phone = phone;
            od.status = false;
            od.time = DateTime.Now.ToLocalTime();
            od.totalprice = totalprice;
            od.userid = Convert.ToInt32(userid);
            string orderlist = " {\"Total\":\"0\",\"Rows\":" + Request["goods_list"] + "}";
            JObject json1 = (JObject)JsonConvert.DeserializeObject(orderlist);
            JArray array = (JArray)json1["Rows"];
            foreach (var jObject in array)
            {
                string id = jObject["id"].ToString();
                string goodscounts = jObject["goodsCount"].ToString();
                int bookid = Convert.ToInt32(id);
                int counts = Convert.ToInt32(goodscounts);
                List<book> bookcheck = bookdb.book.Where(u => u.bookid == bookid).Select(m => m).ToList();
                if (bookcheck[0].stock < counts) { return "0"; }
            }
            bookdb.order.Add(od);
            bookdb.SaveChanges();
            foreach (var jObject in array)
            {
                string id = jObject["id"].ToString();
                string goodscounts = jObject["goodsCount"].ToString();
                int bookid = Convert.ToInt32(id);
                int counts = Convert.ToInt32(goodscounts);
                System.IO.File.AppendAllText(orderurl, id + '\n');
                System.IO.File.AppendAllText(orderurl, goodscounts + '\n');
                var books = bookdb.book.Where(u => u.bookid == bookid).SingleOrDefault();
                books.stock = books.stock - counts;
                books.salenum = books.salenum + counts;
                bookdb.SaveChanges();
            }
            System.IO.File.AppendAllText(orderurl, "\n");
            return "1";
        }
        public string settlement2()
        {
            string userid = "";
            try { userid = Request.Cookies["userid"].Value; }
            catch (Exception e) { return "0"; }
            string datet = DateTime.Now.ToLocalTime().ToString();
            string dt = null;
            for (int i = 0; i < datet.Length; i++)
            {
                if (datet[i] >= '0' && datet[i] <= '9')
                {
                    dt = dt + datet[i];
                }
            }
            string orderid = userid + dt;
            string orderurl = Server.MapPath("../order/") + userid + "\\" + orderid + ".txt";
            string consignee = Request["consignee"];
            string phone = Request["phone"];
            string address = Request["address"];
            double price = Convert.ToDouble(Request["good_price"]);

            int bookid = Convert.ToInt32(Request["good_id"]);
            int counts = Convert.ToInt32(Request["good_count"]);

            double totalprice = counts * price;

            FileStream fs = new FileStream(orderurl, FileMode.OpenOrCreate);
            fs.Close();
            book_store_db bookdb = new book_store_db();
            order od = new order();
            od.address = address;
            od.name = consignee;
            od.orderid = orderid;
            od.orderurl = orderurl;
            od.phone = phone;
            od.status = false;
            od.time = DateTime.Now.ToLocalTime();
            od.totalprice = totalprice;
            od.userid = Convert.ToInt32(userid);
            List<book> bookcheck = bookdb.book.Where(u => u.bookid == bookid).Select(m => m).ToList();
            if (bookcheck[0].stock < counts) { return "2"; }
            bookdb.order.Add(od);
            bookdb.SaveChanges();
            System.IO.File.AppendAllText(orderurl, bookid.ToString() + '\n');
            System.IO.File.AppendAllText(orderurl, counts.ToString() + '\n');
            var books = bookdb.book.Where(u => u.bookid == bookid).SingleOrDefault();
            books.stock = books.stock - counts;
            books.salenum = books.salenum + counts;
            bookdb.SaveChanges();
            System.IO.File.AppendAllText(orderurl, "\n");
            return "1";
        }
        public ActionResult bookedit()
        {
            return View();
        }
        public JsonResult adminbooksearch(int limit, int offset, string bookname, string type)
        {
            book_store_db bookdb = new book_store_db();
            var lstRes = new List<book>();
            if (bookname != "" && type != "")
            {
                lstRes = bookdb.book.Where(s => s.bookname.Contains(bookname) && s.type == type).Select(m => m).ToList();
            }
            else if (bookname == "" && type == "")
            {
                lstRes = bookdb.book.Select(m => m).ToList();
            }
            else if (bookname != "")
            {
                lstRes = bookdb.book.Where(s => s.bookname.Contains(bookname)).Select(m => m).ToList();
            }
            else
            {
                lstRes = bookdb.book.Where(s => s.type == type).Select(m => m).ToList();
            }
            var total = lstRes.Count;
            var rows = lstRes.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        public string Base64ToImage(string base64Str, string path, string imgName)
        {
            //声明一个string类型的相对路径
            string filename = "";
            //取图片的后缀格式
            string hz = base64Str.Split(',')[0].Split(';')[0].Split('/')[1];
            //base64Str为base64完整的字符串，先处理一下得到我们所需要的字符串
            string[] str = base64Str.Split(',');  
            byte[] imageBytes = Convert.FromBase64String(str[1]);
            //读入MemoryStream对象
            MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            memoryStream.Write(imageBytes, 0, imageBytes.Length);
            filename = path + imgName + "." + hz;//所要保存的相对路径及名字
            string tmpRootDir = Server.MapPath(path); //获取程序根目录 
            if (!Directory.Exists(tmpRootDir))
            {
                Directory.CreateDirectory(tmpRootDir);
            }
            string imagesurl2 = tmpRootDir + imgName + "." + hz; //转换成绝对路径 
            //  转成图片
            Image image = Image.FromStream(memoryStream);
            //   图片名称
            string iname = DateTime.Now.ToString("yyMMddhhmmss");
            // 将图片存到本地Server.MapPath("pic\\") + iname + "." + hz
            image.Save(imagesurl2);  
            return filename;
        }
        public ActionResult order_page()
        {
            string orderid = Request["orderid"];
            book_store_db bookdb = new book_store_db();
            List<order> orders = bookdb.order.Where(u => u.orderid == orderid).Select(m => m).ToList();
            string orderurl = orders[0].orderurl;
            StreamReader sr = new StreamReader(orderurl, System.Text.Encoding.Default);
            List<orderbook> books = new List<orderbook>();
            string id = null;
            string counts = null;
            while ((id = sr.ReadLine().ToString()) != "")
            {
                var book1 = new orderbook();
                int bookid = Convert.ToInt32(id);
                counts = sr.ReadLine().ToString();
                book1.bookid = bookid;
                book1.counts = Convert.ToInt32(counts);
                List<book> book2 = bookdb.book.Where(u => u.bookid == bookid).Select(m => m).ToList();
                book1.imgUrl = book2[0].image_url;
                book1.price = book2[0].price;
                book1.totalprice = book1.price * book1.counts;
                book1.bookname = book2[0].bookname;
                books.Add(book1);
            }
            return View(books);
        }
        public ActionResult user_account()
        {
            int userid = 0;
            try { userid = Convert.ToInt32(Request.Cookies["userid"].Value); }
            catch (Exception e) { return Content("<script>alert('请先登录');location.href='Login'</script>"); }
            book_store_db bookdb = new book_store_db();
            List<user> users = bookdb.user.Where(u => u.userid == userid).Select(m => m).ToList();
            return View(users);
        }
        public string changeinfo()
        {
            string email = Request["email"];
            string password = Request["password"];
            string phone = Request["phone"];
            string address = Request["address"];
            int userid = Convert.ToInt32(Request.Cookies["userid"].Value);
            book_store_db bookdb = new book_store_db();
            var users = bookdb.user.Where(u => u.userid == userid).SingleOrDefault();
            users.email = email;
            users.phone = phone;
            users.password = password;
            users.useraddress = address;
            bookdb.SaveChanges();
            return "1";
        }
        public ActionResult user_charts()
        {
            int userid = 0;
            try { userid = Convert.ToInt32(Request.Cookies["userid"].Value); }
            catch (Exception e) { return Content("<script>alert('请先登录');location.href='Login'</script>"); }
            book_store_db bookdb = new book_store_db();
            var orders = bookdb.order.Where(u => u.userid == userid).Select(m => m).ToList<order>();
            return View(orders);
        }
        public ActionResult user_faq()
        {
            int userid = 0;
            try { userid = Convert.ToInt32(Request.Cookies["userid"].Value); }
            catch (Exception e) { return Content("<script>alert('请先登录');location.href='Login'</script>"); }
            return View();
        }
        public string userfaq()
        {
            string userid = Request["user_id"];
            string content = Request["content"];
            book_store_db bookdb = new book_store_db();
            feedback fd = new feedback();
            fd.substance = content;
            fd.time = DateTime.Now.ToLocalTime();
            fd.userid = Convert.ToInt32(userid);
            bookdb.feedback.Add(fd);
            bookdb.SaveChanges();
            return "1";
        }
        public string adminbookedit()
        {
            book_store_db bookdb = new book_store_db();

            string bookid = Request["book-id"];
            string imgurl = Request["book-img"];
            string bookname = Request["book-name"];
            string synopsis = Request["book-info"];
            string type = Request["book-type"];
            string price = Request["book-price"];
            string stock = Request["book-count"];

            if (bookid == "")
            {
                //新增
                book books = new book();
                books.bookname = bookname;
                books.price = Convert.ToDouble(price);
                books.stock = Convert.ToInt32(stock);
                books.synopsis = synopsis;
                books.type = type;
                var q = (bookdb.book.Select(e => e.bookid).Max() + 1).ToString();
                books.image_url = Base64ToImage(imgurl, "../Book/", q);
                books.salenum = 0;
                bookdb.book.Add(books);
                bookdb.SaveChanges();
                return "success";

            }
            else
            {
                //改
                int booksid = Convert.ToInt32(bookid);
                var books = bookdb.book.Where(u => u.bookid == booksid).SingleOrDefault();
                var q = bookid;
                if (imgurl[0] == 'd')
                    books.image_url = Base64ToImage(imgurl, "../Book/", q);
                books.bookname = bookname;
                books.price = Convert.ToDouble(price);
                books.stock = Convert.ToInt32(stock);
                books.synopsis = synopsis;
                books.type = type;
                try
                {
                    bookdb.SaveChanges();
                }
                catch (Exception e)
                {
                    return "0";

                }


            }
            return "1";
        }
        public string adminbookdel()
        {
            book_store_db bookdb = new book_store_db();
            string orderlist = " {\"Total\":\"0\",\"Rows\":" + Request["delete-list"] + "}";
            JObject json1 = (JObject)JsonConvert.DeserializeObject(orderlist);
            JArray array = (JArray)json1["Rows"];
            foreach (var jObject in array)
            {
                int id = Convert.ToInt32(jObject["bookid"].ToString());
                book books = bookdb.book.Where(r => r.bookid == id).FirstOrDefault();
                bookdb.book.Remove(books);
                bookdb.SaveChanges();
            }
            return "1";
        }
        public ActionResult admin_faq()
        {
            book_store_db bookdb = new book_store_db();
            var feedbacks = bookdb.feedback.Select(m => m).ToList<feedback>();
            feedbacks = feedbacks.OrderByDescending(m => m.time).ToList();
            return View(feedbacks);
        }
        public ActionResult admin_order_edit()
        {
            return View();
        }
        public JsonResult adminordersearch(int limit, int offset, string orderid, string status)
        {
            book_store_db bookdb = new book_store_db();
            var lstRes = new List<order>();
            bool flag;
            if (orderid != "" && status != "")
            {
                if (status == "1") flag = true;
                else flag = false;
                lstRes = bookdb.order.Where(s => s.orderid.Contains(orderid) && s.status == flag).Select(m => m).ToList();
            }
            else if (orderid == "" && status == "")
            {
                lstRes = bookdb.order.Select(m => m).ToList();
            }
            else if (orderid != "")
            {
                lstRes = bookdb.order.Where(s => s.orderid.Contains(orderid)).Select(m => m).ToList();
            }
            else
            {
                if (status == "1") flag = true;
                else flag = false;
                lstRes = bookdb.order.Where(s => s.status == flag).Select(m => m).ToList();
            }
            var total = lstRes.Count;
            var rows = lstRes.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        public string delivery()
        {
            book_store_db bookdb = new book_store_db();
            string orderid = Request["order-id"];
            var orders = bookdb.order.Where(u => u.orderid == orderid).SingleOrDefault();
            orders.status = true;
            bookdb.SaveChanges();
            return "1";
        }
        public ActionResult bookpage()
        {
            book_store_db bookdb = new book_store_db();
            int bookid = Convert.ToInt32(Request["good_id"]);
            var books = bookdb.book.Where(u => u.bookid == bookid).Select(m => m).ToList<book>();
            return View(books);
        }
    }
}