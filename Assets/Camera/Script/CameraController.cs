using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    CinemachineVirtualCamera mainCamera;

    private static CameraController Instanse;

    //public static GameObject target {  get;  set; }
   
    public static CameraController instanse { get { return Instanse; } }

    private void Awake()
    {
        Instanse = this;
        mainCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        //FollowCamera();
    }

    public void FollowCamera(GameObject target)
    {
        if(target != null)
        {
            mainCamera.Follow = target.transform;
            mainCamera.m_Lens.OrthographicSize = 15;

        }
    }

    public void StartCamera()
    {
        mainCamera.transform.position = new Vector3(0, 0, 0);
        mainCamera.m_Lens.OrthographicSize = 30;

    }

}
