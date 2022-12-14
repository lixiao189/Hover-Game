using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public AudioSource fireSound;
    public float fireRate = 0.06f;

    public ParticleSystem flame;
    public GameObject mobilePart;
    public GameObject bulletPrefeb;
    public float bulletSpeed = 350f;

    private float timeSinceLastShot = 0f;

    void Fire()
    {
        fireSound.Play();
        flame.Play();

        // Rotate the mobile part around its z axis with LerpAngle
        float currentAngle = mobilePart.transform.localEulerAngles.z;
        float wantedAngle = currentAngle + 10f;
        float newAngle = Mathf.LerpAngle(currentAngle, wantedAngle, 3f);
        mobilePart.transform.localEulerAngles = new Vector3(0, 0, newAngle);

        // Instantiate a bullet
        var bullet = Instantiate(bulletPrefeb, bulletPrefeb.transform.position, bulletPrefeb.transform.rotation);
        bullet.SetActive(true);
        var bulletRigidBody = bullet.GetComponent<Rigidbody>();
        bulletRigidBody.velocity = bullet.transform.forward * bulletSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        flame.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && timeSinceLastShot >= fireRate)
        {
            timeSinceLastShot = 0;
            // Fire a bullet
            Fire();
        }

        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)))
        {
            flame.Stop();
        }
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        if (timeSinceLastShot < fireRate)
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }
}
