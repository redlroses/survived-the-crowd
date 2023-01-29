using System;

namespace Sources.Audio
{
    public interface IAudioStoppable : IAudioPlayable
    {
        public event Action AudioStopped;
    }
}