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
            var inertia = Inertia.Add(Inertia.DirectionFor(adjustment));
            var positionX = Position.x + inertia._x;
            var positionY = Position.y + inertia._y;
            var position = new Position(positionX, positionY);
            return new Car(inertia, position);
        }
    }
}