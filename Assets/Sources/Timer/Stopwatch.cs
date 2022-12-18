namespace Sources.Timer
{
    public sealed class Stopwatch
    {
        private float _elapsedTime;

        public void Tick(float deltaTime)
        {
            _elapsedTime += deltaTime;
        }

        public void Reset()
        {
            _elapsedTime = 0;
        }

        public string GetTime()
            => $"{_elapsedTime:00.00}";
    }
}
