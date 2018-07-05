using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchscript : MonoBehaviour {	
    public GameObject parentObject;
    public float shakeIntensity = 2f;

    private float maxShake = .1f;
    private float curShake = 0f;
    private bool shakeUp = false;
    private bool allowShake = false;

    private PlayerMovement playerMovement;

    Vector3 startPos;
    Quaternion lockedRotation;


    void Start() {
        //startPos = this.transform.position;
        //lockedRotation = this.transform.rotation;
        //lockedRotation = Quaternion.Euler(-90, 0, 0);
        playerMovement = parentObject.transform.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update () {
        if(playerMovement.checkMoving()) {
            allowShake = true;
        } else {
            allowShake = false;
        }

        if(allowShake) {
            if(shakeUp && curShake >= maxShake) {
                shakeUp = false;
            }
            if(!shakeUp && curShake <= -maxShake) {
                shakeUp = true;
            }
            if(shakeUp) {
                curShake += Time.deltaTime * shakeIntensity;
            } else {
                curShake -= Time.deltaTime * shakeIntensity;
            }
        } else {
            curShake = 0f;
        }
        
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + curShake, this.transform.position.z);

        //lockedRotation = Quaternion.Euler(lockedRotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
    }

    private void LateUpdate() {
        //this.transform.rotation = lockedRotation;
    }
}
