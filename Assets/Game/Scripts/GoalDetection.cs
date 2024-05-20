using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalDetectoin : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI gameOverText;
    public Button newGameButtton;
    public Button quitButton;
    ThirdPersonController playerController1;
    ThirdPersonController playerController2;
    public GameObject player1;
    public GameObject player2;
    public Ball ball;
    Vector3 PlayerPosition1;
    Vector3 PlayerPosition2;
    Vector3 PlayerRotation1;
    Vector3 PlayerRotation2;
    Player playerScript1;
    Player playerScript2;
    public GameObject goal1;
    public GameObject goal2;
    Vector3 level2 = new Vector3();
    Vector3 level3 = new Vector3();
    Vector3 level4 = new Vector3();
    Vector3 level5 = new Vector3();
    Vector3 level6 = new Vector3();
    Vector3 detectorlevel2 = new Vector3();
    Vector3 detectorlevel3 = new Vector3();
    Vector3 detectorlevel4 = new Vector3();
    Vector3 detectorlevel5 = new Vector3();
    Vector3 detectorlevel6 = new Vector3();
    public AudioSource goalSound;

    void Start()
    {
        PlayerPosition1 = player1.transform.position;
        PlayerPosition2 = player2.transform.position;
        PlayerRotation1 = player1.transform.eulerAngles;
        PlayerRotation2 = player2.transform.eulerAngles;
        playerScript1 = player1.GetComponent<Player>();
        playerScript2 = player2.GetComponent<Player>();
        playerController1 = player1.GetComponent<ThirdPersonController>();
        playerController2 = player2.GetComponent<ThirdPersonController>();
        goalText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        newGameButtton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        level2 = new Vector3(1.1f, 1.1f, 1.1f);
        level3 = new Vector3(0.9f, 0.9f, 0.9f);
        level4 = new Vector3(0.8f, 0.8f, 0.8f);
        level5 = new Vector3(0.7f, 0.7f, 0.7f);
        level6 = new Vector3(0.5f, 0.5f, 0.5f);

        detectorlevel2 = new Vector3(0.1f, 4.3f, 2.8f);
        detectorlevel3 = new Vector3(0.1f, 3.6f, 2.3f);
        detectorlevel4 = new Vector3(0.1f, 3.2f, 2.1f);
        detectorlevel5 = new Vector3(0.1f, 2.8f, 1.8f);
        detectorlevel6 = new Vector3(0.1f, 2.1f, 1.3f);

        newGameButtton.onClick.AddListener(NewGameClicked);
        quitButton.onClick.AddListener(QuitClicked);

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Ball"))
        {

            goalSound.Play();
            if(name.Equals("GoalDetector"))
            {
                playerScript1.scoreGoal();
            }
            else if (name.Equals("GoalDetector2"))
            {
                playerScript2.scoreGoal();
            }
            Reset();
            UpdateText();
        }
    }

    void UpdateText()
    {
        scoreText.text = "Score: " + playerScript2.score + " - " + playerScript1.score;
    }

    private void Reset()
    {
        ball.isStick = false;
        ball.stickToPlayer1 = false;
        ball.stickToPlayer2 = false;

        playerController1.MoveSpeed = 0;
        playerController2.MoveSpeed = 0;

        Rigidbody rigidbody = ball.transform.gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;

        player1.transform.position = PlayerPosition1;
        player2.transform.position = PlayerPosition2;
        player1.transform.eulerAngles = PlayerRotation1;
        player2.transform.eulerAngles = PlayerRotation2;

        ball.transform.position = new Vector3(0f, 0.3f, 0f);

        if(playerScript1.score == 6)
        {
            gameOverText.SetText("Player 1 wins");
            gameOverText.gameObject.SetActive(true);
            newGameButtton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            return;
        }

        if (playerScript2.score == 6)
        {
            gameOverText.SetText("player 2 wins");
            gameOverText.gameObject.SetActive(true);
            newGameButtton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            return;
        }

        goalText.gameObject.SetActive(true);
        ResizeGoal();
        Invoke("Resume", 3);

    }

    void ResizeGoal()
    {

        if (name.Equals("GoalDetector"))
        {
            int score = playerScript1.score;

            switch (score) {
                case 1:
                    goal1.transform.localScale = level2;
                    transform.localScale = detectorlevel2;
                    break;
                case 2:
                    goal1.transform.localScale = level3;
                    transform.localScale = detectorlevel3;
                    break;
                case 3:
                    goal1.transform.localScale = level4;
                    transform.localScale = detectorlevel4;
                    break;
                case 4:
                    goal1.transform.localScale = level5;
                    transform.localScale = detectorlevel5;
                    break;
                case 5:
                    goal1.transform.localScale = level6;
                    transform.localScale = detectorlevel6;
                    break;
            }


        }
        else if (name.Equals("GoalDetector2"))
        {
            int score = playerScript2.score;

            switch (score)
            {
                case 1:
                    goal2.transform.localScale = level2;
                    transform.localScale = detectorlevel2;
                    break;
                case 2:
                    goal2.transform.localScale = level3;
                    transform.localScale = detectorlevel3;
                    break;
                case 3:
                    goal2.transform.localScale = level4;
                    transform.localScale = detectorlevel4;
                    break;
                case 4:
                    goal2.transform.localScale = level5;
                    transform.localScale = detectorlevel5;
                    break;
                case 5:
                    goal2.transform.localScale = level6;
                    transform.localScale = detectorlevel6;
                    break;
            }
        }

    }

    void Resume()
    {

        playerController1.MoveSpeed = 5;
        playerController2.MoveSpeed = 5;
        goalText.gameObject.SetActive(false);
    }


    public void NewGameClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitClicked()
    {
        Application.Quit();
    }
}

