using System;
using System.Configuration;
using Ninject.Extensions.Logging;
using Ninject;

namespace Helper.Configuration
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    public class Configuration : IConfiguration
    {
        private static ILogger _logger;

        public Configuration()
        {
            GetProperties();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger"></param>
        [Inject]
        public Configuration(ILogger logger)
        {
            _logger = logger;
            GetProperties();
        }

        private void GetProperties()
        {
            PathToApi = GetAppString("PathToAPI");
            SenderMailAddress = GetAppString("FromEmailAddress");
            SenderMailPassword = GetAppString("FromEmailPassword");

            bool enableSslForSmtp = false;
            bool.TryParse(GetAppString("EnableSSLForSmtp"), out enableSslForSmtp);
            EnableSslForSmtp = enableSslForSmtp;

            SmtpHost = GetAppString("SmtpHost");
            int port = 0;
            SmtpPort = int.TryParse(GetAppString("SmtpPort"), out port) ? port : (int?)null;

            SmsServiceUrl = GetAppString("SmsServiceUrl");
            SmsServiceActionUrl = GetAppString("SmsServiceActionUrl");
            SmsServiceLogin = GetAppString("SmsServiceLogin");
            SmsServicePassword = GetAppString("SmsServicePassword");
            SmsNaming = GetAppString("SmsNaming");

            bool sendSms = false;
            bool.TryParse(GetAppString("SendAlarmSms"), out sendSms);
            SendAlarmSms = sendSms;

            bool sendEmail = false;
            bool.TryParse(GetAppString("SendAlarmEmail"), out sendEmail);
            SendAlarmEmail = sendEmail;

            OperatorManual = GetAppString("OperatorManual");

            OperatorManualX5 = GetAppString("OperatorManualX5");

            bool enableFlowDiagram = false;
            bool.TryParse(GetAppString("EnableFlowDiagram"), out enableFlowDiagram);
            EnableFlowDiagram = enableFlowDiagram;

            bool enableMonitoring = false;
            bool.TryParse(GetAppString("EnableMonitoring"), out enableMonitoring);
            EnableMonitoring = enableMonitoring;

            DevicePollerDefaultAddress = GetAppString("WCF-DevicePoller-Path");

            AllowedAlarmDevicesForIntegration = GetAppString("AllowedAlarmDevicesForIntegration");
        }

        /// <summary>
        /// Путь к api
        /// </summary>
        public string PathToApi { get; private set; }

        /// <summary>
        /// Email отправителя   
        /// </summary>
        public string SenderMailAddress { get; private set; }

        /// <summary>
        /// Пароль к почте отправителя
        /// </summary>
        public string SenderMailPassword { get; private set; }

        /// <summary>
        /// Использовать ssl для почтового сервера smtp
        /// </summary>
        public bool EnableSslForSmtp { get; private set; }

        /// <summary>
        /// Адрес smtp службы
        /// </summary>
        public string SmtpHost { get; private set; }

        /// <summary>
        /// Порт smtp службы
        /// </summary>
        public int? SmtpPort { get; private set; }

        /// <summary>
        /// Адрес службы смс оповещения
        /// </summary>
        public string SmsServiceUrl { get; private set; }

        /// <summary>
        /// Адрес метода для отправки смс
        /// </summary>
        public string SmsServiceActionUrl { get; private set; }

        /// <summary>
        /// Логин для доступа к службе смс оповещения
        /// </summary>
        public string SmsServiceLogin { get; private set; }

        /// <summary>
        /// Пароль для доступа к службе смс оповещения
        /// </summary>
        public string SmsServicePassword { get; private set; }

        /// <summary>
        /// Наименование отправителя
        /// </summary>
        public string SmsNaming { get; private set; }

        /// <summary>
        /// Отсылать смс об аварии
        /// </summary>
        public bool SendAlarmSms { get; private set; }

        /// <summary>
        /// Отсылать email об аварии
        /// </summary>
        public bool SendAlarmEmail { get; private set; }

        /// <summary>
        /// Имя файла руководство оператора
        /// </summary>
        public string OperatorManual { get; private set; }

        /// <summary>
        /// Имя файла руководство оператора для X5
        /// </summary>
        public string OperatorManualX5 { get; private set; }

        /// <summary>
        /// Показывать ли мнемосхематор
        /// </summary>
        public bool EnableFlowDiagram { get; private set; }

        /// <summary>
        /// Показывать ли мониторинг
        /// </summary>
        public bool EnableMonitoring { get; private set; }

        /// <summary>
        /// Адрес службы опроса по умолчанию
        /// </summary>
        public string DevicePollerDefaultAddress { get; private set; }

        private static string GetAppString(string paramName)
        {
            if (String.IsNullOrEmpty(paramName))
                throw new ArgumentNullException("paramName");

            return ConfigurationManager.AppSettings[paramName];
        }

        /// <summary>
        /// Не передавать аварии в АСКО по следующим блокам
        /// </summary>
        public string AllowedAlarmDevicesForIntegration { get; private set; }
    }
}
