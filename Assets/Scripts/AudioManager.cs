using System.Collections.Generic;
using UnityEngine;

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
            if (Tracks.ContainsKey(_CurrentlyPlaying))
            {
                Tracks[_CurrentlyPlaying].SetActive(false);
            }
            _CurrentlyPlaying = value;
            if (Tracks.ContainsKey(_CurrentlyPlaying))
            {
                Tracks[_CurrentlyPlaying].SetActive(true);
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
}
