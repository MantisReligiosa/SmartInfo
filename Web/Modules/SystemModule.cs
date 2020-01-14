using DomainObjects;
using Nancy.ModelBinding;
using ServiceInterfaces;
using System.Collections.Generic;
using Web.Models;

namespace Web.Modules
{
    public class SystemModule : WrappedNancyModule
    {
        private readonly ISystemController _systemController;

        public SystemModule(ISystemController systemController)
            : base()
        {
            _systemController = systemController;

            Get["/api/fonts"] = Wrap(GetFonts, "Ошибка загрузки шрифтов");
            Get["/api/datetimeformats"] = Wrap(GetDatetimeFormats, "Ошибка загрузки форматов даты/времени");
            // Нельзя менять на GET, т.к. есть параметры
            Post["/api/screenResolution"] = Wrap(GetScreenInfo, "Ошибка загрузки информации о экранах");
        }

        private ScreenInfo GetScreenInfo()
        {
            var data = this.Bind<ScreenResolutionRequest>();
            ScreenInfo screenInfo;
            if (!data.RefreshData)
            {
                screenInfo = _systemController.GetDatabaseScreenInfo();
                if (screenInfo == null)
                {
                    screenInfo = _systemController.GetSystemScreenInfo();
                    _systemController.SetDatabaseScreenInfo(screenInfo);
                }
            }
            else
            {
                screenInfo = _systemController.GetSystemScreenInfo();
                _systemController.SetDatabaseScreenInfo(screenInfo);
            }
            return screenInfo;
        }

        private IEnumerable<DateTimeFormat> GetDatetimeFormats()
        {
            return _systemController.GetDatetimeFormats();
        }

        private FontInfo GetFonts()
        {
            return new FontInfo
            {
                Fonts = _systemController.GetFonts(),
                Sizes = _systemController.GetFontSizes(),
                Indexes = _systemController.GetFontHeightIndex()
            };
        }
    }
}