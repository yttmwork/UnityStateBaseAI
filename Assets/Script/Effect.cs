using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("DestroyWait");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DestroyWait()
    {
        yield return new WaitForSeconds(5.0f);

        Destroy(gameObject);
    }
}
