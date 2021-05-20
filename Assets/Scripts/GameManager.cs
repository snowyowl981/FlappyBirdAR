using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;

    public int score = 0;

    public PlayerMove playerMove;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerMove.gameManager = this;
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        Debug.Log(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
