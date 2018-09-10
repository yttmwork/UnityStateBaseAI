using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    GameObject bulletPrefab;    // 弾プレハブ

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Zで発射
		if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject obj = Instantiate(this.bulletPrefab, transform.position, Quaternion.identity);
            // プレイヤーの向きに回転
            obj.transform.Rotate(transform.eulerAngles);
        }
	}
}
