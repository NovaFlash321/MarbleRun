using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHudManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumber, levelName, playerTimer, endTimer, resetText, spawnText;
    [SerializeField] private GameObject LevelInfo, PlayerHud, LevelWin, LevelFail;
    private LevelInfo lInfo;
    private LevelState lstate;
    private string timerToText;
    private float levelTimer;
    private PlayerControls controls;
    private PlayerControls.MenuActionsActions mActions;
    private float resetMenuAction, pauseMenuAction;
    private string resetKey, pauseKey;
    // Start is called before the first frame update
    void Start()
    {
        GetMenuActions();
        DontDestroyOnLoad(this);
    }
    private void GetMenuActions()
    {
        controls = new PlayerControls();
        mActions = controls.MenuActions;

        mActions.ResetLevel.performed += ctx => resetMenuAction = ctx.ReadValue<float>();
        mActions.Pause.performed += ctx => pauseMenuAction = ctx.ReadValue<float>();
        resetKey = mActions.ResetLevel.bindings[0].overridePath;
        pauseKey = mActions.Pause.bindings[0].overridePath;
        
            }

    // Update is called once per frame
    void Update()
    {
        CheckLevelState();   
    }

    string timerMinutes;
    string timerSeconds;
    string timerMilliseconds;
    private void CheckLevelState()
    {
        lstate = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetLevelState();
        switch (lstate)
        {
            case LevelState.LEVELSTART: 
                LevelInfo.SetActive(true);
                PlayerHud.SetActive(false);
                LevelWin.SetActive(false);
                LevelFail.SetActive(false);
                lInfo = GameObject.FindGameObjectWithTag("LevelData").GetComponent<LevelInfo>();
                int num = lInfo.GetLevelNumber();
                string name = lInfo.GetLevelName();
                levelNumber.text = $"LEVEL {num}";
                levelName.text = $"{name}";
                levelTimer = 0f;
                string spawnBind = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GetSpawnBind();
                spawnText.text = $"PRESS {spawnBind} TO START";
                
                break;
            case LevelState.PLAYERSPAWN:
            
                LevelInfo.SetActive(false);
                PlayerHud.SetActive(true);
                LevelWin.SetActive(false);
                LevelFail.SetActive(false);
                levelTimer += Time.deltaTime;
                int seconds = Mathf.FloorToInt(levelTimer);
                int minutes = (seconds / 60);
                timerMinutes = minutes.ToString();
                timerSeconds = seconds.ToString();
                timerMilliseconds = (levelTimer - Mathf.Floor(levelTimer)).ToString(".##");
                playerTimer.text = $"{timerMinutes}:{timerSeconds}{timerMilliseconds}";
                break;    
            case LevelState.LEVELEND:
            
                LevelInfo.SetActive(false);
                PlayerHud.SetActive(false);
                LevelWin.SetActive(true);
                LevelFail.SetActive(false);
                endTimer.text = $"TIME: {timerMinutes}:{timerSeconds}{timerMilliseconds}";
                break;
                
            case LevelState.MENU:
                LevelInfo.SetActive(false);
                PlayerHud.SetActive(false);
                LevelWin.SetActive(false);
                LevelFail.SetActive(false);
                break;
            case LevelState.PLAYERFAIL:
                LevelInfo.SetActive(false);
                PlayerHud.SetActive(false);
                LevelWin.SetActive(false);
                LevelFail.SetActive(true);
                resetText.text = $"PRESS {resetKey} TO RESTART";
                break;

        }
    }


}
