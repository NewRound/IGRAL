using UnityEngine;

public class CamaraMoving : MonoBehaviour
{
    [SerializeField] private MainCam cam;
    [SerializeField] private Transform veiwPoint1;
    [SerializeField] private float maxtime;


    private void OnTriggerEnter(Collider other)
    {
        if (cam == null)
            cam = GameManager.Instance.MainCam;

        if (other.TryGetComponent<InputController>(out InputController player))
        {
            cam.SetMainCam(veiwPoint1);
            Invoke("ReturnFollowTarget", maxtime);
        }
    }

    private void ReturnFollowTarget()
    {
        cam.SetMainCam();
    }
}
