using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    
    [SerializeField] private UIManager _ui;
    public UnityEvent SceneReload;
    private bool _gameActive;
    
    void Start()
    {
        _gameActive = false;
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneReload;
        _ui.ExitButtonClicked.AddListener(ExitGame);
        _ui.RestartButtonClicked.AddListener(ResetGame);
        _ui.GameStateChanged.AddListener(ChangeGameState);
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneReload;
        _ui.ExitButtonClicked.RemoveListener(ExitGame);
        _ui.RestartButtonClicked.RemoveListener(ResetGame);
        _ui.GameStateChanged.RemoveListener(ChangeGameState);
    }

    private void ChangeGameState()
    {
        _gameActive = !_gameActive;
        Time.timeScale = _gameActive ? 1 : 0;
    }
    
    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    
    private void OnSceneReload(Scene scene, LoadSceneMode mode)
    {
        SceneReload?.Invoke();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
    
    
}
