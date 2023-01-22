namespace Infrastructure.Services
{
    // Service-locator instead of plugins DI.
    public class SL
    {
        //public static SLC Container => _instance ??= new SLC();

        private static SL _instance;

        public static void RegisterSingle<TService>(TService implementation) where TService : IService
            => Implementation<TService>.Instance = implementation;

        public static TService Single<TService>() where TService : IService
            => Implementation<TService>.Instance;

        private class Implementation<TService> where TService : IService
        {
            public static TService Instance;
        }
    }
}
