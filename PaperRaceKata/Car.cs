namespace PaperRaceKata
{
    public class Car
    {
        public Car(Inertia inertia, Position position)
        {
            Inertia = inertia;
            Position = position;
        }

        public Inertia Inertia { get; }
        public Position Position { get; }

        public Car With(Adjustment adjustment)
        {
            return new Car(Inertia.Add(Inertia.DirectionFor(adjustment)), Position);
        }
    }
}