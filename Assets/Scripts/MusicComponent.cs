using UnityEngine;

public class MusicComponent : MonoBehaviour
{
    public AudioSource s = null;

    void OnEnable()
    {
        s.Play();
    }

    void OnDisable()
    {
        s.Stop();
    }
}
