using AutoMapper;
using System.Reflection;

namespace Web.Profiles
{
    internal static class AutoMapperConfig
    {
        private static IMapper _mapper;

        private static readonly object SyncRoot = new object();

        /// <summary>
        /// Маппер
        /// </summary>
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    lock (SyncRoot)
                    {
                        if (_mapper == null)
                        {
                            var currentAssembly = Assembly.GetAssembly(typeof(AutoMapperConfig));

                            var config = new MapperConfiguration(cfg =>
                            {
                                cfg.AddMaps(currentAssembly);
                            });

                            _mapper = config.CreateMapper();
                        }
                    }
                }

                return _mapper;
            }
        }
    }
}
