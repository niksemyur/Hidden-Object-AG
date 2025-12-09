using UnityEngine;
using Zenject;
using HiddenObject.Item;
using HiddenObject.Signals.GameState;
using HiddenObject.Signals.Input;
using HiddenObject.Signals.Item;

public class InputManager : MonoBehaviour, IInitializable
{
    [Header("Item Layer")]
    [SerializeField] private LayerMask _itemLayerMask; //слой объектов поиска на уровне

    [Inject] private readonly SignalBus _signalBus;

    private Camera _mainCamera;

    private bool gamePause;
    private bool gameOver;

    public void Initialize()
    {
        _mainCamera = Camera.main;
        if (!_mainCamera)
        {
            Debug.LogError("Камера не обнаружена");
        }

        _signalBus.Subscribe<GameOverSignal>(OnGameOver);
        _signalBus.Subscribe<LevelCompletedSignal>(OnLevelComplete);

        _signalBus.Subscribe<GamePauseSignal>(OnPause);
        _signalBus.Subscribe<GameResumeSignal>(OnResume);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _signalBus.Fire(new EscapeKeyPressedSignal());
        }
        if (Input.GetMouseButtonDown(0))
        {
            ProcessClick(Input.mousePosition);
        }
    }

    private void ProcessClick(Vector2 screenPosition)
    {
        if (gamePause || gameOver) return;

        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _itemLayerMask);

        if (hit.collider != null)
        {
            var controller = hit.transform.GetComponent<ItemController>();
            _signalBus.Fire(new ItemClickedSignal { ItemId = controller.GetId() });
        }
    }

    private void OnGameOver()
    {
        SetGameOver(true);
    }
    private void OnLevelComplete()
    {
        SetGameOver(true);
    }
    private void OnPause()
    {
        SetGamePause(true); 
    }
    private void OnResume()
    {
        SetGamePause(false);
    }

    public void SetGamePause(bool state)
    {
        gamePause = state;
    }

    public void SetGameOver(bool state)
    {
        gameOver = state;
    }
}