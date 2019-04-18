using AutoMapper;
using Nancy;
using Nancy.Security;
using NLog;
using System;
using Web.Profiles;

namespace Web.Modules
{
    public abstract class WrappedNancyModule : NancyModule
    {
        protected ILogger _logger;
        protected IMapper _mapper = AutoMapperConfig.Mapper;

        public WrappedNancyModule()
        {
            this.RequiresAuthentication();
            _logger = LogManager.GetCurrentClassLogger();
        }

        protected Func<dynamic, dynamic> Wrap<TResponceModel>(Func<TResponceModel> func, string errorMsg)
        {
            try
            {
                return parameters =>
                {
                    return Response.AsJson(func.Invoke());
                };
            }
            catch (Exception ex)
            {
                var exception = new Exception(errorMsg, ex);
                _logger.Error(exception);
                throw exception;
            }
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