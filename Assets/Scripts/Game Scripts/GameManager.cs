using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] GameCameras;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] GameObject GoalPoint;
    [SerializeField]private LevelState lState;
    [SerializeField] private int firstLevel;
    private Camera spawnCamera, goalCamera, playerCamera;
    [SerializeField]private int levelIndex;
    // Start is called before the first frame update
    void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(this);

        
    }

    bool hasLoadedLevel = false;
    // Update is called once per frame
    void Update()
    {


        if(SceneManager.GetActiveScene().buildIndex != levelIndex && !hasLoadedLevel) 
        {
            OnLevelLoad();
        }
        CheckGameState();
        

        
        // CheckForNewCamera();
    }



    private void CheckGameState()
    {
        if(lState == LevelState.LEVELSTART)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SpawnPlayer();
            }
        }
        if(lState == LevelState.PLAYERSPAWN)
        {
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponentInChildren<Camera>().gameObject.SetActive(true);
        }
        if(lState == LevelState.LEVELEND)
        {
            spawnCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(false);
            goalCamera.gameObject.SetActive(true);
        }
    }




    private void SpawnPlayer()
    {
        spawnCamera.gameObject.SetActive(false);
        spawnCamera.gameObject.SetActive(false);
        Instantiate(Player, SpawnPoint.transform);
        playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
        playerCamera.gameObject.SetActive(true);
        lState = LevelState.PLAYERSPAWN;
    }

    public GameObject[] GetCameras()
    {
        return GameCameras;
    }

    public void OnLevelLoad()
    {
        SetLevelState(LevelState.LEVELSTART);
        SpawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        if(SpawnPoint != null)
        {
            spawnCamera = SpawnPoint.GetComponentInChildren<Camera>();
            spawnCamera.gameObject.SetActive(true);
        }
        GoalPoint = GameObject.FindGameObjectWithTag("Goal");
        if(GoalPoint != null)
        {
            goalCamera = GoalPoint.GetComponentInChildren<Camera>();
            goalCamera.gameObject.SetActive(false);
        }
        hasLoadedLevel = true;
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void SetLevelState(LevelState _state)
    {
        lState = _state;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void GetLevelAssets()
    {
        
    }


    /// <summary>
    ///Loads the first level of the game
    ///  </summary>
    public void StartGame()
    {
        
        SceneManager.LoadSceneAsync(firstLevel, LoadSceneMode.Single);
        

    }

}

public enum LevelState{
    LEVELSTART,
    PLAYERSPAWN,
    LEVELEND,
    MENU,
    PAUSE,
    LOAD
}
