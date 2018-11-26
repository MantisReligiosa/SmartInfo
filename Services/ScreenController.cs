using DataExchange;
using DataExchange.Requests;
using DataExchange.Responces;
using DomainObjects;
using DomainObjects.Specifications;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ScreenController : IScreenController
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string _screenWidthParameterName = "ScreenWidth";
        private const string _screenHeightParameterName = "ScreenHeight";

        public ScreenController(
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.Create();
        }

        public ScreenInfo GetDatabaseScreenInfo()
        {
            var widthParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.ByName(_screenWidthParameterName))).FirstOrDefault();
            var heightParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.ByName(_screenHeightParameterName))).FirstOrDefault();
            if (widthParameter != null && heightParameter != null)
                return new ScreenInfo
                {
                    Height = Convert.ToInt32(heightParameter.Value),
                    Width = Convert.ToInt32(widthParameter.Value)
                };
            return null;
        }

        public void SetDatabaseScreenInfo(ScreenInfo screenInfo)
        {
            var widthParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.ByName(_screenWidthParameterName))).FirstOrDefault();
            var heightParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.ByName(_screenHeightParameterName))).FirstOrDefault();
            if (widthParameter == null)
            {
                _unitOfWork.Parameters.Create(new Parameter
                {
                    Id = Guid.NewGuid(),
                    Name = _screenWidthParameterName,
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
                _unitOfWork.Parameters.Create(new Parameter
                {
                    Id = Guid.NewGuid(),
                    Name = _screenHeightParameterName,
                    Value = screenInfo.Height.ToString()
                });
            }
            else
            {
                heightParameter.Value = screenInfo.Height.ToString();
                _unitOfWork.Parameters.Update(heightParameter);
            }
            _unitOfWork.Complete();
        }

        public ScreenInfo GetSystemScreenInfo()
        {
            var broker = Broker.GetBroker();
            var responce = broker.GetResponce(new GetScreenSizeRequest()) as GetScreenSizeResponce;
            var details = new List<ScreenDetail>();

            foreach(var s in responce.Screens)
            {
                details.Add(new ScreenDetail
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
                ScreenDetails = details.ToArray()
            };
        }
    }
}
