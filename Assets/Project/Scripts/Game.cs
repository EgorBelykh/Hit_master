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
        var obj = new GameObject("Bullet Pool");
        var bulletStorage = obj.AddComponent<BulletStorage>();
        yield return null;
        wayHandler = GetComponent<WayHandler>();
        player = Instantiate(playerPrefab);
        yield return null;
        wayHandler.Initialization(bulletStorage);
        wayHandler.onWayFinishedEvent += LevelFinished;
        yield return null;
        player.Initialization(wayHandler.GetPointAt(0), bulletStorage);
        player.onPathCompleteEvent += PlayerPathComplete;
        player.onDiedEvent += LevelFinished;
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
            batleManager.StartBatle(wayHandler.GetEnemies(), player.transform);
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


