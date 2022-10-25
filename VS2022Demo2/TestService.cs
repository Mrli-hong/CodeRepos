namespace VS2022Demo2
{
    public class TestService
    {
        private string[] files;
        public TestService()
        {
            this.files = Directory.GetFiles("D:/Program Files (x86)", "*.exe", SearchOption.AllDirectories);
        }
        public int Count 
        {
            get { return this.files.Length; }
        }
    }
}
