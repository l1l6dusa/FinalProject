using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;

[RequireComponent(typeof(UIPanel))]
public class UIManager : MonoBehaviour
{
    //public UnityEvent GameStateChanged;
    public UnityEvent GamePaused;
    public UnityEvent GameUnpaused;
    public UnityEvent RestartButtonClicked;
    public UnityEvent ExitButtonClicked;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private InteractableItem[] _stars;
    [SerializeField] private InteractableItem[] _enemies;
    [SerializeField] private InteractableItem _winArea;
    [SerializeField] private ObserveRendererInSight _camera;
    
    private bool _gameActive;
    private UIPanel _panel;
    private int _starValue;
    private int _maxStarValue;
    private bool _isInGameMenuOpened;

    public bool GameActive
    {
        get { return _gameActive; }
        set { _gameActive = value; }
    }
    
    private void Start()
    {
        _panel = GetComponent<UIPanel>();
        _starValue = 0;
        _maxStarValue = _stars.Length;
        _isInGameMenuOpened = false;
        _panel.SetPanelActive(PanelType.StartUp);
        _panel.Slider.value = _starValue;
        _panel.SliderText.text = $"{_starValue}/{_maxStarValue}";
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _continueButton.onClick.AddListener(OnSettingsContinueButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsContinueButtonClicked);
        AddListenersToStars(IncrementSliderValue);
        AddListenersToEnemies(ActivateLoseMenu);
        _winArea.CollidedWithPlayer.AddListener(ActivateWinMenu);
        _camera.BecameInvisible.AddListener(ActivateLoseMenu);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        _continueButton.onClick.RemoveListener(OnSettingsContinueButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        _settingsButton.onClick.RemoveListener(OnSettingsContinueButtonClicked);
        RemoveListenersFromStars(IncrementSliderValue);
        RemoveListenersFromEnemies(ActivateLoseMenu);
        _winArea.CollidedWithPlayer.RemoveListener(ActivateWinMenu);
        _camera.BecameInvisible.RemoveListener(ActivateLoseMenu);
    }
    
    private void OnPlayButtonClicked()
    {
        SetGameState(_gameActive);
        _panel.SetPanelActive(PanelType.InGameUI);
    }

    private void OnExitButtonClicked()
    {
        ExitButtonClicked?.Invoke();
    }

    private void OnRestartButtonClicked()
    {
        RestartButtonClicked?.Invoke();
    }

    private void OnSettingsContinueButtonClicked()
    {
        _isInGameMenuOpened = !_isInGameMenuOpened;
        _panel.SetPanelActive(_isInGameMenuOpened ? PanelType.PauseMenu : PanelType.InGameUI);
        SetGameState(_gameActive);
    }

    public void IncrementSliderValue()
    {
        _starValue++;
        _panel.SliderText.text = $"{_starValue}/{_maxStarValue}";
        _panel.Slider.value = (float)_starValue / _maxStarValue;
    }

    private void AddListenersToStars(UnityAction action)
    {
        foreach (var star in _stars)
        {
            star.CollidedWithPlayer.AddListener(action);
        }
    }
    
    private void RemoveListenersFromStars(UnityAction action)
    {
        foreach (var star in _stars)
        {
            star.CollidedWithPlayer.RemoveListener(action);
        }
    }

    private void ActivateLoseMenu()
    {
        _panel.SetPanelActive(PanelType.LoseMenu);
        SetGameState(_gameActive);
    }
    
    private void AddListenersToEnemies(UnityAction action)
    {
        foreach (var enemy in _enemies)
        {
            enemy.CollidedWithPlayer.AddListener(action);
        }
    }
    
    private void RemoveListenersFromEnemies(UnityAction action)
    {
        foreach (var enemy in _enemies)
        {
            enemy.CollidedWithPlayer.RemoveListener(action);
        }
    }

    private void ActivateWinMenu()
    {
        _panel.SetPanelActive(PanelType.WinMenu);
        //GameStateChanged.Invoke();
        SetGameState(_gameActive);
    }

    private void SetGameState(bool state)
    {
        if (state == false)
        {
            GameUnpaused?.Invoke();
        }
        else
        {
            GamePaused?.Invoke();
        }
    }

    public void SetUIState(bool active)
    {
        _gameActive = active;
    }
}
