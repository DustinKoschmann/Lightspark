using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour {
    public Animator animRock;


	// Use this for initialization
	void Start () {
        animRock.SetBool("OpenStones", true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
