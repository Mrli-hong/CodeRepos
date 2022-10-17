using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo3
{
    class Program
    {
        static void  Main(string[] args)
        {

            #region test
            //Console.WriteLine("请输入您的姓名");
            //string name = Console.ReadLine();
            //using MyDb ctx = new MyDb();
            //var h1 = await ctx.rents.SingleAsync(h => h.ID == 1);
            //if (string.IsNullOrEmpty(h1.Name))
            //{
            //    await Task.Delay(5000);
            //    h1.Name = name;
            //    try
            //    {
            //        await ctx.SaveChangesAsync();
            //        Console.WriteLine("抢到手了");
            //    }
            //    catch (DbUpdateConcurrencyException ex)
            //    {
            //        var entry = ex.Entries.First();
            //        var dbValues = await entry.GetDatabaseValuesAsync();
            //        string newOwner = dbValues.GetValue<string>(nameof(Rent.Name));
            //        Console.WriteLine($"并发冲突，被{newOwner}提前抢走了");
            //    }
            //}
            //else
            //{
            //    if (h1.Name == name)
            //    {
            //        Console.WriteLine("这个房子已经是你的了，不用抢");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"这个房子已经被{h1.Name}抢走了");
            //    }
            //}
            //Console.ReadLine();
            //using (MyDb db = new MyDb())
            //{
            //    Console.WriteLine("请输入用户名字:");
            //    string Name = Console.ReadLine();
            //    var a = db.rents.Single(e => e.ID == 1);
            //    Thread.Sleep(3000);
            //    a.Name = Name;
            //    try
            //    {
            //        db.SaveChanges(); Console.ReadKey();
            //    }
            //    catch (DbUpdateConcurrencyException ex)
            //    {
            //        var entry = ex.Entries.First();
            //        var dbValues = entry.GetDatabaseValues();
            //        string newOwner = dbValues.GetValue<string>(nameof(Rent.Name));
            //        Console.WriteLine($"并发冲突，被{newOwner}提前抢走了"); 
            //    }
            //    Console.ReadKey();
            //}
            //using (var tx = db.Database.BeginTransaction())
            //{
            //    var a = db.rents.FromSqlInterpolated($@"select * from rents where ID=1 for update").Single();
            //    if (!string.IsNullOrEmpty(a.Name))
            //    {
            //        Console.WriteLine("已被{0}占领", a.Name);

            //        Console.ReadLine();
            //        return;
            //    }
            //    //db.rents.FromSqlInterpolated($@"update rents set Name={Name}, inTime={DateTime.Now} where ID=1 for update;");
            //    a.Name = Name;
            //    a.InTime = DateTime.Now;
            //    Thread.Sleep(5000);
            //    Console.WriteLine("修改成功！");
            //    db.SaveChanges();
            //    tx.Commit();
            //    Console.ReadLine();
            //}

            //var a = new Rent() { ID = 1, Name = Name, InTime = DateTime.Now };
            //var result = db.Entry(a);
            //result.Property("Name").IsModified = true;
            //result.Property("InTime").IsModified = true;
            #endregion
        }
    }
}
