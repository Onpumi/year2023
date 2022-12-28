using UnityEngine;
using UnityEngine.SceneManagement;

public class PageScene : MonoBehaviour
{
    [SerializeField] private Transform _frontGround;
    [SerializeField] private Sound _sound;
    private ViewerScene _viewerScene;
    private CanvasGroup _canvasGroup;
    private bool _isPlayScene = true;
   private void Awake()
   {
       _canvasGroup = _frontGround.GetComponent<CanvasGroup>();
       _viewerScene = new ViewerScene( _canvasGroup, _sound );
       _viewerScene.DisplayScene( DisplayType.Enable );
   }

   private void Update()
   {

       if ( (_sound is null) == false && _sound.IsFinish == true)
       {
         _viewerScene.DisplayScene( DisplayType.Disable );
       }
   }
}
