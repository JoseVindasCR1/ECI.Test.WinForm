namespace ECI.Test.Shared.Models
{
    public class ClientDog
    {
        public int ClientId { get; set; }
        public int DogId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Dog Dog { get; set; }
    }
}
