using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraFollower : MonoBehaviour
{
    [SerializeField]
    public Transform target; 
    

    private void LateUpdate()
    {
        Vector3 offset = new Vector3(0, 50, 0);
        this.transform.position = target.position + offset;
    }
}

