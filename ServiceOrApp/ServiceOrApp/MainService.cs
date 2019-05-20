
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ServiceOrApp
{
    partial class MainService : ServiceBase
    {
        private ServiceModel _serviceModel = new GetConfig().GetServiceModel();
        private ConfigModel _configModel = new GetConfig().GetOrSetConfigModel();
        public MainService()
        {
            InitializeComponent();
        }

        System.Timers.Timer timer; //定时器System.Timers
        protected override void OnStart(string[] args)
        {
            // TODO:  在此处添加代码以启动服务。

            System.IO.File.AppendAllText(_serviceModel.LogPath, "服务已启动……" + DateTime.Now.ToString() + "\r\n");
            timer = new System.Timers.Timer();
            timer.Interval = 5*60*1000;//毫秒为单位 默认100
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.SentEmail);//委托方法按照固定参数写入
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            // TODO:  在此处添加代码以执行停止服务所需的关闭操作。

            System.IO.File.AppendAllText(_serviceModel.LogPath, "服务已停止……" + DateTime.Now.ToString() + "\r\n");
            this.timer.Enabled = false;
        }

        private void SentEmail(object sender, System.Timers.ElapsedEventArgs e)//必须按照参数方法才能调用System.Timers.ElapsedEventHandler方法
        {
            DateTime nowTime = DateTime.Now;

            SendEmail sendEmail = new SendEmail();
            if (sendEmail.SendCommonEmail())
            {
                System.IO.File.AppendAllText(_serviceModel.LogPath, "定时服务 发送邮件成功" + DateTime.Now.ToString() + "\r\n");
            }
            

        }


    }
}
