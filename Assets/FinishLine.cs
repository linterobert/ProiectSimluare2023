using System.Collections;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarBody"))
        {
            GameController.Instance.EndGame();
        }
    }
}
