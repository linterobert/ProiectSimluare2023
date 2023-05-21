using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{

    public Transform player;
    public Transform car;
    public Transform reciever;

    public bool playerIsOverlapping = false;
    public bool carIsOverlapping = false;

    public float teleportCooldown = 1f;
    private float teleportTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (teleportTimer > 0f)
        {
            teleportTimer -= Time.deltaTime;
        }

        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f)
            {
                // Teleport him!
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);

                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = reciever.position + positionOffset;

                playerIsOverlapping = false;
                teleportTimer = teleportCooldown;
            }
        }

        if (carIsOverlapping)
        {
            Vector3 portalToPlayer = car.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            
            if (dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);

                rotationDiff += 180;
                car.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                car.position = reciever.position + positionOffset;

                carIsOverlapping = false;
                teleportTimer = teleportCooldown;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
        if (other.tag == "CarBody")
        {
            carIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
        if (other.tag == "CarBody")
        {
            carIsOverlapping = false;
        }
    }
}
