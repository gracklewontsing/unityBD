using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDestruction : MonoBehaviour {
    public float lifeSpan = 2.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Destroy(this.gameObject);
        }
    }   
}
