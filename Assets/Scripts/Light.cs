using System;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.UI;
using Random = System.Random;

public class Light : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private bool _isRotate = false;
    [SerializeField] private float _speedRotate = 1;
    private Vector3 _rotateVector;
    private int _rotateDirection;
    private const float MaxAlphaValue = 1f;
    private const float MinAlphaValue = 0f;
    private CanvasGroup _canvasGroup;
    private Image _image;
    private bool _brightUp = true;
    private float _timeDisplay = 1.5f;
    private float _maxSize;
    public event Action OnSwitch; 
    
    private void Awake()
    {
      _canvasGroup = GetComponent<CanvasGroup>();
      _image = GetComponent<Image>();
       var numColor = UnityEngine.Random.Range(0, _colors.Length); 
       _image.color = _colors[ numColor ];
       _timeDisplay = (float) UnityEngine.Random.Range(30, 70) / 50f;
       _maxSize = (float) UnityEngine.Random.Range(1, 10) / 10f;
       transform.GetComponent<RectTransform>().Rotate(0,0,UnityEngine.Random.Range(0, 200));
       Invoke("Shine", UnityEngine.Random.Range(0, _colors.Length));
       transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
       _rotateVector = new Vector3(0, 0, 45);
       _rotateDirection = 1;
    }

    private void OnEnable()
    {
        OnSwitch += Shine;
    }

    private void OnDisable()
    {
        OnSwitch -= Shine;
    }

    private void Shine()
    {
      
        float endValue;         
        if (_brightUp)
        {
            endValue = MaxAlphaValue;
        }
        else
        {
            endValue = MinAlphaValue;
        }        
        
        
        DOTween.Sequence()
            .Append(_canvasGroup.DOFade(endValue, _timeDisplay))
            .Insert(0,transform.DOScale(Vector3.one * _maxSize, _timeDisplay))
            .Insert(0,transform.DORotate( _rotateVector * _rotateDirection, _timeDisplay))
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _brightUp = !_brightUp;
                _rotateDirection *= (-1);
                OnSwitch?.Invoke();
            });
    }


    
}
