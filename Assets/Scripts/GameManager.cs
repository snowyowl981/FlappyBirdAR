using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject tapToStartText;
    public GameObject gameOverPanel;

    public int score = 0;
    public static int touchCount = 0;

    public PlayerMove playerMove;

    public static bool isStart = false;
    public static GameManager Instance;
    public bool isPlaced = false;

    void Awake()
    {
        playerMove.gameManager = this;
        GameManager.Instance = this;
    }


    void Start()
    {
        scoreText.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isPlaced)
        {
            touchCount++;
            if(touchCount == 1)
                isStart = true;
                StartGame();
        }
    }

    public void OnTargetFound()
    {
        isPlaced = true;
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        Debug.Log(score);
    }

    public void StartGame()
    {
        scoreText.enabled = true;
        tapToStartText.SetActive(false);
        //om.enabled = true;
    }

    public void GameOver()
    {
        isStart = false;
        gameOverPanel.SetActive(true);
        playerMove.birdRig.detectCollisions = false;
        playerMove.isDie = true;
        playerMove.anim.enabled = false;
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(0);
        playerMove.birdRig.detectCollisions = true;
        playerMove.isDie = false;
        playerMove.anim.enabled = true;
        touchCount = 0;
    }
}
