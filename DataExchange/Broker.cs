using System;
using System.Collections.Generic;

namespace DataExchange
{
    public class Broker
    {
        private static Broker _broker;
        private static readonly object syncRoot = new object();
        private readonly Dictionary<Type, Func<IRequest, IResponce>> _handlers = new Dictionary<Type, Func<IRequest, IResponce>>();

        public static Broker GetBroker()
        {
            if (_broker == null)
            {
                lock (syncRoot)
                {
                    if (_broker == null)
                        _broker = new Broker();
                }
            }
            return _broker;
        }

        public void RegisterHandler<TRequest>(Func<IRequest, IResponce> handler)
            where TRequest : IRequest
        {
            _handlers[typeof(TRequest)] = handler;
        }

        public IResponce GetResponce<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            var handler = _handlers[request.GetType()];
            return handler.Invoke(request);
        }
    }
}
