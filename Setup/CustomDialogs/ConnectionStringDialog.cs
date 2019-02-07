using Setup.Data;
using Setup.Interfaces;
using Setup.Managers;
using System;
using WixSharp;
using WixSharp.UI.Forms;
namespace Setup.CustomDialogs
{
    public partial class ConnectionStringDialog : ManagedForm
    {
        private ISqlManager _sqlManager;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ConnectionStringDialog()
        {
            InitializeComponent();
            _sqlManager = new MsSqlManager();
        }

        /// <summary>
        /// Обработчик события загрузки кастомной формы
        /// </summary>
        /// <param name="sender">Объект инициатора события</param>
        /// <param name="e">Аргументы события</param>
        private void DialogLoad(object sender, EventArgs e)
        {
            DrawLayout();
            LoadDataContext();
        }

        /// <summary>
        /// Инициализация контекста формы
        /// </summary>
        private void LoadDataContext()
        {
            connectionStringInput1.ConnectionString =
                MsiRuntime.Session[Properties.ConnectionString.PropertyName].ToString();
        }

        /// <summary>
        /// Метод начальной отрисовки внешнего вида диалога
        /// </summary>
        private void DrawLayout()
        {
            banner.Image = Runtime.Session.GetResourceBitmap( Parameters.WixBannerParameter);

            var ratio = (float)banner.Image.Width / banner.Image.Height;
            topPanel.Height = (int)(banner.Width / ratio);

            var upShift = (int)(next.Height * 2.3) - bottomPanel.Height;
            bottomPanel.Top -= upShift;
            bottomPanel.Height += upShift;

            middlePanel.Top = topPanel.Bottom + 10;
            middlePanel.Height = (bottomPanel.Top - 10) - middlePanel.Top;
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Назад"
        /// </summary>
        /// <param name="sender">Объект инициатора события</param>
        /// <param name="e">Аргументы события</param>
        private void BackClick(object sender, EventArgs e)
        {
            Shell.GoPrev();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Далее"
        /// </summary>
        /// <param name="sender">Объект инициатора события</param>
        /// <param name="e">Аргументы события</param>
        private void NextClick(object sender, EventArgs e)
        {
            var connectionString = connectionStringInput1.ConnectionString;

            try
            {
                _sqlManager.CheckServerAvailability(connectionString);

                if (!_sqlManager.IsDatabaseExists(connectionString) &&
                    !_sqlManager.IsUserHasRestoreDatabaseRights(connectionString))
                {
                    NotificationManager.ShowExclamationMessage(
                        $"Пользователь не имеет прав на создание БД");
                    return;
                }
            }
            catch (Exception ex)
            {
                NotificationManager.ShowExclamationMessage(
                    $"{ex.Message}: {ex.InnerException?.Message}");
                return;
            }

            MsiRuntime.Data[Properties.ConnectionString.PropertyName] = connectionString;
            Shell.GoNext();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Отмена"
        /// </summary>
        /// <param name="sender">Объект инициатора события</param>
        /// <param name="e">Аргументы события</param>
        private void CancelClick(object sender, EventArgs e)
        {
            Shell.Cancel();
        }
    }
}
