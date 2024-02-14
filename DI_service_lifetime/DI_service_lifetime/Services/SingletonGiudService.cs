namespace DI_service_lifetime.Services
{
    public class SingletonGiudService : ISingeltonGuidService
    {

        private readonly Guid _instance;

        public SingletonGiudService( )
        {
            _instance = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return _instance.ToString();
        }

       
    }
}
