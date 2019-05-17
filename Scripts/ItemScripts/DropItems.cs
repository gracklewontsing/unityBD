using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour {
    public GameObject[] items;
    int randomInt;

    public void Drop()
    {
        randomInt = Random.Range(0, items.Length);
        Instantiate(items[randomInt], transform.position, Quaternion.identity);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
