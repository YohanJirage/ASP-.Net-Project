namespace DI_service_lifetime.Services
{
    public class TransientGuideService : ITransientGuidService
    {

        private readonly Guid _instance;

        public TransientGuideService()
        {
            _instance = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return _instance.ToString();
        }

    }
}
