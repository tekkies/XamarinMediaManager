﻿using System;
using System.Windows.Controls;
using MediaManager.Media;
using MediaManager.Notifications;
using MediaManager.Platforms.Wpf.Media;
using MediaManager.Platforms.Wpf.Notificiations;
using MediaManager.Platforms.Wpf.Player;
using MediaManager.Platforms.Wpf.Volume;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Volume;

namespace MediaManager
{
    public class MediaManagerImplementation : MediaManagerBase, IMediaManager<MediaElement>
    {
        public MediaManagerImplementation()
        {

        }

        private IMediaPlayer _mediaPlayer;
        public override IMediaPlayer MediaPlayer
        {
            get
            {
                if (_mediaPlayer == null)
                    _mediaPlayer = new WpfMediaPlayer();
                return _mediaPlayer;
            }
            set => SetProperty(ref _mediaPlayer, value);
        }

        public WpfMediaPlayer WpfMediaPlayer => (WpfMediaPlayer)MediaPlayer;
        public MediaElement Player => WpfMediaPlayer.Player;

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

        private IMediaExtractor _mediaExtractor;
        public override IMediaExtractor MediaExtractor
        {
            get
            {
                if (_mediaExtractor == null)
                    _mediaExtractor = new MediaExtractor();
                return _mediaExtractor;
            }
            set => SetProperty(ref _mediaExtractor, value);
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

        public override TimeSpan Position => Player?.Position ?? TimeSpan.Zero;

        public override TimeSpan Duration => Player?.NaturalDuration.TimeSpan ?? TimeSpan.Zero;

        public override float Speed
        {
            get
            {
                return (float)Player.SpeedRatio;
            }
            set
            {
                Player.SpeedRatio = value;
            }
        }

        public override RepeatMode RepeatMode { get; set; }
    }
}
