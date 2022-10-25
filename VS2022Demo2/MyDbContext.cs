namespace VS2022Demo2
{
    public record Book(int Id, string Name);
    public class MyDbContext
    {
        public static Book? GetBookByID(int id)
        {
            switch (id)
            {
                case 0:
                    return new Book(0, "程序员经典");
                case 1:
                    return new Book(1, "C#经典入门实战");
                case 2:
                    return new Book(2, "ASP.NET入门");
                default:
                    return null;
            }
        }
        public static Task<Book?> GetBookByIDAsync(int id)
        {
            var result = GetBookByID(id);
            return Task.FromResult(result);
        }
    }
}
