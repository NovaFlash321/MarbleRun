using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> musicTracks;
    [SerializeField] private AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = musicTracks[Random.Range(0,musicTracks.Count - 1)];
        musicSource.Play();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
