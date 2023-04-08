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
    [SerializeField] GameObject PlayerDisplay;
    [SerializeField]private LevelState lState;
    [SerializeField] private int firstLevel;
    private Camera spawnCamera, goalCamera, playerCamera;
    [SerializeField]private int levelIndex;
    private float spawnPlayerAction, resetPlayerAction, continuePlayerAction, pausePlayerAction;
    private string spawnPlayerBind, resetPlayerBind, continuePlayerBind, pausePlayerBind;
    private PlayerControls controls;
    PlayerControls.MenuActionsActions mActions;

    // Start is called before the first frame update
    void Awake()
    {
        GetControls();
    }
    void Start()
    {
        
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(this);

        
    }
    private void GetControls()
    {
        controls = new PlayerControls();
        mActions = controls.MenuActions;

        mActions.SpawnPlayer.performed += ctx => spawnPlayerAction = ctx.ReadValue<float>();
        mActions.Continue.performed += ctx => continuePlayerAction = ctx.ReadValue<float>();
        mActions.ResetLevel.performed += ctx => resetPlayerAction = ctx.ReadValue<float>();
        mActions.Pause.performed += ctx => pausePlayerAction = ctx.ReadValue<float>();
        pausePlayerBind = GetBindName(mActions.Pause.bindings[0].path);
        continuePlayerBind = GetBindName(mActions.Continue.bindings[0].path);
        spawnPlayerBind = GetBindName(mActions.SpawnPlayer.bindings[0].path);
        resetPlayerBind = GetBindName(mActions.ResetLevel.bindings[0].path);
        
        Debug.Log(spawnPlayerBind);
        
    }

    bool hasLoadedLevel = false;
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(spawnPlayerAction);

        if(SceneManager.GetActiveScene().buildIndex != levelIndex && !hasLoadedLevel) 
        {
            OnLevelLoad();
        }
        CheckGameState();
        

        
        // CheckForNewCamera();
    }

    public void OnEnable()
    {
        controls.Enable();
    }
    public void OnDisable() 
    {
        controls.Disable();
    }

    private void CheckGameState()
    {
        if(lState == LevelState.MENU || lState == LevelState.PAUSE || lState == LevelState.LOAD) PlayerDisplay.SetActive(false);
        else PlayerDisplay.SetActive(true);
        switch(lState)
        {
            case LevelState.LEVELSTART:  
                if(spawnPlayerAction != 0)
                {
                    SpawnPlayer();
                }
                break;
            case LevelState.PLAYERSPAWN:
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponentInChildren<Camera>().gameObject.SetActive(true);
                break;
            case LevelState.LEVELEND:
                spawnCamera.gameObject.SetActive(false);
                playerCamera.gameObject.SetActive(false);
                goalCamera.gameObject.SetActive(true);
                if(continuePlayerAction != 0)
                {
                    hasLoadedLevel = false;
                    LoadLevel();
                }
                break;
            case LevelState.PLAYERFAIL:
                playerCamera.transform.SetParent(this.transform);
                break;


        }
    }

    public string GetBindName(string bindInput)
    {
        if(bindInput.Contains("<Keyboard>/")) bindInput = bindInput.Substring(11);
        string firstChar = bindInput.Substring(0,1);
        firstChar = firstChar.ToUpper();
        char first = firstChar[0];
        bindInput = bindInput.Replace(bindInput[0],first);
        return bindInput;
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
    public LevelState GetLevelState()
    {
        return lState;
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

    public void LoadLevel()
    {
        int level = GameObject.FindGameObjectWithTag("LevelData").GetComponent<LevelInfo>().GetLevelNumber();
        levelIndex = level;
        SceneManager.LoadSceneAsync($"Level{level + 1}", LoadSceneMode.Single);
    }
    public string GetSpawnBind()
    {
        return spawnPlayerBind;
    }
    public string GetResetBind()
    {
        return resetPlayerBind;
    }
    public string GetContinueBind()
    {
        return continuePlayerBind;
    }
    public string GetPauseBind()
    {
        return pausePlayerBind;
    }
}

public enum LevelState{
    LEVELSTART,
    PLAYERSPAWN,
    PLAYERFAIL,
    LEVELEND,
    MENU,
    PAUSE,
    LOAD
}
