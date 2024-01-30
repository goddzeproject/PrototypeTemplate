using Cinemachine;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        public void FollowToObject(GameObject Vcamera, GameObject following)
        {
            Vcamera.GetComponent<CinemachineVirtualCamera>().Follow = following.transform;
            Vcamera.GetComponent<CinemachineVirtualCamera>().LookAt = following.transform;
        }
    }
}