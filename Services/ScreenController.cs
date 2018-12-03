using DataExchange;
using DataExchange.Requests;
using DataExchange.Responces;
using DomainObjects;
using DomainObjects.Blocks;
using DomainObjects.Parameters;
using DomainObjects.Specifications;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Videopanel GetDatabaseScreenInfo()
        {
            var widthParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.OfType<ScreenWidth>())).FirstOrDefault();
            var heightParameter = (_unitOfWork.Parameters.Find(ParameterSpecification.OfType<ScreenHeight>())).FirstOrDefault();
            if (widthParameter != null && heightParameter != null)
                return new Videopanel
                {
                    Height = Convert.ToInt32(heightParameter.Value),
                    Width = Convert.ToInt32(widthParameter.Value),
                    Displays = _unitOfWork.Displays.GetAll().ToArray()
                };
            return null;
        }

        public void SetDatabaseScreenInfo(Videopanel screenInfo)
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

        public Videopanel GetSystemScreenInfo()
        {
            var broker = Broker.GetBroker();
            var responce = broker.GetResponce(new GetScreenSizeRequest()) as GetScreenSizeResponce;
            var details = new List<Display>();

            foreach (var s in responce.Screens)
            {
                details.Add(new Display
                {
                    Id = Guid.NewGuid(),
                    Height = s.Height,
                    Left = s.Left,
                    Top = s.Top,
                    Width = s.Width
                });
            }

            return new Videopanel
            {
                Height = responce.Height,
                Width = responce.Width,
                Displays = details.ToArray()
            };
        }

        public TextBlock AddTextBlock()
        {
            var block = _unitOfWork.DisplayBlocks.Create(new TextBlock
            {
                Height = 50,
                Width = 200,
                Text = "Текст"
            }) as TextBlock;
            _unitOfWork.Complete();
            return block;
        }
    }
}
