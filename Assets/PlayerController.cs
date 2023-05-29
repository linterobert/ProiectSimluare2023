using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb;
    public LayerMask layerMask;
    public bool grounded;
    public bool isInCar;
    public Renderer playerRenderer;

    [SerializeField]
    public GameObject PlayerCamera;
    [SerializeField]
    public GameObject CarCamera;

    [SerializeField]
    public GameObject PlayerMinimap;
    [SerializeField]
    public GameObject CarMinimap;

    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.playerRenderer = GetComponentInChildren<Renderer>();
        this.playerRenderer.enabled = true;

        this.isInCar = false;
        
        PlayerCamera.SetActive(true);
        CarCamera.SetActive(false);

        PlayerMinimap.SetActive(true);
        CarMinimap.SetActive(false);

        this.spawnPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.GameRunning)
        {
            PlayerCamera.SetActive(false);
            CarCamera.SetActive(false);
            return;
        }


        if (!isInCar)
        {
            Grounded();
            Jump();
            Move();
            CheckCarEnter();
        }
        else
        {
            CheckCarExit();
        }
    }
    // Update is called once per frame
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up *  3f, ForceMode.VelocityChange);
            grounded = false; 
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
    }
    
    private void Grounded()
    {
        if(Physics.CheckSphere(this.transform.position + Vector3.down, 1.5f, layerMask))
        {
            this.grounded = true;
        }
        else
        {
            this.grounded = false;
        }
    }

    private void Move()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        //  movement direction based on the player's forward and right vectors
        Vector3 playerForward = transform.forward;
        Vector3 playerRight = transform.right;
        playerForward.y = 0f; 
        playerRight.y = 0f;
        playerForward.Normalize();
        playerRight.Normalize();

        //  movement vector relative to the player's rotation
        Vector3 movement = (playerForward * verticalAxis + playerRight * horizontalAxis).normalized;
        
        //  rotation direction based on the movement vector
        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);

        // rotate the player towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 3);

        
        float moveSpeed = 4.5f;
        this.transform.position += movement * moveSpeed * Time.deltaTime;


        // animator parameters
        anim.SetFloat("vertical", verticalAxis);
        anim.SetFloat("horizontal", horizontalAxis);
    }



    private void CheckCarExit()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            this.isInCar = false;
            this.transform.parent = null;
            this.rb.isKinematic = false;
            

            GameObject car = GameObject.FindGameObjectWithTag("Car");
            car.GetComponent<CarControl>().playerControl = false;

            Vector3 carPosition = car.transform.position;

            // offset to the left
            Vector3 offset = -car.transform.right * 2.0f;
            
            transform.position = carPosition + offset;
            
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            this.playerRenderer.enabled = true;
            EnableChildren(true);

            PlayerCamera.SetActive(true);
            CarCamera.SetActive(false);

            PlayerMinimap.SetActive(true);
            CarMinimap.SetActive(false);
        }
    }

    private void CheckCarEnter()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            this.isInCar = true;
            // by tag
            GameObject car = GameObject.FindGameObjectWithTag("Car");
            car.GetComponent<CarControl>().playerControl = true;
            this.transform.parent = car.transform;
            this.rb.isKinematic = true;
            this.playerRenderer.enabled = false;

            EnableChildren(false);

            PlayerCamera.SetActive(false);
            CarCamera.SetActive(true);

            PlayerMinimap.SetActive(false);
            CarMinimap.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject car = GameObject.FindGameObjectWithTag("Car");
            car.transform.position += Vector3.up * 1.0f;
            car.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    private void EnableChildren(bool enable)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(enable);
        }
    }

}
