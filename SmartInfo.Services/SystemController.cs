using DomainObjects;
using DomainObjects.Parameters;
using SmartInfo.ServiceInterfaces;

namespace SmartInfo.Services;

public class SystemController : ISystemController
{
    private readonly IBroker _broker;
    private readonly IUnitOfWork _unitOfWork;

    public SystemController(IUnitOfWorkFactory unitOfWorkFactory, IBroker broker)
    {
        _broker = broker;
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

    public ScreenInfo GetSystemScreenInfo()
    {
        throw new NotImplementedException();
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
        _unitOfWork.Displays.DeleteMany(existsDisplays);
        _unitOfWork.Displays.CreateMany(screenInfo.Displays);
        _unitOfWork.Complete();
    }

    public IEnumerable<int> GetFontSizes()
    {
        for (var i = 8; i <= 11; i++)
        {
            yield return i;
        }
        for (var i = 12; i <= 28; i+=2)
        {
            yield return i;
        }
        for (var i = 30; i <= 200; i += 10)
        {
            yield return i;
        }
    }

    public IEnumerable<string> GetFonts()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<double> GetFontHeightIndex() => new List<double>
    {
        0.1, 0.2, 0.5, 0.8, 1, 1.2, 1.5, 1.8, 2
    };

    public string GetVersion()
    {
        throw new NotImplementedException();
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