using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class StarSlider : MonoBehaviour
{
    [SerializeField] private Star[] _stars;
    [SerializeField] private TMP_Text _startCounter;
    
    private Slider _slider;
    
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _stars.Length;
    }
}
