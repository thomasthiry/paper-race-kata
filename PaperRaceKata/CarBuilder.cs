namespace PaperRaceKata
{
    public class CarBuilder
    {
        private Position _position = new Position(0, 0);
        private Inertia _inertia = new Inertia(0, 0);

        public static CarBuilder ACar()
        {
            return new CarBuilder();
        }

        public CarBuilder With(Position position)
        {
            this._position = position;
            return this;
        }

        public CarBuilder With(Inertia inertia)
        {
            _inertia = inertia;
            return this;
        }

        public Car Build()
        {
            return new Car(_inertia, _position);
        }
    }
}