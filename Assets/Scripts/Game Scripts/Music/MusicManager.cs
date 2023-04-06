using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> playableMusicTracks;
    [SerializeField] List<AudioClip> DNBTracks, DubstepTracks, HouseTracks;
    [SerializeField] private AudioSource musicSource;
    public bool playDnb, playHouse, playDubstep;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SetMusicList();
        StartMusic();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void StartMusic()
    {
        int track = Random.Range(0, playableMusicTracks.Count);
        musicSource.clip = playableMusicTracks[track];
        musicSource.Play();
    }
    private void SetMusicList()
    {
        if(playDnb)
        {
            foreach(AudioClip _clip in DNBTracks)
            {
                playableMusicTracks.Add(_clip);
            }
        }
        if(playDubstep)
        {
            foreach(AudioClip _clip in DubstepTracks)
            {
                playableMusicTracks.Add(_clip);
            }
        }
        if(playHouse)
        {
            foreach(AudioClip _clip in HouseTracks)
            {
                playableMusicTracks.Add(_clip);
            }
        }
    }
    public void ToggleDubstep()
    {
        playDubstep = !playDubstep;   
    }
    public void ToggleDNB()
    {
        playDnb = !playDnb;
    }
    public void ToggleHouse()
    {
        playHouse = !playHouse;
    }

    public bool GetDubstep()
    {
        return playDubstep;
    }
    public bool GetDNB()
    {
        return playDnb;
    }
    public bool GetHouse()
    {
        return playHouse;
    }
    public void ApplyMusicSettings()
    {
        playableMusicTracks = new List<AudioClip>();
        SetMusicList();
        StartMusic();
    }
}

public enum MusicGenres
{
    DNB,
    HOUSE,
    DUBSTEP
}