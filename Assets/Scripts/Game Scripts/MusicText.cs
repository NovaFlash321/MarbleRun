using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicText : MonoBehaviour
{
    [SerializeField] MusicManager mManager;
    [SerializeField] MusicGenres mGenres;
    bool isEnabled = false;
    [SerializeField ]TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetGenreEnable(); 
        if(isEnabled) text.text = "ON";
        else text.text = "OFF";
    }


    public void GetGenreEnable()
    {
        switch(mGenres)
        {
            case MusicGenres.DNB: isEnabled = mManager.GetDNB(); break;
            case MusicGenres.HOUSE: isEnabled = mManager.GetHouse(); break;
            case MusicGenres.DUBSTEP: isEnabled = mManager.GetDubstep(); break;
        }
    }
}

