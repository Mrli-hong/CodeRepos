using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingDemo1
{
    class NLogTest
    {
        private readonly ILogger<NLogTest> logger;
        public NLogTest(ILogger<NLogTest> logger)
        {
            this.logger = logger;
        }
        public void Test()
        {
            logger.LogDebug("数据库");
            logger.LogWarning("数据库还行");
            logger.LogInformation("你好");
            logger.LogCritical("数据库炸了");
            try
            {
                File.ReadAllText("A:/1.txt");
                logger.LogDebug("读取文件成功！");
            }
            catch (Exception ex)
            {
                //两个参数的LogDebug
                logger.LogDebug(ex,"读取文件失败");
            }
        }
    }
}
