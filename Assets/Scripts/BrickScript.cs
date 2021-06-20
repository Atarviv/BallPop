using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject BallPrefab;
    public int Row, Col;
    public GameObject OwnBall;
    public GameManager Manager;
    public static bool GameRunning;
    void Awake()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (OwnBall == null)
            Pull();
        else
            particle.startColor = OwnBall.GetComponent<BallScript>().color;
        }
    public void Pull()
        {
        if (Row < Manager.Board.GetLength(0)-1)
            {
            GameObject[,] board = Manager.Board;
            if (board[Row+1, Col].GetComponent<BrickScript>().OwnBall != null)
                {
                OwnBall = board[Row+1, Col].GetComponent<BrickScript>().OwnBall;
                board[Row+1, Col].GetComponent<BrickScript>().OwnBall = null;
                OwnBall.GetComponent<BallScript>().StartCoroutine("NewLocation", transform.position.y);
                }
            }
        else
            {
            OwnBall = Instantiate(BallPrefab, transform.position+transform.up, transform.rotation);
            OwnBall.GetComponent<BallScript>().StartCoroutine("NewLocation", transform.position.y);
            }
        
        }
    private void OnMouseDown()
        {
        if (OwnBall != null&&GameRunning)
           {
            particle.Play();
            Destroy(OwnBall);
            Color color = OwnBall.GetComponent<BallScript>().color;
            OwnBall = null;
            Manager.points+=Mathf.Pow(Manager.KillBalls(color, Row, Col)+1,2);
            }
        }
    }
