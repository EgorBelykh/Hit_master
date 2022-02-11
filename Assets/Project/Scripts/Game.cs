using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

[RequireComponent(
    typeof(WayHandler),
    typeof(InputHandler),
    typeof(UIController)
    )]
public class Game : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;

    private WayHandler wayHandler;
    private BatleManager batleManager;
    private Player player;
    private InputHandler inputHandler;
    private UIController uIController;

    private bool isGamePlay = false;
    private bool isGameInitialization = false;

    private void Awake()
    {
        batleManager = new BatleManager();
    }

    private void Start()
    {
        StartCoroutine(GameInitialization());
    }

    private IEnumerator GameInitialization()
    {
        uIController = GetComponent<UIController>();
        uIController.Initialization();
        yield return null;
        wayHandler = GetComponent<WayHandler>();
        wayHandler.Initialization();
        wayHandler.onWayFinishedEvent += LevelFinished;
        yield return null;
        player = Instantiate(playerPrefab);
        player.Initialization(wayHandler.GetPointAt(0));
        player.onPathCompleteEvent += PlayerPathComplete;
        var camera = FindObjectOfType<CinemachineVirtualCamera>();
        camera.Follow = player.transform;
        camera.LookAt = player.transform;
        yield return null;
        inputHandler = GetComponent<InputHandler>();
        inputHandler.onClickEvent += ClickInput;
        yield return null;
        isGameInitialization = true;
        inputHandler.EnableInput();
        uIController.StartScreenShow();
    }


    private void PlayerPathComplete()
    {
        
        if (wayHandler.GetIsBatle())
        {
            batleManager.StartBatle(wayHandler.GetEnemies());
            batleManager.onBatleCompleteEvent += BatleComplete;
            player.BatleStart();
            uIController.BatleScreenShow();
        }
        else
        {
            player.SetDistanation(wayHandler.GetNextPoint());
        }

    }

    private void ClickInput()
    {
        if (isGamePlay)
        {
            player.Click();
        }
        else
        {
            PlayerPathComplete();
            isGamePlay = true;
            uIController.CurrentScreenHide();
        }
    }

    private void LevelFinished()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    private void BatleComplete()
    {
        batleManager.onBatleCompleteEvent -= BatleComplete;
        player.BatleStop();
        player.SetDistanation(wayHandler.GetNextPoint());
        uIController.CurrentScreenHide();
        
    }
}


