namespace MonoGameLibrary.Audio;

public class AudioController : IDisposable
{
    private readonly List<SoundEffectInstance> activeSoundEffects;

    private float previousSongVolume;
    private float previousSoundEffectVolume;

    public bool IsMuted { get; private set; }

    public bool IsDisposed { get; private set; }

    public AudioController()
    {
        activeSoundEffects = new List<SoundEffectInstance>();
    }

    ~AudioController()
    {
        Dispose(false);
    }

    public float SongVolume
    {
        get => IsMuted ? 0 : MediaPlayer.Volume;
        set => MediaPlayer.Volume = Math.Clamp(value, 0, 1);
    }

    public float SoundEffectVolume
    {
        get => IsMuted ? 0 : SoundEffect.MasterVolume;
        set => SoundEffect.MasterVolume = Math.Clamp(value, 0, 1);
    }

    public void Update()
    {
        // Start removing from end of list so that RemoveAt does not shift the indices of the remaining elements
        for (int i = activeSoundEffects.Count - 1; i >= 0; i--)
        {
            var instance = activeSoundEffects[i];
            if (instance.State == SoundState.Stopped)
            {
                if (!instance.IsDisposed)
                {
                    instance.Dispose();
                }
                activeSoundEffects.RemoveAt(i);
            }
        }
    }

    public SoundEffectInstance PlaySoundEffect(SoundEffect soundEffect)
    {
        return PlaySoundEffect(soundEffect, 1, 1, 0, false);
    }

    public SoundEffectInstance PlaySoundEffect(SoundEffect soundEffect, float volume, float pitch, float pan, bool isLooped)
    {
        SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();

        soundEffectInstance.Volume = volume;
        soundEffectInstance.Pitch = pitch;
        soundEffectInstance.Pan = pan;
        soundEffectInstance.IsLooped = isLooped;

        soundEffectInstance.Play();

        activeSoundEffects.Add(soundEffectInstance);

        return soundEffectInstance;
    }

    public void PlaySong(Song song, bool isRepeating = true)
    {
        if (MediaPlayer.State == MediaState.Playing)
        {
            MediaPlayer.Stop();
        }

        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = isRepeating;
    }

    public void PauseAudio()
    {
        MediaPlayer.Pause();
        foreach (var instance in activeSoundEffects)
        {
            instance.Pause();
        }
    }

    public void ResumeAudio()
    {
        MediaPlayer.Resume();
        foreach (var instance in activeSoundEffects)
        {
            instance.Resume();
        }
    }

    public void MuteAudio()
    {
        previousSongVolume = MediaPlayer.Volume;
        previousSoundEffectVolume = SoundEffect.MasterVolume;
        MediaPlayer.Volume = 0;
        SoundEffect.MasterVolume = 0;
        IsMuted = true;
    }

    public void UnmuteAudio()
    {
        MediaPlayer.Volume = previousSongVolume;
        SoundEffect.MasterVolume = previousSoundEffectVolume;
        IsMuted = false;
    }

    public void ToggleMute()
    {
        if (IsMuted)
        {
            UnmuteAudio();
        }
        else
        {
            MuteAudio();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            foreach (var instance in activeSoundEffects)
            {
                instance.Dispose();
            }
            activeSoundEffects.Clear();
        }

        IsDisposed = true;
    }
}