namespace SuperHeroAPI.Models
{
    public class ActorJohnWick:IActor
    {
        public ActorJohnWick()
        {

        }
        public string ActorName()
        {
            return "John Wick";
        }
    }

    public class ActorBruceLee : IActor
    {
        public ActorBruceLee()
        {

        }
        public string ActorName()
        {
            return "Bruce Lee";
        }
    }
}
