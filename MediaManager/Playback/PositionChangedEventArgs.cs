using System;

namespace MediaManager.Playback
{
    public class PositionChangedEventArgs : EventArgs
    {
        public PositionChangedEventArgs(TimeSpan position)
        {
            Position = position;
        }

        public TimeSpan Position { get; }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
