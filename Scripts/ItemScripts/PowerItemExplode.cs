using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemExplode : MonoBehaviour {
    public GameObject pickupEffect;



    public void Pickup()
    {
        GameObject newpickUpEffect = (GameObject)Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(newpickUpEffect, 1);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
