using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Self : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(aspetta());
	}

    private IEnumerator aspetta()
    {
        yield return new WaitForSeconds(2);
        DestroyObject(gameObject);
    }
}
