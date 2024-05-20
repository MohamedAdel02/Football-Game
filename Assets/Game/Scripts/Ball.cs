using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus.Input;
using UnityEngine;


public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    StarterAssetsInputs starterAssetsInputs1;
    StarterAssetsInputs starterAssetsInputs2;
    ThirdPersonController playerController1;
    ThirdPersonController playerController2;
    public Transform playerTransform1;
    public Transform playerTransform2;
    public Transform ballPosition1;
    public Transform ballPosition2;
    public bool stickToPlayer1;
    public bool stickToPlayer2;
    public bool isStick = false;
    Player player;
    float speed;
    Vector3 previousLocation;
    public Player playerScript1;
    public Player playerScript2;
    Vector3 PlayerPosition1;
    Vector3 PlayerPosition2;
    Vector3 PlayerRotation1;
    Vector3 PlayerRotation2;


    void Start()
    {
        PlayerPosition1 = playerTransform1.position;
        PlayerPosition2 = playerTransform2.position;
        PlayerRotation1 = playerTransform1.eulerAngles;
        PlayerRotation2 = playerTransform2.eulerAngles;
        playerScript1 = playerTransform1.GetComponent<Player>();
        playerScript2 = playerTransform2.GetComponent<Player>();
        playerController1 = playerTransform1.GetComponent<ThirdPersonController>();
        playerController2 = playerTransform2.GetComponent<ThirdPersonController>();
        starterAssetsInputs1 = playerTransform1.GetComponent<StarterAssetsInputs>();
        starterAssetsInputs2 = playerTransform2.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stickToPlayer1)
        {
            player = playerScript1;
        }

        if (stickToPlayer2)
        {
            player = playerScript2;
        }

        if (isStick)
        {

            if (starterAssetsInputs1.tackle)
            {
                stickBallToPlayer1();
            }

            if (starterAssetsInputs2.tackle)
            {
                stickBallToPlayer2();
            }

        }
        else
        {
            stickBallToPlayer1();
            stickBallToPlayer2();
        }

        starterAssetsInputs1.tackle = false;
        starterAssetsInputs2.tackle = false;

        if (stickToPlayer1)
        {
            Vector3 currentLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
            transform.position = ballPosition1.position;
            transform.Rotate(new Vector3(playerTransform1.forward.x, playerTransform1.forward.y, playerTransform1.forward.z), speed);
            previousLocation = currentLocation;
        }


        if (stickToPlayer2)
        {
            Vector3 currentLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
            transform.position = ballPosition2.position;
            transform.Rotate(new Vector3(playerTransform2.forward.x, playerTransform2.forward.y, playerTransform2.forward.z), speed);
            previousLocation = currentLocation;
        }

        if(transform.position.x < -13.65 || transform.position.x > 13.65 || transform.position.z < -8.7 || transform.position.z > 8.7)
        {
            playerController1.MoveSpeed = 0;
            playerController2.MoveSpeed = 0;
            isStick = false;
            stickToPlayer1 = false;
            stickToPlayer2 = false;
            playerTransform1.position = PlayerPosition1;
            playerTransform2.position = PlayerPosition2;
            playerTransform1.eulerAngles = PlayerRotation1;
            playerTransform2.eulerAngles = PlayerRotation2;
            gameObject.SetActive(false);
            Invoke("RePosition", 1);
        }

    }

    void RePosition()
    {
        playerController1.MoveSpeed = 5;
        playerController2.MoveSpeed = 5;
        gameObject.SetActive(true);
        Rigidbody rigidbody = transform.gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        transform.position = GetBallPosition();

    }

    Vector3 GetBallPosition()
    {

        if(player == playerScript1)
        {
            return new Vector3(8.5f, 0.3f, 0f);
        }  
        else
        {
            return new Vector3(-8.5f, 0.3f, 0f);
        }

    }

    void stickBallToPlayer1()
    {
        float distance1 = Vector3.Distance(transform.position, playerTransform1.position);
        if (distance1 < 0.5)
        {
            stickToPlayer1 = true;
            stickToPlayer2 = false;
            playerScript1.ballAttachedToPlayer = this;
            isStick = true;
        }
    }

    void stickBallToPlayer2()
    {
        float distance2 = Vector3.Distance(transform.position, playerTransform2.position);
        if (distance2 < 0.5)
        {
            stickToPlayer2 = true;
            stickToPlayer1 = false;
            playerScript2.ballAttachedToPlayer = this;
            isStick = true;
        }
    }
}
