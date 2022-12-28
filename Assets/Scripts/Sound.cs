using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    private float _maxLengthSound;
    public bool IsFinish { get; private set; } 

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        IsFinish = false;
        _maxLengthSound = MaxLengthSound();
        DontDestroyOnLoad(this);
    }

    private float MaxLengthSound()
    {
        var maxLengthSound = _audioClips[0].length; 

        for (int i = 1; i < _audioClips.Length; i++)
        {
            maxLengthSound = (maxLengthSound < _audioClips[i].length)
                ? (_audioClips[i].length)
                : (maxLengthSound);
        }

        return maxLengthSound;
    }

    private IEnumerator PlayBackGroundMusic()
    {
        for( int i = 0 ; i < _audioClips.Length; i++ )
        {
            _audioSource.PlayOneShot(_audioClips[i]);
        }
        yield return new WaitForSeconds(_maxLengthSound);
        IsFinish = true;
    }

    public void StartPlayMusic()
    {
        StartCoroutine(PlayBackGroundMusic());
    }
    
  
}
