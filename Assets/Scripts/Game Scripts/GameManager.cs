using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] GameCameras;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isSpawned)
        {
            SpawnPlayer();
        }
        CheckForNewCamera();
        
    }

    private bool isSpawned = false;
    private void CheckForNewCamera()
    {
        
        if(GameObject.FindGameObjectsWithTag("Camera").Length > 1 && !isSpawned ) 
        {
            GameCameras = GameObject.FindGameObjectsWithTag("Camera");
            GameCameras[0].SetActive(false);
            isSpawned = true;
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(Player, SpawnPoint.transform);
    }

    public GameObject[] GetCameras()
    {
        return GameCameras;
    }
}
