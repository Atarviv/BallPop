using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BricksGenerator : MonoBehaviour
{
    public Camera MainCamera;
    public int Row, Col;
    public GameObject[,] board;
    [Header("StartField")]
    public Text Columns;
    public Text Rows;
    public GameObject StartPanel;
    [Header("Prefabs")]
    public GameObject BrickPrefab;
    public GameObject BallPrefab;
    public void Generate()
        {
        for(int i = 0;i< Row;i++)
            for(int j = 0;j<Col;j++)
                {
                board[i, j] = Instantiate(BrickPrefab, new Vector2((float)-(Col-1)/2+j,(float)-(Row-1)/2+i), transform.rotation);
                board[i, j].GetComponent<BrickScript>().Row = i;
                board[i, j].GetComponent<BrickScript>().Col = j;
                board[i, j].GetComponent<BrickScript>().OwnBall = Instantiate(BallPrefab, board[i, j].transform.position, transform.rotation);
                }
        float size = 5.5f;
        if (Row > 11 || Col > 25)
            {
            if (Row / 2 > Col / 4) size = Row / 2;
            else size = Col / 4;
            size *= 1.1f;
            }
        MainCamera.orthographicSize = size;
        }
    public void StartGame()
        {
        Row = int.Parse(Rows.text);
        Col = int.Parse(Columns.text);
        if (Row >= 3 && Row <= 15 && Col >= 3 && Col <= 15)
            {
            BrickScript.GameRunning = true;
            board = new GameObject[Row, Col];
            StartPanel.SetActive(false);
            Generate();
            }
        }
}
