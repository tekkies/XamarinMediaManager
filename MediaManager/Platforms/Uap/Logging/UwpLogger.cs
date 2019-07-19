using Windows.Media.Playback;
using MediaManager.Logging;

namespace MediaManager.Platforms.Uap
{
    internal class UwpLogger : Logger 
    {
        public void Log(MediaPlaybackSession mediaPlaybackSession, object args)
        {
            Log($"{mediaPlaybackSession.Position}");
        }

        public void Log(MediaPlayer mediaPlayer, object args)
        {
            Log($"{mediaPlayer.CurrentState}");
        }
    }
}
