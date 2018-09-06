using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed;        // 速度

    [SerializeField]
    private float lifeTime;     // 寿命

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(0.0f, 0.0f, this.speed) * Time.deltaTime);

        // 寿命を減らす
        this.lifeTime -= Time.deltaTime;
        if (this.lifeTime <= 0.0f)
        {
            // 削除
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        // 弾が敵に当たれば消す
        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
