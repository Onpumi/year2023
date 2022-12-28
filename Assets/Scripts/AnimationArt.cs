using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationArt : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _timeFrame = 1f;
    [SerializeField] private float _fps = 10;
    [SerializeField] private Move Move;
    [SerializeField] private float _timeStartDelay = 0;
    private Sprite _sprite;
    private Image _image;
    private bool _isAnimate = true;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _sprites[0];
        if (_timeStartDelay > 0)
        {
            _isAnimate = false;
            transform.gameObject.SetActive(false);
            Invoke("EnableAnimation", _timeStartDelay);
        }
    }


    private void EnableAnimation()
    {
        transform.gameObject.SetActive(true);
        _isAnimate = true;
    }

    private void UpdateAnimation()
    {
        if (_isAnimate == true)
        {
            int index = (int)(Time.time * _fps);
            index = index % _sprites.Length;
            _image.sprite = _sprites[index];
            if (Move == Move.Left)
            {
                transform.position -= transform.right * Time.deltaTime;
            }
            else if (Move == Move.Right)
            {
                transform.position += transform.right * Time.deltaTime;
            }
        }
    }
    
    private void Update () 
    {
       UpdateAnimation();
    }

}

public enum Move
{
    None,
    Left,
    Right
}