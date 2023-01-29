using System;

namespace Sources.Audio
{
    public interface IAudioPlayable
    {
        public event Action AudioPlayed;
    }
}