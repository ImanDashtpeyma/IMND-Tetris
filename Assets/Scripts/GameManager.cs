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
    public bool gameStatus;
    // Start is called before the first frame update
   public void Start()
    {
        gameStatus = true;
        grid =GetComponent<Grid>();
        tetrisUI=GetComponent<TetrisUI>();

        //StartGame();
        if (gameStatus)
        {
            SpawnTetromino();
        }

    }

    //void StartGame()
    //{
    //    SpawnTetromino();

    //}

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


        if (passedTeime >= movmentFrequency&&gameStatus==true)
        {
            passedTeime -= movmentFrequency;
            MoveTetromino(Vector3.down);
        }
        UserInput();
    }

    void SpawnTetromino()
    {
        if (gameStatus==true)
        {
            int index = Random.Range(0, Tetrominos.Length);
            currentTetromino = Instantiate(Tetrominos[index], new Vector3(5, 18, 0), Quaternion.identity);
        }
       
        
    }

    void MoveTetromino(Vector3 direction)
    {
        if (!grid.GameOver())
        {
            currentTetromino.transform.position += direction;


            if (!isValidPosition())
            {
                currentTetromino.transform.position -= direction;
                if (direction == Vector3.down)
                {
                    grid.UpdateGrid(currentTetromino.transform);
                    grid.CheckForLines();
                    SpawnTetromino();
                }
            }
        }
        

    }
    bool isValidPosition()
    {
        return grid.IsValidPosition(currentTetromino.transform);
    }
    void UserInput()
    {

        if (gameStatus==true)
     {
            
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveTetromino(Vector3.left);
        
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveTetromino(Vector3.right);
        
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
            currentTetromino.transform.Rotate(0, 0, 90);
            if (!isValidPosition())
            {
                currentTetromino.transform.Rotate(0, 0, -90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 0; i <5; i++)
            {
                MoveTetromino(Vector3.down);
            }
        }
     }

    }
   

}
