using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    public Slider Slider => _slider;
    public TMP_Text SliderText => _sliderText;

    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _continueButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _winTag;
    [SerializeField] private GameObject _loseTag;
    [SerializeField] private GameObject _settingsButton;
    [SerializeField] private TMP_Text _sliderText;
    [SerializeField]private CanvasGroup _canvasGroup;
    [SerializeField] private float _alphaPerFrame;
    private Coroutine _fadeInCoroutine;
    
    private WaitForEndOfFrame _waitForEndOfFrame;
    
    private void Start()
    {
        _canvasGroup.GetComponent<CanvasGroup>();
        _waitForEndOfFrame = new WaitForEndOfFrame();
    }
    
    public void SetPanelActive(PanelType type)
    {
        if(_fadeInCoroutine != null)
        {
            StopCoroutine(_fadeInCoroutine);
        }
        _canvasGroup.alpha = 0;
        switch (type)
        {
            case PanelType.StartUp:
                _panel.SetActive(true);
                _playButton.SetActive(true);
                _exitButton.SetActive(true);
                _continueButton.SetActive(false);
                _restartButton.SetActive(false);
                _slider.gameObject.SetActive(false);
                _winTag.SetActive(false);
                _loseTag.SetActive(false);
                _settingsButton.SetActive(false);
                break;
            case PanelType.PauseMenu:
                _panel.SetActive(true);
                _playButton.SetActive(false);
                _exitButton.SetActive(true);
                _continueButton.SetActive(true);
                _restartButton.SetActive(true);
                _settingsButton.SetActive(false);
                _slider.gameObject.SetActive(false);
                _winTag.SetActive(false);
                _loseTag.SetActive(false);
                break;
            case PanelType.WinMenu:
                _panel.SetActive(true);
                _playButton.SetActive(false);
                _exitButton.SetActive(true);
                _continueButton.SetActive(false);
                _restartButton.SetActive(true);
                _settingsButton.SetActive(false);
                _winTag.SetActive(true);
                _loseTag.SetActive(false);
                _slider.gameObject.SetActive(true);
                break;
            case PanelType.LoseMenu:
                _panel.SetActive(true);
                _playButton.SetActive(false);
                _exitButton.SetActive(true);
                _continueButton.SetActive(false);
                _restartButton.SetActive(true);
                _loseTag.SetActive(true);
                _winTag.SetActive(false);
                _slider.gameObject.SetActive(true);
                _settingsButton.SetActive(false);
                break;
            case PanelType.InGameUI:
                _panel.SetActive(false);
                _playButton.SetActive(false);
                _exitButton.SetActive(false);
                _continueButton.SetActive(false);
                _restartButton.SetActive(false);
                _loseTag.SetActive(false);
                _winTag.SetActive(false);
                _slider.gameObject.SetActive(true);
                _settingsButton.SetActive(true);
                break;
        }
        _fadeInCoroutine = StartCoroutine(FadeInEnumerator());
    }

    private IEnumerator FadeInEnumerator()
    {
        while (_canvasGroup.alpha < 1f)
        {
            _canvasGroup.alpha += _alphaPerFrame;
            yield return _waitForEndOfFrame;
        }
    }
}
