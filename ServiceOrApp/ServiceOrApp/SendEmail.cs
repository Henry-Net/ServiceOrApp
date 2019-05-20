using System;
using System.Net.Mail;
using System.Text;

namespace ServiceOrApp
{
    public class SendEmail
    {
        private ConfigModel _configModel { get; set; }
        private ServiceModel _serviceModel { get; set; }

        public SendEmail()
        {
            _configModel = new GetConfig().GetOrSetConfigModel() ;
            _serviceModel = new GetConfig().GetServiceModel();
            _configModel.EmailSubject = "auto email service to you";
            _configModel.EmailContent = "<h1>Welcome "+_configModel.UserName+ " use auto service</h1><br>自动服务邮件<br>欢迎使用自动发邮件服务<br><img src='http://pics.sc.chinaz.com/files/pic/moban/201904/moban3734.jpg'>";
        }

        /// <summary>
        /// 发送通用邮件
        /// </summary>
        /// <returns></returns>
        public bool SendCommonEmail()
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(_configModel.UserEmailAddress);//收件人地址 
            mailMessage.CC.Add(_configModel.BCCEmailAddress);//抄送人地址 

            mailMessage.From = new MailAddress(_configModel.AdminEmailAddress, "Henry");//发件人邮箱，名称 

            mailMessage.Subject = _configModel.EmailSubject;//邮件标题 
            mailMessage.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8 

            mailMessage.IsBodyHtml = true;//邮件内容是否是HTML

            mailMessage.Body = _configModel.EmailContent;//邮件内容 
            mailMessage.BodyEncoding = Encoding.UTF8;//内容格式为UTF8 

            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = _configModel.SmtpServer;//SMTP服务器地址 
            smtpClient.Port = Int32.Parse(_configModel.SmtpPort);//SMTP端口，QQ邮箱填写587 

            smtpClient.EnableSsl = true;//启用SSL加密 

            //发件人邮箱账号，授权码(注意此处，是授权码你需要到qq邮箱里点设置开启Smtp服务，然后会提示你第三方登录时密码处填写授权码)
            smtpClient.Credentials = new System.Net.NetworkCredential(_configModel.AdminEmailAddress, _configModel.AdminEmailPassword);

            try
            {
                smtpClient.Send(mailMessage);//发送邮件
            }
            catch (Exception e)
            {
                var exceptionMessage = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送带附件的邮件
        /// </summary>
        /// <returns></returns>
        public bool SendEmailAndAttachment()
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(_configModel.UserEmailAddress);//收件人地址 
            mailMessage.CC.Add(_configModel.BCCEmailAddress);//抄送人地址 

            mailMessage.From = new MailAddress(_configModel.AdminEmailAddress, "Henry");//发件人邮箱，名称 

            mailMessage.Subject = _configModel.EmailSubject;//邮件标题 
            mailMessage.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8 

            mailMessage.IsBodyHtml = true;//邮件内容是否是HTML

            mailMessage.Body = _configModel.EmailContent;//邮件内容 
            mailMessage.BodyEncoding = Encoding.UTF8;//内容格式为UTF8 

            Attachment dataAttachment = new Attachment(_serviceModel.LogPath);//根据附件位置找到附件
            mailMessage.Attachments.Add(dataAttachment); //附件需要用Add的方式增加到邮件中


            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = _configModel.SmtpServer;//SMTP服务器地址 
            smtpClient.Port = Int32.Parse(_configModel.SmtpPort);//SMTP端口，QQ邮箱填写587 

            smtpClient.EnableSsl = true;//启用SSL加密 

            //发件人邮箱账号，授权码(注意此处，是授权码你需要到qq邮箱里点设置开启Smtp服务，然后会提示你第三方登录时密码处填写授权码)
            smtpClient.Credentials = new System.Net.NetworkCredential(_configModel.AdminEmailAddress, _configModel.AdminEmailPassword);

            try
            {
                smtpClient.Send(mailMessage);//发送邮件
                dataAttachment.Dispose();//附件发送完成后，需要释放，否则相关文件会是用户锁定状态
            }
            catch (Exception e)
            {
                var exceptionMessage = e.Message;
                return false;
            }
            return true;
        }


    }
}
