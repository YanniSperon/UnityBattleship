using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public enum MusicType
    {
        TitleScreen,
        InGame,
        GameLoss,
        GameWin
    };

    [System.Serializable]
    public class MusicEntry
    {
        public MusicType type;
        public GameObject obj;
    }

    // DO NOT CHANGE DURING RUNTIME WITHOUT UPDATING ENTRIES DICT
    // This is implemented like this because Unity does not support dictionaries natively in the inspector
    // (they are not serializable)
    [SerializeField]
    private List<MusicEntry> Entries = new List<MusicEntry>();

    [HideInInspector]
    private Dictionary<MusicType, GameObject> Tracks = new Dictionary<MusicType, GameObject>();



    private MusicType _CurrentlyPlaying;
    [HideInInspector]
    public MusicType CurrentlyPlayingTrack {
        get
        {
            return _CurrentlyPlaying;
        }
        set
        {
            GameObject obj = null;
            if (Tracks.TryGetValue(_CurrentlyPlaying, out obj))
            {
                obj.SetActive(false);
            }
            _CurrentlyPlaying = value;
            if (Tracks.TryGetValue(_CurrentlyPlaying, out obj))
            {
                obj.SetActive(true);
            }
        }
    }

    public MusicType TrackToStartWith = MusicType.TitleScreen;

    private void UpdateTracks()
    {
        Tracks.Clear();
        for (int i = 0; i < Entries.Count; i++)
        {
            Tracks.Add(Entries[i].type, Entries[i].obj);
        }
    }

    public void Awake()
    {
        UpdateTracks();
        CurrentlyPlayingTrack = TrackToStartWith;
    }

    public void Start()
    {
        SetMasterVolume(10.0f);
        SetMusicVolume(10.0f);
        SetSFXVolume(10.0f);
    }

    public AudioMixer mixer;

    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    public void Pause()
    {
        paused.TransitionTo(0.01f);
    }

    public void Unpause()
    {
        unpaused.TransitionTo(0.01f);
    }

    public void SetMasterVolume(float level)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10((level * 0.05f) + 0.0001f) * 40.0f);
    }

    public void SetMusicVolume(float level)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10((level * 0.05f) + 0.0001f) * 40.0f);
    }

    public void SetSFXVolume(float level)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10((level * 0.05f) + 0.0001f) * 40.0f);
    }
}
