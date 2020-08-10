namespace PaperRaceKata
{
    public class Car
    {
        public Car(Inertia inertia)
        {
            Inertia = inertia;
            Position = new Position(0, 0);
        }

        public Inertia Inertia { get; private set; }
        public Position Position { get; internal set; }

        public Car With(Adjustment adjustment)
        {
            return new Car(Inertia.Add(Inertia.DirectionFor(adjustment)));
        }
    }
}