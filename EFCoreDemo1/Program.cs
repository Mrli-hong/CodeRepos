using System;
using System.Threading.Tasks;

namespace EFCoreDemo1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Book book = new Book() { Name = "C#程序设计", Nums = 19, Tile = "一趟中考", PubTime = new DateTime(1998, 1, 1) };
                Book book1 = new Book() { Name = "C#程序设计2", Nums = 29, Tile = "asd", PubTime = new DateTime(1992, 1, 1) };
                Book book2 = new Book() { Name = "C#程序设计3", Nums = 119, Tile = "zxc", PubTime = new DateTime(1948, 1, 1) };
                Book book3 = new Book() { Name = "C#程序设计4", Nums = 139, Tile = "qqq", PubTime = new DateTime(1928, 1, 1) };
                Book book4 = new Book() { Name = "C#程序设计5", Nums = 49, Tile = "qAq", PubTime = new DateTime(1918, 1, 1) };
                db.Books.Add(book);
                db.Books.Add(book1);
                db.Books.Add(book2);
                db.Books.Add(book3);
                db.Books.Add(book4);
                await db.SaveChangesAsync();
            }
        }
    }
}
