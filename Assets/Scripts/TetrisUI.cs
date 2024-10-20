using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TetrisUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    private Button retry;
    private Button start;
    public Image imgGameOver;
    private GameManager gameManager;
    public AudioSource audioDrop;
    public AudioSource audioMenuClick;
    public AudioSource audioRotate;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        retry =GameObject.Find("Btn Retry").GetComponent<Button>();
        start = GameObject.Find("Btn Start").GetComponent<Button>();
        audioDrop = GameObject.Find("Drop").GetComponent<AudioSource>();
        audioMenuClick= GameObject.Find("MenuClick").GetComponent<AudioSource>();
        audioRotate= GameObject.Find("Rotate").GetComponent<AudioSource>();
        retry.onClick.AddListener(RestartGame);
        retry.gameObject.SetActive(false);
        start.onClick.AddListener(StartGame);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log(" Restart Was Clicked");


    }

    void StartGame()
    {

        Debug.Log("Start Was Clicked");
        start.gameObject.SetActive(false);
        retry.gameObject.SetActive(true);
        gameManager.gameStatus = true;
        gameManager.StartGame();

    }

}
