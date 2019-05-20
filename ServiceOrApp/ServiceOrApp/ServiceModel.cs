using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOrApp
{
    public class ServiceModel
    {
        //服务exe 路径
        public string ServicePath { get; set; }
        //Log.txt 路径
        public string LogPath { get; set; }
        //ServiceConfig.Xml 路径
        public string ConfigXmlPath { get; set; }

        //服务名字
        public string ServiceName { get; set; }
    }
    public class ConfigModel
    {
        //发件账号
        public string AdminEmailAddress { get; set; }

        //发件密码或授权码
        public string AdminEmailPassword { get; set; }

        //发件邮箱服务地址
        public string SmtpServer { get; set; }

        //发件邮箱服务端口
        public string SmtpPort { get; set; }

        //BCC（抄送）账号
        public string BCCEmailAddress { get; set; }

        #region 收件人的信息内容

        //收件账号
        public string UserEmailAddress { get; set; }
        //收件姓名
        public string UserName { get; set; }
        //收件主题
        public string EmailSubject { get; set; }
        //收件内容
        public string EmailContent { get; set; }

        #endregion


    }
}
