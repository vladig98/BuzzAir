namespace BuzzAir.Models
{
    public class Priority : Service
    {
        public override decimal Price { get => GlobalConstants.PriorityPrice; set => base.Price = value; }

        public override string Name => this.GetType().Name;
    }
}
