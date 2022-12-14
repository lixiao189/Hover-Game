using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotationDamping = 10f;

    private float wantedRotationAngle, currentRotationAngle;
    private float distance, height;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        height = transform.position.y;
        gameController = GameObject.Find("Game System").GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameController.GetStatus() == GameController.GameStatus.GameOver)
            return;

        // Calculate the wanted and current rotation angle
        wantedRotationAngle = player.transform.eulerAngles.y;
        currentRotationAngle = transform.eulerAngles.y;
    }

    void FixedUpdate()
    {
        if (gameController.GetStatus() == GameController.GameStatus.GameOver)
            return;

        // Damp the rotation around the Y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Convert the angle into a quaternion
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the (x,z) camera position based on where the player is
        transform.position = player.transform.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height (y) of the camera
        Vector3 newPosition = new Vector3(transform.position.x, height, transform.position.z);
        transform.position = newPosition;

        // Look at the player
        transform.LookAt(player.transform.position + new Vector3(0, 3f, 0));
    }
}
