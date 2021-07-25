using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(UIPanel))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private InteractableItem[] _stars;
    [SerializeField] private InteractableItem[] _enemies;
    [SerializeField] private InteractableItem _winArea;
    [SerializeField] private ObserveRendererInSight _camera;
    
    public UnityEvent GameStateChanged;
    public UnityEvent RestartButtonClicked;
    public UnityEvent ExitButtonClicked;

    private UIPanel _panel;
    private int _starValue;
    private int _maxStarValue;
    private bool _isInGameMenuOpened;

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
        _winArea.CollisionWithPlayer.AddListener(ActivateWinMenu);
        _camera.onBecameInvisible.AddListener(ActivateLoseMenu);
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
        _winArea.CollisionWithPlayer.RemoveListener(ActivateWinMenu);
        _camera.onBecameInvisible.RemoveListener(ActivateLoseMenu);
    }

    /*private void SetPanelActive(PanelType type)
    {
        
        switch (type)
        {
            case PanelType.StartUp:
                _panel.SetActive(true);
                _playButton.gameObject.SetActive(true);
                _exitButton.gameObject.SetActive(true);
                _continueButton.gameObject.SetActive(false);
                _restartButton.gameObject.SetActive(false);
                .gameObject.SetActive(false);
                _winTag.SetActive(false);
                _loseTag.SetActive(false);
                _settingsButton.gameObject.SetActive(false);
                break;
            case PanelType.PauseMenu:
                _panel.SetActive(true);
                _playButton.gameObject.SetActive(false);
                _exitButton.gameObject.SetActive(true);
                _continueButton.gameObject.SetActive(true);
                _restartButton.gameObject.SetActive(true);
                _settingsButton.gameObject.SetActive(false);
                .gameObject.SetActive(false);
                _winTag.SetActive(false);
                _loseTag.SetActive(false);
                break;
            case PanelType.WinMenu:
                _panel.SetActive(true);
                _playButton.gameObject.SetActive(false);
                _exitButton.gameObject.SetActive(true);
                _continueButton.gameObject.SetActive(false);
                _restartButton.gameObject.SetActive(true);
                _settingsButton.gameObject.SetActive(false);
                _winTag.SetActive(true);
                _loseTag.SetActive(false);
                .gameObject.SetActive(true);
                break;
            case PanelType.LoseMenu:
                _panel.SetActive(true);
                _playButton.gameObject.SetActive(false);
                _exitButton.gameObject.SetActive(true);
                _continueButton.gameObject.SetActive(false);
                _restartButton.gameObject.SetActive(true);
                _loseTag.SetActive(true);
                _winTag.SetActive(false);
                .gameObject.SetActive(true);
                _settingsButton.gameObject.SetActive(false);
                break;
            case PanelType.InGameUI:
                _panel.SetActive(false);
                _playButton.gameObject.SetActive(false);
                _exitButton.gameObject.SetActive(false);
                _continueButton.gameObject.SetActive(false);
                _restartButton.gameObject.SetActive(false);
                _loseTag.SetActive(false);
                _winTag.SetActive(false);
                .gameObject.SetActive(true);
                _settingsButton.gameObject.SetActive(true);
                break;
        }
    }*/
    
    private void OnPlayButtonClicked()
    {
        GameStateChanged?.Invoke();
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
        GameStateChanged?.Invoke();
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
            star.CollisionWithPlayer.AddListener(action);
        }
    }
    
    private void RemoveListenersFromStars(UnityAction action)
    {
        foreach (var star in _stars)
        {
            star.CollisionWithPlayer.RemoveListener(action);
        }
    }

    private void ActivateLoseMenu()
    {
        _panel.SetPanelActive(PanelType.LoseMenu);
        GameStateChanged?.Invoke();
    }
    
    private void AddListenersToEnemies(UnityAction action)
    {
        foreach (var enemy in _enemies)
        {
            enemy.CollisionWithPlayer.AddListener(action);
        }
    }
    
    private void RemoveListenersFromEnemies(UnityAction action)
    {
        foreach (var enemy in _enemies)
        {
            enemy.CollisionWithPlayer.RemoveListener(action);
        }
    }

    private void ActivateWinMenu()
    {
        _panel.SetPanelActive(PanelType.WinMenu);
        GameStateChanged.Invoke();
    }
}
