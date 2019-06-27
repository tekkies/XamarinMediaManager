using System;
using System.Collections.Generic;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Video;
using Xamarin.Forms;

namespace MediaManager.Forms
{
    public class VideoView : View
    {
        protected IMediaManager MediaManager => CrossMediaManager.Current;

        public VideoView()
        {
            MediaManager.BufferingChanged += MediaManager_BufferingChanged;
            MediaManager.PositionChanged += MediaManager_PositionChanged;
            MediaManager.StateChanged += MediaManager_StateChanged;
            MediaManager.PropertyChanged += MediaManager_PropertyChanged;
        }

        private void MediaManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MediaManager.Duration):
                    Duration = MediaManager.Duration;
                    break;
                default:
                    break;
            }
        }

        private void MediaManager_StateChanged(object sender, StateChangedEventArgs e)
        {
            State = e.State;
        }

        private void MediaManager_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            Position = e.Position;
        }

        private void MediaManager_BufferingChanged(object sender, BufferingChangedEventArgs e)
        {
            Buffered = e.Buffered;
        }

        public static readonly BindableProperty VideoAspectProperty =
          BindableProperty.Create(nameof(VideoAspect), typeof(VideoAspectMode), typeof(VideoView), VideoAspectMode.AspectFit);

        public static readonly BindableProperty AutoPlayProperty =
         BindableProperty.Create(nameof(AutoPlay), typeof(bool), typeof(VideoView), true);

        public static readonly BindableProperty BufferedProperty =
         BindableProperty.Create(nameof(Buffered), typeof(TimeSpan), typeof(VideoView), TimeSpan.Zero);

        public static readonly BindableProperty StateProperty =
         BindableProperty.Create(nameof(State), typeof(MediaPlayerState), typeof(VideoView), MediaPlayerState.Stopped);

        public static readonly BindableProperty DurationProperty =
         BindableProperty.Create(nameof(Duration), typeof(TimeSpan), typeof(VideoView), null);

        /*public static readonly BindableProperty IsLoopingProperty =
         BindableProperty.Create(nameof(IsLooping), typeof(bool), typeof(VideoView), false);*/

        /*public static readonly BindableProperty KeepScreenOnProperty =
         BindableProperty.Create(nameof(KeepScreenOn), typeof(bool), typeof(VideoView), false);*/

        public static readonly BindableProperty PositionProperty =
         BindableProperty.Create(nameof(Position), typeof(TimeSpan), typeof(VideoView), TimeSpan.Zero);

        public static readonly BindableProperty ShowControlsProperty =
         BindableProperty.Create(nameof(ShowControls), typeof(bool), typeof(VideoView), false);

        public static readonly BindableProperty SourceProperty =
         BindableProperty.Create(nameof(Source), typeof(object), typeof(VideoView),
             propertyChanging: OnSourcePropertyChanging, propertyChanged: OnSourcePropertyChanged);

        /*public static readonly BindableProperty VideoHeightProperty =
         BindableProperty.Create(nameof(VideoHeight), typeof(int), typeof(VideoView));

        public static readonly BindableProperty VideoWidthProperty =
         BindableProperty.Create(nameof(VideoWidth), typeof(int), typeof(VideoView));

        public static readonly BindableProperty VolumeProperty =
         BindableProperty.Create(nameof(Volume), typeof(double), typeof(VideoView), 1.0, BindingMode.TwoWay, new BindableProperty.ValidateValueDelegate(ValidateVolume));*/

        /*private static bool ValidateVolume(BindableObject o, object newValue)
        {
            double d = (double)newValue;

            return d >= 0.0 && d <= 1.0;
        }*/

        public VideoAspectMode VideoAspect
        {
            get => (VideoAspectMode)GetValue(VideoAspectProperty);
            set => SetValue(VideoAspectProperty, value);
        }

        public bool AutoPlay
        {
            get { return (bool)GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        public TimeSpan Buffered
        {
            get { return (TimeSpan)GetValue(BufferedProperty); }
            internal set { SetValue(BufferedProperty, value); }
        }

        /*public bool CanSeek
        {
            get { return Source != null && Duration.HasValue; }
        }*/

        public MediaPlayerState State
        {
            get { return (MediaPlayerState)GetValue(StateProperty); }
            internal set { SetValue(StateProperty, value); }
        }

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            internal set { SetValue(DurationProperty, value); }
        }
        /*
        public bool IsLooping
        {
            get { return (bool)GetValue(IsLoopingProperty); }
            set { SetValue(IsLoopingProperty, value); }
        }*/

        /*public bool KeepScreenOn
        {
            get { return (bool)GetValue(KeepScreenOnProperty); }
            set { SetValue(KeepScreenOnProperty, value); }
        }*/

        public bool ShowControls
        {
            get { return (bool)GetValue(ShowControlsProperty); }
            set { SetValue(ShowControlsProperty, value); }
        }

        public TimeSpan Position
        {
            get { return (TimeSpan)GetValue(PositionProperty); }
            set
            {

            }
        }

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

       /* public int VideoHeight
        {
            get { return (int)GetValue(VideoHeightProperty); }
        }

        public int VideoWidth
        {
            get { return (int)GetValue(VideoWidthProperty); }
        }

        public double Volume
        {
            get
            {
                return (double)GetValue(VolumeProperty);
            }
            set
            {
                SetValue(VolumeProperty, value);
            }
        }*/

        private static async void OnSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            await CrossMediaManager.Current.Play(newvalue);
        }

        private static void OnSourcePropertyChanging(BindableObject bindable, object oldvalue, object newvalue)
        {
        }
        /*
        private static void OnShowControlsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(PlayerView != null)
                PlayerView.ShowControls = (bool)newValue;
        }

        private static void OnVideoAspectChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            if (PlayerView != null)
                PlayerView.VideoAspect = (VideoAspectMode)newValue;
        }

        private static void OnSourceChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            _ = CrossMediaManager.Current.Play(newValue);
        }*/
    }
}
