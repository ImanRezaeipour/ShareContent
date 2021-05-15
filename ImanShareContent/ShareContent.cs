using System.ServiceProcess;
using ImanShareContent.Content;
using ImanShareContent.Publish;

namespace ImanShareContent
{
    /// <summary>
    /// 
    /// </summary>
    partial class ShareContent : ServiceBase
    {
        System.Timers.Timer getContentTechnologyTimer = new System.Timers.Timer();
        System.Timers.Timer toTelegramTechnologyTimer = new System.Timers.Timer();

        /// <summary>
        /// 
        /// </summary>
        public ShareContent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeContentList()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetContentTechnology()
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(typeof(string));  //Declaring Log4Net 

            try
            {
                //new Digiato().GetDigiato();
                //new Zoomit().GetZoomit();
                //new ITNA().GetITNA();
                new Content.TechnologyContent().GetTechnologyContent();

                logger.Info("Get Content Technology is Succesfully.");
                return "OK";
            }
            catch(System.Exception ex)
            {
                logger.Error(ex.ToString());
                return "BAD";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToTelegramTechnology()
        {
            log4net.ILog logger = log4net.LogManager.GetLogger(typeof(string));  //Declaring Log4Net 
            try
            {
                new TelegramPublish().ToTechnologyGroup();

                logger.Info("To Telegram Technology is Succesfully.");
                return "OK";
            }
            catch(System.Exception ex)
            {
                logger.Error(ex.ToString());
                return "BAD";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // Sleep for 1 Minute
            //System.Threading.Thread.Sleep(30000);
            //new Telegram().ToTechnologyGroup();
            //new Content.TechnologyContent().GetTechnologyContent();

            getContentTechnologyTimer.Interval = System.Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["GetContentTechnologyIntevalMilisecond"].ToString());
            getContentTechnologyTimer.Enabled = true;
            getContentTechnologyTimer.Elapsed += new System.Timers.ElapsedEventHandler(GetMainContentTimer_Elapsed);

            toTelegramTechnologyTimer.Interval = System.Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ToTelegramTechnologyIntervalMilisecond"].ToString());
            toTelegramTechnologyTimer.Enabled = true;
            toTelegramTechnologyTimer.Elapsed += new System.Timers.ElapsedEventHandler(ToTelegramTimer_Elapsed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GetMainContentTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //new Content.TechnologyContent().GetTechnologyContent();
            GetContentTechnology();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ToTelegramTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ToTelegramTechnology();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStop()
        {
            getContentTechnologyTimer.Stop();
            toTelegramTechnologyTimer.Stop();
        }
    }
}
