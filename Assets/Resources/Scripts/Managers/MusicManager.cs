using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip song;
    SoundManager _sound;
    void Start()
    {
        if (_sound == null) _sound = FindObjectOfType<SoundManager>();
        _sound.Play(song,true,0.2f);
    }
}