﻿using System;
using AVFoundation;
using MediaManager.Media;
using MediaManager.Notifications;
using MediaManager.Platforms.Apple.Media;
using MediaManager.Platforms.Apple.Notifications;
using MediaManager.Platforms.Apple.Player;
using MediaManager.Platforms.Apple.Volume;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Volume;

namespace MediaManager
{
    public abstract class AppleMediaManagerBase<TMediaPlayer> : MediaManagerBase, IMediaManager<AVQueuePlayer> where TMediaPlayer : AppleMediaPlayer, IMediaPlayer<AVQueuePlayer>, new()
    {
        private IMediaPlayer _mediaPlayer;
        public override IMediaPlayer MediaPlayer
        {
            get
            {
                if (_mediaPlayer == null)
                {
                    _mediaPlayer = new TMediaPlayer();
                }
                return _mediaPlayer;
            }
            set => SetProperty(ref _mediaPlayer, value);
        }

        public AppleMediaPlayer AppleMediaPlayer => (AppleMediaPlayer)MediaPlayer;

        public AVQueuePlayer Player => ((AppleMediaPlayer)MediaPlayer).Player;

        private IMediaExtractor _mediaExtractor;
        public override IMediaExtractor MediaExtractor
        {
            get
            {
                if (_mediaExtractor == null)
                {
                    _mediaExtractor = new AppleMediaExtractor();
                }
                return _mediaExtractor;
            }
            set => SetProperty(ref _mediaExtractor, value);
        }

        private IVolumeManager _volumeManager;
        public override IVolumeManager VolumeManager
        {
            get
            {
                if (_volumeManager == null)
                    _volumeManager = new VolumeManager();
                return _volumeManager;
            }
            set => SetProperty(ref _volumeManager, value);
        }

        private INotificationManager _notificationManager;
        public override INotificationManager NotificationManager
        {
            get
            {
                if (_notificationManager == null)
                    _notificationManager = new NotificationManager();

                return _notificationManager;
            }
            set => SetProperty(ref _notificationManager, value);
        }

        public override TimeSpan Position
        {
            get
            {
                if (Player?.CurrentItem == null)
                {
                    return TimeSpan.Zero;
                }
                return TimeSpan.FromSeconds(Player.CurrentTime.Seconds);
            }
        }

        public override TimeSpan Duration
        {
            get
            {
                if (AppleMediaPlayer?.Player?.CurrentItem == null)
                {
                    return TimeSpan.Zero;
                }
                if (double.IsNaN(Player.CurrentItem.Duration.Seconds))
                {
                    return TimeSpan.Zero;
                }
                return TimeSpan.FromSeconds(Player.CurrentItem.Duration.Seconds);
            }
        }

        public override float Speed
        {
            get
            {
                if (AppleMediaPlayer?.Player != null)
                    return Player.Rate;
                return 0.0f;
            }
            set
            {
                if (AppleMediaPlayer?.Player != null)
                    Player.Rate = value;
            }
        }

        private AVPlayerLooper _looper;
        public override RepeatMode RepeatMode
        {
            get
            {
                return _looper != null ? RepeatMode.One : RepeatMode.All;
            }
            set
            {
                switch (value)
                {
                    case RepeatMode.Off:
                        _looper = null;
                        break;
                    case RepeatMode.One:
                    case RepeatMode.All:
                        _looper = AVPlayerLooper.FromPlayer(Player, Player.CurrentItem);
                        break;
                    default:
                        break;
                }
                //MediaPlayer.RepeatMode = value;
            }
        }
    }
}
