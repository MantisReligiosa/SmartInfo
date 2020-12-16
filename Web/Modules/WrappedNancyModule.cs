using AutoMapper;
using Nancy;
using Nancy.Security;
using NLog;
using System;
using Web.Models;
using Web.Profiles;

namespace Web.Modules
{
    public abstract class WrappedNancyModule : NancyModule
    {
        protected ILogger _logger;
        protected IMapper _mapper;

        public WrappedNancyModule()
        {
            this.RequiresAuthentication();
            _logger = LogManager.GetCurrentClassLogger();
            _mapper = AutoMapperConfig.Mapper;
        }

        protected Func<dynamic, dynamic> Wrap<TResponceModel>(Func<TResponceModel> func, string errorMsg)
        {
            return parameters =>
                {
                    try
                    {
                        return Response.AsJson(func.Invoke());
                    }
                    catch (Exception ex)
                    {
                        var exception = new Exception(errorMsg, ex);
                        _logger.Error(exception);
                        return Response.AsJson(new ErrorResponce { ErrorMessage = errorMsg });
                    }
                };
        }

        protected Func<dynamic, dynamic> Wrap(Action action, string errorMsg)
        {
            return Wrap(() =>
            {
                action.Invoke();
                return true;
            },
            errorMsg);
        }
    }
}