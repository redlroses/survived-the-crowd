using System.Collections;
using System.Collections.Generic;

namespace Sources.Level
{
    public class CarUnlock : IEnumerator
    {
        private readonly List<ProgressBar> _carUnlocksProgress = new List<ProgressBar>
        {
            new ProgressBar(5, 5),
            new ProgressBar(10, 10)
        };

        private int currentBar;

        private ProgressBar _progress;

        public object Current => _progress;

        public CarUnlock(int currentBar, ProgressBar progress)
        {
            this.currentBar = currentBar;
            _progress = progress;
        }

        public bool MoveNext()
        {
            currentBar++;

            if (currentBar > _carUnlocksProgress.Count)
            {
                return false;
            }

            _progress = _carUnlocksProgress[currentBar];
            return true;
        }

        public void Reset()
        {
            _progress = _carUnlocksProgress[0];
        }
    }
}