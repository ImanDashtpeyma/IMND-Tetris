using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Tetrominos;
    public float movmentFrequency = 0.8f;
    private float passedTeime = 1;
    private GameObject currentTetromino;
    private TetrisUI tetrisUI;
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid =GetComponent<Grid>();
        tetrisUI=GetComponent<TetrisUI>();
        StartGame();
      
    }

    void StartGame()
    {
        SpawnTetromino();

    }

    // Update is called once per frame
    void Update()
    {
        if (grid.score > 10) {
           
        passedTeime += Time.deltaTime*grid.score/10;
           
        }
        else
        {
            passedTeime += Time.deltaTime;
        }


        if (passedTeime >= movmentFrequency)
        {
            passedTeime -= movmentFrequency;
            MoveTetromino(Vector3.down);
        }

        UserInput();
    }

    void SpawnTetromino()
    {
        int index=Random.Range(0,Tetrominos.Length);
        currentTetromino= Instantiate(Tetrominos[index],new Vector3(5,19,0),Quaternion.identity);
        
        
    }

    void MoveTetromino(Vector3 direction)
    {
        currentTetromino.transform.position += direction;
        if (!isValidPosition()&& !grid.GameOver())
        {
            currentTetromino.transform.position -= direction;
            if(direction == Vector3.down)
            {
                grid.UpdateGrid(currentTetromino.transform);
                CheckForlines();
                SpawnTetromino();
            }
        }

    }
    bool isValidPosition()
    {
        return grid.IsValidPosition(currentTetromino.transform);
    }
    void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            MoveTetromino(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)){
            MoveTetromino(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)){
            currentTetromino.transform.Rotate(0, 0, 90);
            if (!isValidPosition())
            {
                currentTetromino.transform.Rotate(0, 0, -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i <5; i++)
            {
                MoveTetromino(Vector3.down);
            }
        }

    }
    void CheckForlines()
    {
      grid.CheckForLines();
    }

}
