using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private int levelNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GetLevelName()
    {
        return levelName;
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }
}
