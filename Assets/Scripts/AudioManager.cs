using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer Mixer;

    public enum Sound
    {
        MenuInteraction,
        Sneeze
    }

    public AudioClip MenuBackgroundMusicClip;
    public AudioClip GameBackgroundChatterClip;
    public AudioClip GameBackgroundMusicClip;
    private AudioSource _backgroundChatterSource;
    private AudioSource _backgroundGameMusicSource;
    private AudioSource _backgroundMenuMusicSource;

    public AudioClip MenuInteraction;
    private AudioSource _menuInteractionSource;

    public AudioClip[] SneezeClips;
    private AudioSource _sneezeSource;

    public float MasterVolume
    {
        get
        {
            float vol; Mixer.GetFloat("Master Volume", out vol);
            return vol;
        }
        set
        {
            Mixer.SetFloat("Master Volume", LinearToDecibel(value));
        }
    }
    public float SFXVolume
    {
        get
        {
            float vol; Mixer.GetFloat("SFX Volume", out vol);
            return vol;
        }
        set
        {
            Mixer.SetFloat("SFX Volume", LinearToDecibel(value));
        }
    }

    public float GetMusicVolumeLinear()
    {
        float vol; Mixer.GetFloat("Music Volume", out vol);
        return DecibelToLinear(vol);
    }

    public float GetMusicVolumeDecibel()
    {
        float vol; Mixer.GetFloat("Music Volume", out vol);
        return vol;
    }

    public void SetMusicVolumeLinear(float linearValue)
    {
        Mixer.SetFloat("Music Volume", LinearToDecibel(linearValue));
    }

    public void SetMusicVolumeDecibel(float decibelVolume)
    {
        Mixer.SetFloat("Music Volume", decibelVolume);
    }

    private float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0.0f)
        {
            dB = 20.0f * Mathf.Log10(linear);
        }
        else
        {
            dB = -144.0f;
        }

        return dB;
    }

    private float DecibelToLinear(float decibel)
    {
        float dB;

        dB = Mathf.Pow(decibel, decibel / 20.0f);

        return dB;
    }

    public void OnSceneLoaded(int sceneBuildIndex)
    {
        switch (sceneBuildIndex)
        {
            case 0: // Main menu scene
                if (_backgroundMenuMusicSource)
                {
                    _backgroundMenuMusicSource.Play();
                    _backgroundChatterSource.Stop();
                    Debug.Log("Menu music");
                }
                break;
            default: // Game scenes
                if (_backgroundGameMusicSource)
                {
                    _backgroundGameMusicSource.Play();
                    _backgroundChatterSource.Play();
                    Debug.Log("Game music");
                }
                break;
        }
    }

    private void Start()
    {
        if (SneezeClips.Length == 0)
        {
            Debug.LogError("There are no sneeze clips in the audio manager! We need at least one");
        }

        CreateAndInitializeSource(ref _backgroundGameMusicSource, GameBackgroundMusicClip);
        SetSourceMixerGroupMixerGroup("Master/Music/Game Music", _backgroundGameMusicSource);
        _backgroundGameMusicSource.loop = true;

        CreateAndInitializeSource(ref _backgroundMenuMusicSource, MenuBackgroundMusicClip);
        SetSourceMixerGroupMixerGroup("Master/Music/Menu Music", _backgroundMenuMusicSource);
        _backgroundMenuMusicSource.loop = true;

        CreateAndInitializeSource(ref _backgroundChatterSource, GameBackgroundChatterClip);
        SetSourceMixerGroupMixerGroup("Master/Music/Background Chatter", _backgroundChatterSource);
        _backgroundChatterSource.loop = true;

        if (GameManager.CurrentGameSceneIndex == 0) // Main menu
        {
            _backgroundMenuMusicSource.Play();

            _backgroundGameMusicSource.Stop();
            _backgroundChatterSource.Stop();
        }
        else
        {
            _backgroundGameMusicSource.Play();
            _backgroundChatterSource.Play();

            _backgroundMenuMusicSource.Stop();
        }

        CreateAndInitializeSource(ref _menuInteractionSource, MenuInteraction);
        SetSourceMixerGroupMixerGroup("Master/SFX/Menu Interaction", _menuInteractionSource);

        for (int i = 0; i < SneezeClips.Length; i++)
        {
            CreateAndInitializeSource(ref _sneezeSource, SneezeClips[i]);
        }
        SetSourceMixerGroupMixerGroup("Master/SFX/Sneeze", _sneezeSource);
    }

    private void CreateAndInitializeSource(ref AudioSource source, AudioClip clip)
    {
        if (!source)
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.clip = clip;
        source.loop = false;
        source.playOnAwake = false;
    }

    private AudioMixerGroup SetSourceMixerGroupMixerGroup(string subPath, AudioSource source)
    {
        AudioMixerGroup group = Mixer.FindMatchingGroups(subPath)[0];
        source.outputAudioMixerGroup = group;
        return group;
    }

    private void OnGamePause(bool paused)
    {
        if (paused)
        {
            
        }
        else
        {
           
        }
    }

    public void PlaySound(Sound sound, bool restart = true)
    {
        switch (sound)
        {
            case Sound.MenuInteraction:
                if (restart || !_menuInteractionSource.isPlaying) _menuInteractionSource.Play();
                break;
            case Sound.Sneeze:
                if (restart || !_sneezeSource.isPlaying)
                {
                    int sneezeIndex = Random.Range(0, SneezeClips.Length);
                    _sneezeSource.clip = SneezeClips[sneezeIndex];
                    _sneezeSource.Play();
                }
                break;
            default:
                break;
        }
    }

    public void StopSound(Sound sound)
    {
        switch (sound)
        {
            case Sound.MenuInteraction:
                _menuInteractionSource.Stop();
                break;
            case Sound.Sneeze:
                _sneezeSource.Stop();
                break;
            default:
                break;
        }
    }
}
