using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ViewerScene
{
    private CanvasGroup _canvasGroup;
    private Sound _sound;
    private float _timeDisplay = 2f;
    private float _endAlpha;

    public ViewerScene( CanvasGroup canvasGroup, Sound sound )
    {
        _canvasGroup = canvasGroup;
        _sound = sound;
    }

    public void DisplayScene(DisplayType displayType)
    {
        _canvasGroup.alpha = 1 - (int)displayType;
        _endAlpha = (int)displayType;
        DOTween.Sequence()
       .Join(_canvasGroup.DOFade(_endAlpha, _timeDisplay))
       .SetEase(Ease.InExpo)
       .OnComplete(() =>
       {
           if( _canvasGroup.alpha == 0 && SceneManager.GetActiveScene().buildIndex == 0 ) _sound.StartPlayMusic();

           if (displayType == DisplayType.Disable)
           {
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
           }
       });
    }   
}

public enum DisplayType 
{
   Enable = 0,
   Disable = 1
}