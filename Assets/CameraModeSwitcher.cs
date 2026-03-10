using UnityEngine;
using Unity.Cinemachine;  

public class CameraModeSwitcher : MonoBehaviour
{
    public CinemachineCamera fpsCam;
    public CinemachineCamera tpsCam;

    private CinemachineCamera activeCam;

    void Start()
    {
        SetActiveCamera(tpsCam);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (activeCam == tpsCam)
                SetActiveCamera(fpsCam);
            else
                SetActiveCamera(tpsCam);
        }
    }

    void SetActiveCamera(CinemachineCamera newCam)
    {
        if (activeCam != null)
            activeCam.Priority = 10;

        newCam.Priority = 20;
        activeCam = newCam;
    }
}
