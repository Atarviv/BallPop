using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    float HighScore;
    public float points;
    float timeLeft;
    public GameObject[,] Board;
    [Header("UI")]
    public Text PointsText;
    public Text TimeLeftText;
    public Text LastScoreText;
    public Text HighScoreText;
    public GameObject EndOfGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        timeLeft = 30;
        Board = GetComponent<BricksGenerator>().board;
        HighScore = PlayerPrefs.GetFloat("HighScore");
    }
    void Update()
    {
        if (BrickScript.GameRunning)
            {
            timeLeft -= Time.deltaTime;
            PointsText.text = points + " points";
            TimeLeftText.text = timeLeft.ToString("F0") + " sec left";
            }
        if (timeLeft <= 0)
            {
            BrickScript.GameRunning = false;
            EndOfGamePanel.SetActive(true);
            LastScoreText.text = "You Scored " + points + " Points!";
            if (points > HighScore)
                HighScore = points;    
            PlayerPrefs.SetFloat("HighScore",HighScore);
            HighScoreText.text = "Your High Score is:" +HighScore.ToString();
            }
    }
    public int KillBalls(Color color,int Row,int Col)
        {
        int Kills = 0;
        Board = GetComponent<BricksGenerator>().board;
        for (int i = -1; i <= 1; i += 2)
            {
            if (Row + i >= 0 && Row + i < Board.GetLength(0))
                if(Board[Row+i,Col].GetComponent<BrickScript>().OwnBall?.GetComponent<BallScript>().color == color)
                    Kills += DestoryBall(color, Row+i, Col);
            if (Col + i >= 0 && Col + i < Board.GetLength(1))
                    if (Board[Row, Col+i].GetComponent<BrickScript>().OwnBall?.GetComponent<BallScript>().color == color)
                    Kills += DestoryBall(color, Row, Col + i);
            }
        return Kills;
        }
    public int DestoryBall(Color color,int Row, int Col)
        {
        Destroy(Board[Row, Col].GetComponent<BrickScript>().OwnBall);
        Board[Row, Col].GetComponent<BrickScript>().particle.Play();
        Board[Row, Col].GetComponent<BrickScript>().OwnBall = null;
        return 1 + KillBalls(color, Row, Col);
        }
    public void PlayAgain()=>
        SceneManager.LoadScene("SampleScene");
}
