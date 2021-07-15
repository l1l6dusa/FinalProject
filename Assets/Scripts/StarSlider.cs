using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class StarSlider : MonoBehaviour
{
    [SerializeField] private Star[] _stars;
    private Slider _slider;
    [SerializeField] private TMP_Text _startCounter;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _stars.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
