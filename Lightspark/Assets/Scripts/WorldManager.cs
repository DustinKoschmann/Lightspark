using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
    public Animator animRock;
    public float sunTimer;

    private GameObject mainLight;
    private Light mainLightComponent;

	// Use this for initialization
	void Start () {
        mainLight = GameObject.Find("MainLight");
        mainLightComponent = mainLight.GetComponent<Light>();
        Sunset();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void OpenRocks() {
        animRock.SetBool("OpenStones", true);
    }

    private void Sunset() {
        StartCoroutine(RotateObject(mainLight, Vector3.left * 70f, sunTimer));
    }

    IEnumerator RotateObject(GameObject gameObj, Vector3 byAngles, float inTime) {
        Quaternion fromAngle = gameObj.transform.rotation;
        Quaternion toAngle = Quaternion.Euler(gameObj.transform.eulerAngles + byAngles);
        for(float t = 0f;t < 1f;t += Time.deltaTime / inTime) {
            gameObj.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
        mainLightComponent.intensity = 0f;
        //DynamicGI.UpdateEnvironment();
    }
}
