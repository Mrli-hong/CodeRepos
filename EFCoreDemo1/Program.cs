using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreDemo1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                #region 验证Guid为主键 EF Core为其赋值
                Dog dog = new Dog();
                dog.Name = "小王";
                Console.WriteLine(dog.ID);
                //通过EF Core为dog的guid赋值
                db.Dogs.Add(dog);

                //无法被EF翻译
                //var person = db.Persons.Where(p => p.Name.PadLeft(5) == "sdas");
                //Console.WriteLine(person.Count());

                IQueryable<Book> result = db.Books.Where(e => e.ID == 2);
                string str = result.ToQueryString();//using Microsoft.EntityFrameWorkCore
                Console.WriteLine(str);

                Console.WriteLine(dog.ID);
                await db.SaveChangesAsync();
                #endregion

                //Book book = new Book() { Name = "C#程序设计", Nums = 19, Tile = "一趟中考", PubTime = new DateTime(1998, 1, 1) };
                //Book book1 = new Book() { Name = "C#程序设计2", Nums = 29, Tile = "asd", PubTime = new DateTime(1992, 1, 1) };
                //Book book2 = new Book() { Name = "C#程序设计3", Nums = 119, Tile = "zxc", PubTime = new DateTime(1948, 1, 1) };
                //Book book3 = new Book() { Name = "C#程序设计4", Nums = 139, Tile = "qqq", PubTime = new DateTime(1928, 1, 1) };
                //Book book4 = new Book() { Name = "C#程序设计5", Nums = 49, Tile = "qAq", PubTime = new DateTime(1918, 1, 1) };
                //db.Books.Add(book);
                //db.Books.Add(book1);
                //db.Books.Add(book2);
                //db.Books.Add(book3);
                //db.Books.Add(book4);
                //await db.SaveChangesAsync();
            }
        }
    }
}
