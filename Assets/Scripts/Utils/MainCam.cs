using UnityEngine;
using Cinemachine;

public class MainCam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    public void SetMainCam()
    {
        _camera.Follow = GameManager.Instance.PlayerTransform;
    }

    public void SetMainCam(Transform transform)
    {
        Debug.Log("½ÇÇà !!!!!!!!!!!!!!!!!!!!");
        _camera.Follow = transform;
    }
}
