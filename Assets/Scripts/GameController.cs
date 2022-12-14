using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameStatus
    {
        Playing,
        GameOver
    }

    public int score = 0;
    public float time = 60f; // 3 minutes

    public GameObject turretPrefab;

    private int turretCount = 0;

    private int tick = 0;
    private GameStatus status;

    public GameObject panel;

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
    public void KillEnemy(int newScoreValue)
    {
        score += newScoreValue;
        // Change score text
        var TextObj = GameObject.Find("Score Text");
        TextObj.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;

        turretCount--;

        if (turretCount <= 0)
        {
            GameOver();
        }
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

    public void GameOver()
    {
        status = GameStatus.GameOver;

        // Show game over text
        var TextObj = GameObject.Find("Game Over Text");
        TextObj.GetComponent<UnityEngine.UI.Text>().text = "Game Over";

        panel.SetActive(true);
    }

    public GameStatus GetStatus()
    {
        return status;
    }

    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("Panel");
        panel.SetActive(false);
        GenerateTurrets(-120, 120, 0, 200);
        status = GameStatus.Playing;
    }

    void FixedUpdate()
    {
        if (status == GameStatus.GameOver)
            return;

        UpdateTime();
        tick++;
        if (tick == 1 / Time.deltaTime)
        {
            time -= 1;
            tick = 0;
        }

        if (time <= 0)
        {
            GameOver();
        }
    }
}
