using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ServiceOrApp
{
    public class GetConfig
    {
        public ConfigModel GetOrSetConfigModel()
        {
            string configFile = Process.GetCurrentProcess().MainModule.FileName;//定位主程序的文件位置 （控制台程序EXE位置）
            string logPath = configFile.Replace("ServiceOrApp.exe", "log.txt");
            string configXmlPath = configFile.Replace("ServiceOrApp.exe", "ServiceConfig.xml");

            ConfigModel configModel = new ConfigModel();
           
            if (!File.Exists(logPath))
            {
                File.AppendAllText(logPath, "定时服务 创建log日志：" + DateTime.Now.ToString()  + "\r\n");
            }
            if (!File.Exists(configXmlPath))
            {
                File.AppendAllText(logPath, "定时服务 创建XML文件开始：" + DateTime.Now.ToString() + "\r\n");
                //
                var xmlDoc = new XmlDocument();
                //Create the xml declaration first 
                xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
                //Create the root node and append into doc 
                var serviceConfig = xmlDoc.CreateElement("serviceConfig");
                xmlDoc.AppendChild(serviceConfig);
                // Contact 
                XmlElement elementAdmin = xmlDoc.CreateElement("serviceSetting");
                XmlAttribute adminEmailtype = xmlDoc.CreateAttribute("type");
                adminEmailtype.Value = "adminEmail";
                elementAdmin.Attributes.Append(adminEmailtype);
                XmlAttribute adminEmailAddress = xmlDoc.CreateAttribute("adminEmailAddress");//发件人邮箱
                adminEmailAddress.Value = "*******";
                elementAdmin.Attributes.Append(adminEmailAddress);
                XmlAttribute adminEmailPassword = xmlDoc.CreateAttribute("adminEmailPassword");//发件人密码或授权码
                adminEmailPassword.Value = "******";
                elementAdmin.Attributes.Append(adminEmailPassword);
                XmlAttribute BCCEmailAddress = xmlDoc.CreateAttribute("BCCEmailAddress");//抄送邮箱
                BCCEmailAddress.Value = "******";
                elementAdmin.Attributes.Append(BCCEmailAddress);
                XmlAttribute smtpPort = xmlDoc.CreateAttribute("smtpPort");
                smtpPort.Value = "587";
                elementAdmin.Attributes.Append(smtpPort);
                XmlAttribute smtpServer = xmlDoc.CreateAttribute("smtpServer");
                smtpServer.Value = "smtp.qq.com";
                elementAdmin.Attributes.Append(smtpServer);

                serviceConfig.AppendChild(elementAdmin);

                // Contact 
                XmlElement elementUser = xmlDoc.CreateElement("serviceSetting");
                XmlAttribute userEmailtype = xmlDoc.CreateAttribute("type");
                userEmailtype.Value = "userEmail";
                elementUser.Attributes.Append(userEmailtype);
                XmlAttribute userEmailAddress = xmlDoc.CreateAttribute("userEmailAddress");//收件人邮箱
                userEmailAddress.Value = "*******";
                elementUser.Attributes.Append(userEmailAddress);
                XmlAttribute userName = xmlDoc.CreateAttribute("userName");
                userName.Value = "Henry";
                elementUser.Attributes.Append(userName);
                serviceConfig.AppendChild(elementUser);

                xmlDoc.Save(configXmlPath);
                File.AppendAllText(logPath, "定时服务 创建XML文件结束：" + DateTime.Now.ToString() + "\r\n");
            }

            XmlDocument doc = new XmlDocument();
            XmlReaderSettings setting = new XmlReaderSettings();
            setting.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(configXmlPath, setting);
            doc.Load(reader);
            XmlNode xn = doc.SelectSingleNode("serviceConfig");
            XmlNodeList xnList = xn.ChildNodes;
            foreach (XmlNode item in xnList)
            {
                XmlElement xe = (XmlElement)item;
                if (xe.GetAttribute("type").ToString() == "adminEmail")
                {
                    configModel.AdminEmailAddress = xe.GetAttribute("adminEmailAddress").ToString();
                    configModel.AdminEmailPassword = xe.GetAttribute("adminEmailPassword").ToString();
                    configModel.BCCEmailAddress = xe.GetAttribute("BCCEmailAddress").ToString();
                    configModel.SmtpPort = xe.GetAttribute("smtpPort").ToString();
                    configModel.SmtpServer = xe.GetAttribute("smtpServer").ToString();
                }
                if (xe.GetAttribute("type").ToString() == "userEmail")
                {
                    configModel.UserEmailAddress = xe.GetAttribute("userEmailAddress").ToString();
                    configModel.UserName = xe.GetAttribute("userName").ToString();
                }
            }

            File.AppendAllText(logPath, "定时服务 读取XML文件完毕：" + DateTime.Now.ToString() + "\r\n");

            return configModel;

        }

        public ServiceModel GetServiceModel()
        {
            string configFile = Process.GetCurrentProcess().MainModule.FileName;//定位主程序的文件位置 （控制台程序EXE位置）
            string logPath = configFile.Replace("ServiceOrApp.exe", "log.txt");
            string configXmlPath = configFile.Replace("ServiceOrApp.exe", "ServiceConfig.xml");
            return new ServiceModel
            {
                ServiceName = "AutoEmailService",
                ServicePath = configFile,
                LogPath = logPath,
                ConfigXmlPath = configXmlPath
            };
        }
    }
}
