namespace Sources.Input
{
    public interface ICarControllable : IControllable
    {
        public void Accelerate();

        public void Decelerate();
    }
}