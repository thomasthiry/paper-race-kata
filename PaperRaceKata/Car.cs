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

        public Car Apply(Adjustment adjustment)
        {
            var positionX = Position.x + Inertia._x;
            var positionY = Position.y + Inertia._y;
            var position = new Position(positionX, positionY);
            return new Car(Inertia.Add(Inertia.DirectionFor(adjustment)), position);
        }
    }
}