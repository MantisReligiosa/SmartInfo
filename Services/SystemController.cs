using DataExchange;
using DataExchange.Requests;
using DataExchange.Responces;
using DomainObjects;
using DomainObjects.Parameters;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class SystemController : ISystemController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemController(
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.Create();
        }

        public ScreenInfo GetDatabaseScreenInfo()
        {
            var widthParameter = _unitOfWork.Parameters.ScreenWidth;
            var heightParameter = _unitOfWork.Parameters.ScreenHeight;
            if (widthParameter != null && heightParameter != null && _unitOfWork.Displays.Count() > 0)
                return new ScreenInfo
                {
                    Height = Convert.ToInt32(heightParameter.Value),
                    Width = Convert.ToInt32(widthParameter.Value),
                    Displays = _unitOfWork.Displays.GetAll().ToArray()
                };
            return null;
        }

        public void SetDatabaseScreenInfo(ScreenInfo screenInfo)
        {
            var widthParameter = _unitOfWork.Parameters.ScreenWidth;
            var heightParameter = _unitOfWork.Parameters.ScreenHeight;
            if (widthParameter == null)
            {
                _unitOfWork.Parameters.Create(new ScreenWidth
                {
                    Value = screenInfo.Width.ToString()
                });
            }
            else
            {
                widthParameter.Value = screenInfo.Width.ToString();
                _unitOfWork.Parameters.Update(widthParameter);
            }
            if (heightParameter == null)
            {
                _unitOfWork.Parameters.Create(new ScreenHeight
                {
                    Value = screenInfo.Height.ToString()
                });
            }
            else
            {
                heightParameter.Value = screenInfo.Height.ToString();
                _unitOfWork.Parameters.Update(heightParameter);
            }

            var existsDisplays = _unitOfWork.Displays.GetAll();
            _unitOfWork.Displays.DeleteRange(existsDisplays);
            _unitOfWork.Displays.CreateMany(screenInfo.Displays);
            _unitOfWork.Complete();
        }

        public ScreenInfo GetSystemScreenInfo()
        {
            var broker = Broker.GetBroker();
            var responce = broker.GetResponce(new GetScreenSizeRequest()) as GetScreenSizeResponce;
            var details = new List<Display>();

            foreach (var s in responce.Screens)
            {
                details.Add(new Display
                {
                    Height = s.Height,
                    Left = s.Left,
                    Top = s.Top,
                    Width = s.Width
                });
            }

            return new ScreenInfo
            {
                Height = responce.Height,
                Width = responce.Width,
                Displays = details.ToArray()
            };
        }

        public IEnumerable<int> GetFontSizes()
        {
            for (int i = 8; i <= 11; i++)
            {
                yield return i;
            }
            for (int i = 12; i <= 28; i+=2)
            {
                yield return i;
            }
            for (int i = 30; i <= 200; i += 10)
            {
                yield return i;
            }
        }

        public IEnumerable<double> GetFontHeightIndex() => new List<double>
        {
            0.1, 0.2, 0.5, 0.8, 1, 1.2, 1.5, 1.8, 2
        };

        public IEnumerable<string> GetFonts()
        {
            var broker = Broker.GetBroker();
            var responce = broker.GetResponce(new GetFontsRequest()) as GetFontsResponce;
            return responce.Fonts;
        }

        public string GetVersion()
        {
            var broker = Broker.GetBroker();
            var responce = broker.GetResponce(new GetVersionRequest()) as GetVersionResponce;

            return $"v{responce.Major}.{responce.Minor} build {responce.Build}";
        }

        public IEnumerable<DateTimeFormat> GetDatetimeFormats()
        {
            return _unitOfWork.DateTimeFormats.GetAll();
        }

        public IEnumerable<SizeUnit> GetSizeUnits()
        {
            yield return new SizeUnit { SizeUnits = SizeUnits.Auto, Name = "Авто" };
            yield return new SizeUnit { SizeUnits = SizeUnits.Pecent, Name = "%" };
        }
    }
}
