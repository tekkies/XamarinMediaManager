using MediaManager.Playback;

namespace MediaManager.Logging
{
    public interface ILogger
    {
        void Log(string message);
        void Log(object sender, PositionChangedEventArgs positionChangedEventArgs);
    }
}
