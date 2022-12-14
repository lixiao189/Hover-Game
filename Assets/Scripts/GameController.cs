using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public float time = 60f; // 3 minutes

    public GameObject turretPrefab;

    private int turretCount = 0;

    private int tick = 0;

    void GenerateTurrets(int left, int right, int down, int up)
    {
        turretCount = Random.Range(4, 8);

        for (int i = 0; i < turretCount; i++)
        {
            int x = Random.Range(left, right);
            int z = Random.Range(down, up);
            int y = 1;

            var turret = Instantiate(turretPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            turret.SetActive(true);
        }
    }

    public void ChangeHP(int newHP)
    {
        // Change HP text
        var TextObj = GameObject.Find("HP Text");
        TextObj.GetComponent<UnityEngine.UI.Text>().text = "HP: " + newHP;
    }

    // Add score
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        // Change score text
        var TextObj = GameObject.Find("Score Text");
        TextObj.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;
    }

    // Update time text
    public void UpdateTime()
    {
        var TextObj = GameObject.Find("Time Text");
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;

        string minuteString = minutes < 10 ? "0" + minutes : minutes.ToString();
        string secondString = seconds < 10 ? "0" + seconds : seconds.ToString();

        TextObj.GetComponent<UnityEngine.UI.Text>().text = "Time: " + minuteString + ":" + secondString;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateTurrets(-120, 120, 0, 200);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        UpdateTime();
        tick++;
        if (tick == 1 / Time.deltaTime)
        {
            time -= 1;
            tick = 0;
        }
    }
}
