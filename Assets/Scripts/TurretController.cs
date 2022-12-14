using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public int HP = 5;
    public int score = 100;
    public ParticleSystem flame;
    public AudioSource explosionMusic;
    public AudioSource fireMusic;

    private bool isAlive = true;
    private GameController gameController;

    public float timeSinceLastFire = 0f;
    public float fireRate = 0.2f;

    public GameObject[] bulletsPrefebs;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            HP--;
            if (isAlive)
            {
                explosionMusic.Play();
                if (HP <= 0)
                {
                    isAlive = false;

                    // Turn off mesh render
                    GetComponent<MeshRenderer>().enabled = false;
                    flame.Play();

                    // Add score
                    gameController.AddScore(score);

                    Destroy(gameObject, 0.5f);
                }
            }
        }
    }

    void EnemyFire()
    {
        fireMusic.Play();

        // Instantiate a bullet
        var bulletPrefeb = bulletsPrefebs[Random.Range(0, bulletsPrefebs.Length)];
        var bullet = Instantiate(bulletPrefeb, bulletPrefeb.transform.position, bulletPrefeb.transform.rotation);

        bullet.SetActive(true);

        // Bullet look at the player
        var player = GameObject.FindGameObjectWithTag("Player");
        bullet.transform.LookAt(player.transform);

        // Rotate around x axis with 90
        bullet.transform.Rotate(new Vector3(180, 0, 0), Space.Self);

        var bulletRigidBody = bullet.GetComponent<Rigidbody>();
        bulletRigidBody.velocity = -bullet.transform.forward * 100f;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("Game System").GetComponent<GameController>();
        flame.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Rotate turret to look at the player
        var player = GameObject.FindGameObjectWithTag("Player");
        // Smoothly rotate rocket launcher towards the target point.
        var rocketLaunchers = GameObject.FindGameObjectsWithTag("RocketLauncher");

        // Rotate all the rocket launcher
        foreach (var rocketLauncher in rocketLaunchers)
        {
            // Rocket launcher look at player
            rocketLauncher.transform.LookAt(player.transform);
            rocketLauncher.transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        }

        // Fire bullets
        timeSinceLastFire += Time.deltaTime;

        if (timeSinceLastFire >= fireRate)
        {
            timeSinceLastFire = 0;
            // Fire a bullet
            EnemyFire();
        }
    }
}
