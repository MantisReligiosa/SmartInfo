﻿using AutoMapper;
using DataExchange;
using DataExchange.Requests;
using DataExchange.Responces;
using DomainObjects;
using DomainObjects.Parameters;
using DomainObjects.Specifications;
using ServiceInterfaces;
using Services.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class SystemController : ISystemController
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string _screenWidthParameterName = "ScreenWidth";
        private const string _screenHeightParameterName = "ScreenHeight";
        private IMapper _mapper => AutoMapperConfig.Mapper;

        public SystemController(
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.Create();
        }

        public ScreenInfo GetDatabaseScreenInfo()
        {
            var widthParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.OfType<ScreenWidth>())).FirstOrDefault();
            var heightParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.OfType<ScreenHeight>())).FirstOrDefault();
            if (widthParameter != null && heightParameter != null && _unitOfWork.Displays.Count(d => true) > 0)
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
            var widthParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.OfType<ScreenWidth>())).FirstOrDefault();
            var heightParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.OfType<ScreenHeight>())).FirstOrDefault();
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

            _unitOfWork.Displays.DeleteRange(_unitOfWork.Displays.GetAll());
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
            yield return 15;
            for (int i = 20; i <= 200; i += 10)
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
            var details = new List<Display>();

            return responce.Fonts;
        }

        public string GetVersion()
        {
            var broker = Broker.GetBroker();
            var responce = broker.GetResponce(new GetVersionRequest()) as GetVersionResponce;

            return $"v{responce.Major}.{responce.Minor}.{responce.Build}";
        }

        public IEnumerable<DateTimeFormat> GetDatetimeFormats()
        {
            return _unitOfWork.DateTimeFormats.GetAll();
        }
    }
}
