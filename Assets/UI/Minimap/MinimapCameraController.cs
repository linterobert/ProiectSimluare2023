using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    private void LateUpdate()
    {
        //this.transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
    }
}
