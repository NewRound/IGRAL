using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = GameManager.Instance.Camera;
        _camera.clearFlags = CameraClearFlags.SolidColor;
        _camera.backgroundColor = Color.gray;
    }
}
