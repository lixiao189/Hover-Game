using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ParticleSystem flame;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "RocketLauncher" || other.gameObject.tag == "Player")
        {
            flame.Play();
            Destroy(gameObject, 0.1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        flame.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
