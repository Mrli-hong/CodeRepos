using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using EFCoreDemo2.Model;
using System.Threading.Tasks;
using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCoreDemo2
{
    class Program
    {
        public static void PrintPartment(int Level, MyDbContext db, Partment root)
        {
            Console.WriteLine(new String('\t', Level) + root.Name);
            var list = db.Partments.Include(e => e.ChildrenPartment).First(e => e == root).ChildrenPartment;
            foreach (var item in list)
            {
                PrintPartment(Level + 1, db, item);
            }
        }
        public static void AddBooks()
        {
            using (MyDbContext db = new MyDbContext())
            {
                var book1 = new Book() { Name = "C#程序设计", Titile = "好书", Price = 19.4 };
                var book2 = new Book() { Name = "ASP.NET入门", Titile = "还行", Price = 29.4 };
                var book3 = new Book() { Name = "MVC设计", Titile = "听老", Price = 69.4 };
                var book4 = new Book() { Name = "杨中科ASP", Titile = "厉害", Price = 39.45 };
                var book5 = new Book() { Name = "数据结构入门", Titile = "不太行", Price = 23.2 };
                var book6 = new Book() { Name = "肖申克的救赎", Titile = "是个电影", Price = 60.2 };
                var book7 = new Book() { Name = "无极剑圣", Titile = "阿尔法偷袭", Price = 1.2 };
                var book8 = new Book() { Name = "Java核心技术", Titile = "好书", Price = 39.4 };
                var book9 = new Book() { Name = "白帽Web", Titile = "Hacking", Price = 29.4 };
                db.Books.Add(book1);
                db.Books.Add(book2);
                db.Books.Add(book3);
                db.Books.Add(book4);
                db.Books.Add(book5);
                db.Books.Add(book6);
                db.Books.Add(book7);
                db.Books.Add(book8);
                db.Books.Add(book9);
                db.SaveChanges();
            }
        }
        public static void AddPartments()
        {
            using (MyDbContext db = new MyDbContext())
            {
                Partment root = new Partment() { Name = "总舰艇" };
                Partment wu = new Partment() { Name = "情报部门" };
                Partment wu2 = new Partment() { Name = "武备部门" };
                Partment wu3 = new Partment() { Name = "机电部门" };
                root.ChildrenPartment.Add(wu);
                root.ChildrenPartment.Add(wu2);
                root.ChildrenPartment.Add(wu3);
                Partment qing = new Partment() { Name = "通信" };
                Partment qing2 = new Partment() { Name = "雷达" };
                Partment qing3 = new Partment() { Name = "旗语" };
                Partment qing4 = new Partment() { Name = "电子对抗" };
                wu.ChildrenPartment.Add(qing);
                wu.ChildrenPartment.Add(qing2);
                wu.ChildrenPartment.Add(qing3);
                wu.ChildrenPartment.Add(qing4);
                Partment bei = new Partment() { Name = "对海" };
                Partment bei2 = new Partment() { Name = "对陆" };
                Partment bei3 = new Partment() { Name = "对空" };
                Partment bei4 = new Partment() { Name = "反潜" };
                wu2.ChildrenPartment.Add(bei);
                wu2.ChildrenPartment.Add(bei2);
                wu2.ChildrenPartment.Add(bei3);
                wu2.ChildrenPartment.Add(bei4);
                Partment bei1 = new Partment() { Name = "舱段" };
                Partment bei21 = new Partment() { Name = "电工" };
                Partment bei31 = new Partment() { Name = "燃机" };
                Partment bei41 = new Partment() { Name = "传动" };
                wu3.ChildrenPartment.Add(bei1);
                wu3.ChildrenPartment.Add(bei21);
                wu3.ChildrenPartment.Add(bei31);
                wu3.ChildrenPartment.Add(bei41);
                db.Partments.Add(root);
                db.SaveChanges();
            }
        }
        public static void QueryBooks(string searchString, bool searchAll, bool orderByPrice, double upperPrice)
        {
            using (MyDbContext db = new MyDbContext())
            {
                IQueryable<Book> sqlStr = db.Books.Where(e => e.Price <= upperPrice);
                if (searchAll)
                {
                    sqlStr = sqlStr.Where(e => e.Titile.Contains(searchString) || e.Name.Contains(searchString));
                }
                else
                    sqlStr = sqlStr.Where(e => e.Titile.Contains(searchString));
                if (orderByPrice)
                {
                    sqlStr = sqlStr.OrderBy(e => e.Price);
                }
                foreach (var item in sqlStr)
                {
                    Console.WriteLine($"书名：{item.Name}，标题：{item.Titile}，价格：{item.Price}");
                }
            }
        }
        public static void PrintPage(int pageIndex, int pageSize)
        {
            using (MyDbContext db = new MyDbContext())
            {
                int sumNum = db.Books.Count();
                var resultBooks = db.Books.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                foreach (var item in resultBooks)
                {
                    Console.WriteLine($"书名：{item.Name}，标题：{item.Titile}，价格：{item.Price}");
                }
                Console.WriteLine($"一共{Math.Ceiling(sumNum * 1.0 / pageSize)}页");
            }
        }
        static async Task Main(string[] args)
        {
             
            using (MyDbContext db = new MyDbContext())
            {
                
                Book b1 = new Book() { ID = 1, Name = "MVC" };
                var a = db.Entry(b1);
                a.Property("Name").IsModified = true;
                Console.WriteLine(a.DebugView.LongView);
                //var result = db.Books.Take(2).ToArray();
                //var a1 = result[0];
                //var a2 = result[1];
                //a1.Name = "Hello";
                //db.Remove(a2);
                //Book a3 = new Book();
                //Book a4 = new Book() { Name="Ni"};
                //db.Books.Add(a4);
                //EntityEntry b1 = db.Entry(a1);
                //EntityEntry b2 = db.Entry(a2);
                //EntityEntry b3 = db.Entry(a3);
                //EntityEntry b4 = db.Entry(a4);
                //Console.WriteLine(b1.State);
                //Console.WriteLine(b3.DebugView.LongView); 
                //Console.WriteLine(b2.State);
                //Console.WriteLine(b3.State);
                //Console.WriteLine(b4.State);
                #region test
                //var result = db.Database.GetDbConnection().Query<Book>("select * from T_Partment where id=1");
                //foreach (var item in result)
                //{
                //    Console.WriteLine(item.Name);
                //}
                //DbConnection conn = db.Database.GetDbConnection();
                //if (conn.State != System.Data.ConnectionState.Open)
                //{
                //    await conn.OpenAsync();
                //}
                //using (var cmd = conn.CreateCommand())
                //{
                //    cmd.CommandText = @"select Name from T_Partment where id=@id";
                //    var param = cmd.CreateParameter();
                //    param.ParameterName = "@id";
                //    param.Value = 1;
                //    cmd.Parameters.Add(param);
                //    using (var reader = await cmd.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            var result = await reader.GetFieldValueAsync<String>(0);
                //            Console.WriteLine(result);
                //        }
                //    }
                //}
                //IQueryable<Partment> GetBookSqlStr = db.Partments.FromSqlInterpolated(@$"select * from T_Partment ");
                //foreach (var item in GetBookSqlStr.Include(e=>e.OriPartment).OrderBy(e=>Guid.NewGuid()))
                //{
                //    Console.WriteLine(item.Name);
                //}

                //int age = 10;
                //string Name = "LI";
                //FormattableString sql = @$"insert into T_Book(Title,Name) select TiTle,{Name},'哈哈' from  T_Articles Where age>={age}";
                //Console.WriteLine("格式"+sql.Format);
                //Console.WriteLine("参数" + String.Join(',',sql.GetArguments()));
                //await db.Database.ExecuteSqlInterpolatedAsync(@$"insert into T_Book(Title,Name)
                //                                              select TiTle,{Name},'哈哈'
                //                                              from 
                //                                              T_Articles
                //                                              Where age>={age}
                //                                              ");
                //IQueryable<Book> sqlStr = db.Books;
                //foreach (var item in sqlStr)
                //{
                //    Console.WriteLine(item.Name);
                //}
                //QueryBooks("MVC", true, false, 100);
                //PrintPage(3, 2);
                //PrintPartment(0,db,db.Partments.Include(e=>e.ChildrenPartment).Single(e => e.OriPartment == null));
                //IQueryable queryable = db.Articles.Where(e => e.Content.Contains("HZ"));
                //IEnumerable<Article> Ienumber = db.Articles;
                //var reuslt = Ienumber.Where(e => e.Content.Contains("HZ"));
                //Article article = new Article { Title = "你好", Content = "sdsad" };
                //Comment c1 = new Comment() { Message = "tinghaode" };
                //Comment c2 = new Comment() { Message = "tinghaode2" };
                //article.Comments.Add(c1);
                //article.Comments.Add(c2);
                //db.Articles.Add(article);
                //db.SaveChanges();Single(e=>e.Id==1);
                //var result = db.Articles.Include(e => e.Comments).First(e=>e.Id==1);
                //foreach (var item in result.Comments)
                //{
                //    Console.WriteLine("-------------------------");
                //    Console.WriteLine(item.Id);
                //    Console.WriteLine("-------------------------");
                //}
                //var articleID = db.Comments.Select(e => new { ID = e.Id,id=e.ArticleId}).First(e=>e.ID==1);
                //Console.WriteLine(articleID.ID);
                #endregion
            }
        }
    }
}
