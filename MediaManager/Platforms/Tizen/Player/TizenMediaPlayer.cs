﻿using System;
using System.Threading.Tasks;
using MediaManager.Media;
using MediaManager.Platforms.Tizen.Media;
using MediaManager.Platforms.Tizen.Video;
using MediaManager.Player;
using MediaManager.Video;
using Tizen.Multimedia;
using TizenPlayer = Tizen.Multimedia.Player;

namespace MediaManager.Platforms.Tizen.Player
{
    public class TizenMediaPlayer : IMediaPlayer<TizenPlayer, VideoView>
    {
        protected MediaManagerImplementation MediaManager = CrossMediaManager.Tizen;

        public TizenMediaPlayer()
        {
        }

        private TizenPlayer _player;
        public TizenPlayer Player
        {
            get
            {
                if (_player == null)
                    Initialize();
                return _player;
            }
            set
            {
                _player = value;
            }
        }

        protected virtual void Initialize()
        {
            Player = new TizenPlayer();
            Player.ErrorOccurred += Player_ErrorOccurred;
            Player.PlaybackInterrupted += Player_PlaybackInterrupted;
            Player.PlaybackCompleted += Player_PlaybackCompleted;
            Player.BufferingProgressChanged += Player_BufferingProgressChanged;
        }

        private void Player_BufferingProgressChanged(object sender, BufferingProgressChangedEventArgs e)
        {
            //TODO: Percent is not correct here
            MediaManager.Buffered = TimeSpan.FromMilliseconds(e.Percent);
        }

        private void Player_PlaybackCompleted(object sender, EventArgs e)
        {
            MediaManager.OnMediaItemFinished(this, new MediaItemEventArgs(MediaManager.MediaQueue.Current));
        }

        private void Player_PlaybackInterrupted(object sender, PlaybackInterruptedEventArgs e)
        {

        }

        private void Player_ErrorOccurred(object sender, PlayerErrorOccurredEventArgs e)
        {
            MediaManager.OnMediaItemFailed(this, new MediaItemFailedEventArgs(MediaManager.MediaQueue.Current, new Exception(e.ToString()), e.ToString()));
        }

        public bool AutoAttachVideoView { get; set; } = true;

        public IVideoView VideoView { get; set; }

        public VideoView PlayerView => VideoView as VideoView;

        public event BeforePlayingEventHandler BeforePlaying;
        public event AfterPlayingEventHandler AfterPlaying;

        public Task Pause()
        {
            Player.Pause();
            return Task.CompletedTask;
        }

        public async Task Play(IMediaItem mediaItem)
        {
            Player.SetSource(mediaItem.ToMediaSource());
            await Player.PrepareAsync();
            Player.Start();
        }

        public Task Play()
        {
            Player.Start();
            return Task.CompletedTask;
        }

        public async Task SeekTo(TimeSpan position)
        {
            //TODO: Probably not good
            await Player.SetPlayPositionAsync(Convert.ToInt32(position.TotalMilliseconds), false);
        }

        public Task Stop()
        {
            Player.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Player.ErrorOccurred -= Player_ErrorOccurred;
            Player.PlaybackInterrupted -= Player_PlaybackInterrupted;
            Player.PlaybackCompleted -= Player_PlaybackCompleted;
            Player.BufferingProgressChanged -= Player_BufferingProgressChanged;
        }
    }
}
