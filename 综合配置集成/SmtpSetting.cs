namespace 综合配置集成
{
    public record SmtpSetting
    {
        public string UserName { get; set; }
        public string Server { get; set; }
        public string Password { get; set; }  
    }

}
