using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS2022Demo1;
public class Dog
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    //public int Id { get;init; }
    //public string Name { get; init; }
    public Dog(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public void AA()
    {
        this.Name = "String";
    }
}
internal record person(int Id,string Name);
internal record dog(int Id, string Name)
{
    public string AGE { get; set; }
    public dog(int Id, string Name, string AGE) : this( Id, Name)
    {
        this.AGE = AGE;
    }
}