namespace PaperRaceKata
{
    public class Car
    {
        public Car(Inertia inertia)
        {
            Inertia = inertia;
            Position = new Position(0, 0);
        }

        public Inertia Inertia { get; }
        public Position Position { get; }

        public Car With(Adjustment adjustment)
        {
            return new Car(Inertia.Add(Inertia.DirectionFor(adjustment)));
        }
    }
}