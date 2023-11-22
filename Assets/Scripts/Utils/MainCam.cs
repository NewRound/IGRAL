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
        _camera.Follow = transform;
    }
}
