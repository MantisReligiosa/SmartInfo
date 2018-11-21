using DomainObjects;
using DomainObjects.Specifications;
using ServiceInterfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class ScreenController : IScreenController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScreenInfoProvider _screenInfoProvider;
        private const string _screenWidthParameterName = "ScreenWidth";
        private const string _screenHeightParameterName = "ScreenHeight";

        public ScreenController(
            IUnitOfWorkFactory unitOfWorkFactory,
            IScreenInfoProvider screenInfoProvider)
        {
            _unitOfWork = unitOfWorkFactory.Create();
            _screenInfoProvider = screenInfoProvider;
        }

        public async Task<ScreenInfo> GetDatabaseScreenInfoAsync()
        {
            var widthParameter = (await _unitOfWork.Parameters.FindAsync(ParameterSpecification.ByName(_screenWidthParameterName))).FirstOrDefault();
            var heightParameter = (await _unitOfWork.Parameters.FindAsync(ParameterSpecification.ByName(_screenHeightParameterName))).FirstOrDefault();
            if (widthParameter != null && heightParameter != null)
                return new ScreenInfo
                {
                    Height = Convert.ToInt32(heightParameter.Value),
                    Width = Convert.ToInt32(widthParameter.Value)
                };
            return null;
        }

        public async void SetDatabaseScreenInfoAsync(ScreenInfo screenInfo)
        {
            var widthParameter = (await _unitOfWork.Parameters.FindAsync(ParameterSpecification.ByName(_screenWidthParameterName))).FirstOrDefault();
            var heightParameter = (await _unitOfWork.Parameters.FindAsync(ParameterSpecification.ByName(_screenHeightParameterName))).FirstOrDefault();
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
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ScreenInfo> GetSystemScreenInfoAsync()
        {
            return _screenInfoProvider.GetScreenInfo();
        }
    }
}
