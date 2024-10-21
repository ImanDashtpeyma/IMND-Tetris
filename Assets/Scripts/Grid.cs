using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform[,] grid;
    public int width, height;
    public int speedLevel;
    public int score;
    private TetrisUI tetrisUI;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        speedLevel = 1;
        score = 0;
        grid = new Transform[width, height];
        tetrisUI = GetComponent<TetrisUI>();
        gameManager = GetComponent<GameManager>();
        tetrisUI.levelText.text = " Speed : " + speedLevel.ToString();
        tetrisUI.scoreText.text = "Score : " + score.ToString();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGrid(Transform tetromino)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetromino)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetromino)
        {
            Vector2 pos = Round(mino.position);
            if (pos.y < height)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public static Vector2 Round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }


    public bool IsInsideBorder(Vector2 pos)
    {
        return (int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0 && (int)pos.y < height;
    }

    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if (pos.y > height - 1)
        {
            return null;
        }
        return grid[(int)pos.x, (int)pos.y];
    }

    public bool IsValidPosition(Transform tetromino)
    {
        foreach (Transform mino in tetromino)
        {
            Vector2 pos = Round(mino.position);
            if (!IsInsideBorder(pos))
            {
                return false;
            }

            if (GetTransformAtGridPosition(pos) != null && GetTransformAtGridPosition(pos).parent != tetromino)
            {
                return false;
            }
        }
        return true;
    }

    //Remove Fullline Mino
    public void CheckForLines()
    {
        for (int y = 0; y < height; y++)
        {
            if (LineIsFull(y))
            {
                DeleteLine(y);
                DecreaseRowsAbove(y + 1);
                y--;
            }
        }
    }
    //Check if we have Fullline tetromino
    bool LineIsFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }
    //Distroy Mino in fullline and set it to NULL and set the score and level
    void DeleteLine(int y)
    {

        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
        score = score + 10;
        speedLevel += 1;
        tetrisUI.scoreText.text = "Score : " + score.ToString();
        tetrisUI.levelText.text = " Speed : " + speedLevel.ToString();
        tetrisUI.audioDrop.Play();
        Debug.Log(score);
    }

    //Clear Board when Game is over
    void ClearGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                    grid[x, y] = null;
                }
            }

        }

    }
    //GameOver condition
    public bool GameOver()
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, 19] != null)
            {
                ClearGrid();
                tetrisUI.imgGameOver.gameObject.SetActive(true);

                gameManager.gameStatus = false;

                Debug.Log("Game Over Try Again");
                return true;
            }
        }

        return false;

    }

    //Move upper line of removed line to removed line
    void DecreaseRowsAbove(int startRow)
    {
        for (int y = startRow; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }
}

