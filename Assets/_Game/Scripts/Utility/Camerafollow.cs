using Cinemachine;
using DG.Tweening;
using System.Collections.Generic;

public class Camerafollow : Singleton<Camerafollow>
{
    public CinemachineVirtualCamera MainMenuCamera;
    public CinemachineVirtualCamera ShopSkinCamera;
    public CinemachineVirtualCamera GamePlayCamera;
    private List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    private CinemachineVirtualCamera ActiveCamera = null;

    private void Start()
    {
        cameras.Add(MainMenuCamera);
        cameras.Add(ShopSkinCamera);
        cameras.Add(GamePlayCamera);
    }

    public void SwitchCamera(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                LevelManager.Instance.player.ChangeAnim(Constant.IDLE_ANIM_STRING);
                ActiveCamera = MainMenuCamera;
                break;
            case GameState.ShopSkin:
                LevelManager.Instance.player.ChangeAnim(Constant.DANCE_ANIM_STRING);
                ActiveCamera = ShopSkinCamera;
                break;
            case GameState.GamePlay:
                ActiveCamera = GamePlayCamera;
                break;
            case GameState.Win:
                LevelManager.Instance.player.ChangeAnim(Constant.WIN_ANIM_STRING);
                ActiveCamera = MainMenuCamera;
                break;
            default:
                return;
        }
        ActiveCamera.Priority = 10; 
        foreach (CinemachineVirtualCamera camera in cameras)
        {
            if (camera != ActiveCamera)
            {
                camera.Priority = 0;
            }
        }
    }

    internal void ScaleCamera(float currentScale)
    {
        //float target = Constant.DEFAULT_SIZE_CAMERA * currentScale;
        //DOTween.To(() => GamePlayCamera.m_Lens.OrthographicSize,
        //          x => GamePlayCamera.m_Lens.OrthographicSize = x,
        //          target,
        //          1f);
    }
}
