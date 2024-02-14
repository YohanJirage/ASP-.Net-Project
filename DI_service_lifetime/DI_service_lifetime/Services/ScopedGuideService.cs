namespace DI_service_lifetime.Services
{
    public class ScopedGuideService : IScopedGuidService
    {


        private readonly Guid _instance;

        public ScopedGuideService()
        {
            _instance = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return _instance.ToString();
        }

    }
}
