namespace PaperRaceKata
{
    public class Car
    {
        private readonly Inertia _inertia;

        public Car(Inertia inertia, Position position)
        {
            _inertia = inertia;
            Position = position;
        }

        public Position Position { get; }

        public Car Apply(Adjustment adjustment)
        {
            var inertia = _inertia.Add(Inertia.DirectionFor(adjustment));
            var positionX = Position.x + inertia._x;
            var positionY = Position.y + inertia._y;
            var position = new Position(positionX, positionY);
            return new Car(inertia, position);
        }
    }
}