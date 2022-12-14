using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float turnSpeed, speed;
    public GameObject flame;

    private float moveHorizontal, moveVertical;
    private Rigidbody carRigidbody;

    private int HP = 100;
    private GameController gameController;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            if (HP > 0)
            {
                HP -= 10;
                gameController.ChangeHP(HP);
            }
        }
    }

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        gameController = GameObject.Find("Game System").GetComponent<GameController>();
    }


    void Update()
    {
        // Read movement keys
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (moveVertical == 0)
        {
            flame.SetActive(false);
        }
        else
        {
            flame.SetActive(true);
        }

        // Lock transform's x and z axis rotation
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // Turning
        transform.Rotate(new Vector3(0.0f, moveHorizontal * turnSpeed, 0.0f));

        // Forward or backwards
        Vector3 fwd = transform.forward;
        carRigidbody.velocity = fwd * speed * moveVertical;
    }
}
