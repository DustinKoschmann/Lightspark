using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed = 100.0f;
    public float mouseSensitivity = 2f;
    public float maxVel = 20f;


    private CharacterController controller;
    private bool isMoving = false;
    private float pickupRadius = 100f;
    private GameObject torch;
    private WorldManager worldManager;

    float xVal = 0f;
    float yVal = 0f;

    // Use this for initialization
    void Start () {
        worldManager = GameObject.Find("WorldManager").GetComponent<WorldManager>();
        controller = this.transform.GetComponent<CharacterController>();
        torch = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update () {
        wasdMovement();
        mouseMovement();
        checkPickup();
	}

    void checkPickup() {
        Debug.DrawLine(transform.position, transform.position + transform.forward * pickupRadius, Color.red);
        if(Input.GetKeyDown(KeyCode.E)) {
            Pickup pickup;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, pickupRadius)) {
                if(hit.collider.GetComponent<Pickup>() != null) {
                    pickup = hit.collider.GetComponent<Pickup>();
                    pickup.RemoveObj();
                    torch.SetActive(true);
                    worldManager.OpenRocks();
                }
            }
        }
    }

    void wasdMovement() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(horizontalInput, 0f, verticalInput);

        Vector3 forward = this.transform.TransformDirection(move);


        if(verticalInput != 0 || horizontalInput != 0) {
            controller.SimpleMove(forward * movementSpeed);
            isMoving = true;
        } else {
            isMoving = false;
        }

        if(!controller.isGrounded) {
            controller.Move(new Vector3(0f, -1f, 0f));
        }
    }

    void mouseMovement() {
        if(xVal >= 360f) {
            xVal = 0f;
        }
        if(yVal >= 360f) {
            yVal = 0f;
        }


        xVal += mouseSensitivity * Input.GetAxis("Mouse X");
        yVal -= mouseSensitivity * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(yVal, xVal, 0.0f);

        if(Input.GetButtonDown("Fire1")) {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public bool checkMoving() {
        return isMoving;
    }
}
