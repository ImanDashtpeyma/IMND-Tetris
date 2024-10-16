using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Tetrominos;
    public float movmentFrequency = 0.8f;
    private float passedTeime = 0;
    private GameObject currentTetromino;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTetromino();
    }

    // Update is called once per frame
    void Update()
    {
        passedTeime += Time.deltaTime;
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
        currentTetromino= Instantiate(Tetrominos[index],new Vector3(5,9,0),Quaternion.identity);
    }

    void MoveTetromino(Vector3 direction)
    {
        currentTetromino.transform.position += direction;
        if (!isValidPosition())
        {

            currentTetromino.transform.position -= direction;
            if(direction == Vector3.down)
            {
                CheckForlines();
                SpawnTetromino();
            }
        }

    }
    bool isValidPosition()
    {
        return true;
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
            if (isValidPosition())
            {
                currentTetromino.transform.Rotate(0, 0, -90);
            }
        }

    }
    void CheckForlines()
    {

    }

}
