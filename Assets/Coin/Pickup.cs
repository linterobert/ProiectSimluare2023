using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float _rotationSpeed = 100.0f;
    private float _multiplier = 1.75f;
    public float _duration = 5.0f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, -_rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter (Collider other)
    {

        if (other.CompareTag("CarBody"))
        {
            StartCoroutine(Collect(other));
        }
    }

    IEnumerator Collect(Collider car) 
    {
        Debug.Log("Pickup collected.");

        CarControl carStats = car.transform.parent.GetComponent<CarControl>();
        carStats.motorForce *= _multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(_duration);

        carStats.motorForce /= _multiplier;

        Destroy(gameObject);
    }
}
