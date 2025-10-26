using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Tooltip("Change here movement speed as well as direction of movement")]
    [Header("Movement Speed and Direction")]
    [SerializeField] private float pigMovementSpeed = -5;
    [SerializeField] private float birdMovementSpeed = 5;

    [Header("TapInputMode")]
    [SerializeField] private int maxTaps = 4;
    [SerializeField] private float movementTime = 0.3f;

    [Header("HoldInputMode")]
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private float catchDistance = 3.8f; // Distance at which bird catches pig

    [Header("Win Components")]
    [TextArea]
    [SerializeField] private string wonText;
    [Space(8)]
    [SerializeField] private GameObject gameWonCanvas;
    [SerializeField] private TextMeshProUGUI wonTextMeshPro;

    [Header("Controllers")]
    [SerializeField] private BirdController birdController;
    [SerializeField] private PigController pigController;
    [SerializeField] private BGM_AudioController bGM_AudioController;

    [Header("Managers")]
    [SerializeField] private ProgressUIController progressUIController;
    [SerializeField] private ParallaxManager parallaxManager;
    [SerializeField] private GameModeManager gameModeManager;

    private InputSystem_Base inputSystem;
    private InputAction jump_Action;

    private void Awake()
    {
        InitializeInputSystem();
    }
    private void Start()
    {
        InitializeParallax();
    }
    private void OnDestroy()
    {
        CleanUpInputSystem();
    }
    private void CleanUpInputSystem()
    {
        UnSubscribeEvent();
        inputSystem?.Dispose();
        jump_Action?.Dispose();
    }

    private void InitializeInputSystem()
    {
        inputSystem = new InputSystem_Base();
        inputSystem.Enable();
        jump_Action = inputSystem.Player.Jump;
    }
    private void InitializeParallax()
    {
        parallaxManager.SpawnParallax();
        parallaxManager.SetPosition();
    }

    private void SubscribeEvents()
    {
        if (birdController.baseInput != null)
        {
            jump_Action.performed += birdController.PlayerJump_Action_Performed;
            jump_Action.canceled += birdController.PlayerJump_Action_Canceled;
            jump_Action.performed += pigController.PlayerJump_Action_Performed;
            jump_Action.canceled += pigController.PlayerJump_Action_Canceled;
            birdController.baseInput.progress_Action += OnGameProgressChanged;
        }

        progressUIController.OnProgressChanged_Action += CheckWinCondition;
    }
    private void UnSubscribeEvent()
    {
        if (birdController.baseInput != null)
        {
            jump_Action.performed -= birdController.PlayerJump_Action_Performed;
            jump_Action.canceled -= birdController.PlayerJump_Action_Canceled;
            jump_Action.performed -= pigController.PlayerJump_Action_Performed;
            jump_Action.canceled -= pigController.PlayerJump_Action_Canceled;
            birdController.baseInput.progress_Action -= OnGameProgressChanged;
        }

        progressUIController.OnProgressChanged_Action -= CheckWinCondition;
    }
    private void HandleEnemyCaught()
    {
        birdController.OnCatch();
        pigController.OnCatch();
        parallaxManager.StopParallax();
    }

    //Called by UI Button
    public void Play(int gameMode)
    {
        SetGameMode(gameMode);
        SubscribeEvents();
        progressUIController.Enable();
        pigController.GameStarted();
        birdController.GameStarted();
        parallaxManager.StartParallax();
        bGM_AudioController.PlayBGM();
    }
    private void SetGameMode(int gameModeIndex)
    {
        System.Type gameModeType = gameModeManager.SelectGameMode(gameModeIndex);
        switch (gameModeIndex)
        {
            case 0:
                TapInputMode birdTapInput = (TapInputMode)birdController.gameObject.AddComponent(gameModeType);
                birdController.baseInput = birdTapInput;
                birdTapInput.Initialize(maxTaps, movementTime, birdMovementSpeed);

                TapInputMode pigTapInput = (TapInputMode)pigController.gameObject.AddComponent(gameModeType);
                pigController.baseInput = pigTapInput;
                pigTapInput.Initialize(maxTaps, movementTime, pigMovementSpeed);
                break;
            case 1:

                HoldInputMode birdHoldInput = (HoldInputMode)birdController.gameObject.AddComponent(gameModeType);
                birdController.baseInput = birdHoldInput;
                birdHoldInput.Initialize(enemyTransform, catchDistance, birdMovementSpeed);

                HoldInputMode pigHoldInput = (HoldInputMode)pigController.gameObject.AddComponent(gameModeType);
                pigController.baseInput = pigHoldInput;
                pigHoldInput.Initialize(enemyTransform, catchDistance, pigMovementSpeed);
                break;
            default:
                pigController.baseInput = null;
                birdController.baseInput = null;
                break;
        }

    }

    public void GameWon()
    {
        HandleEnemyCaught();
        gameWonCanvas.SetActive(true);
        wonTextMeshPro.SetText(wonText);
        bGM_AudioController.StopBGM();
        UnSubscribeEvent();
    }

    private void OnGameProgressChanged(float progress)
    {
        progressUIController.UpdateProgress(progress);
    }
    private void CheckWinCondition(float progress)
    {
        if(progress >= 1f)
        {
            GameWon();
        }
    }

    //Called by UI Button
    public void ResetGame()
    {
        birdController.ResetState();
        pigController.ResetState();
        progressUIController.ResetProgress();
        parallaxManager.SetPosition();
    }
}
