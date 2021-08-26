using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{ 
    public UnityEvent SceneReloaded;
    
    [SerializeField] private UIManager _ui;
    
    private bool _gameActive;
    
    private void Start()
    {
        PauseGame();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneReload;
        _ui.ExitButtonClicked.AddListener(ExitGame);
        _ui.RestartButtonClicked.AddListener(ResetGame);
        _ui.GamePaused.AddListener(PauseGame);
        _ui.GameUnpaused.AddListener(UnpauseGame);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneReload;
        _ui.ExitButtonClicked.RemoveListener(ExitGame);
        _ui.RestartButtonClicked.RemoveListener(ResetGame);
        _ui.GamePaused.RemoveListener(PauseGame);
        _ui.GameUnpaused.RemoveListener(UnpauseGame);
    }

    private void PauseGame()
    {
        _ui.SetUIState(false);
        Time.timeScale = 0;
    }

    private void UnpauseGame()
    {
        _ui.SetUIState(true);
        Time.timeScale = 1;
    }
    
    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    
    private void OnSceneReload(Scene scene, LoadSceneMode mode)
    {
        SceneReloaded?.Invoke();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
    
    
}
