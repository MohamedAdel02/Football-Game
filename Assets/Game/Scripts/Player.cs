using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    StarterAssetsInputs starterAssetsInputs;
    Animator animator;
    public Ball ballAttachedToPlayer;
    float timeShoot = -1f;
    public const int ANIMATION_LAYER_SHOOT = 1;
    public int score = 0;
    public AudioSource kickSound;

    void Start()
    {

        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (starterAssetsInputs.shoot) 
        {
            starterAssetsInputs.shoot = false;
            timeShoot = Time.time;
            animator.Play("Shoot", ANIMATION_LAYER_SHOOT, 0);
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, 1f);
        }


        if (timeShoot > 0)
        {

            if (ballAttachedToPlayer != null && Time.time - timeShoot > 0.2)
            {
                kickSound.Play();
                ballAttachedToPlayer.stickToPlayer1 = false;
                ballAttachedToPlayer.stickToPlayer2 = false;
                ballAttachedToPlayer.isStick = false;
                Rigidbody rigidbody = ballAttachedToPlayer.transform.gameObject.GetComponent<Rigidbody>();
                Vector3 shootDirectoin = transform.forward;
                shootDirectoin.y += 0.3f;
                rigidbody.AddForce(shootDirectoin * 13f, ForceMode.Impulse);

                ballAttachedToPlayer = null;
            }

            if(Time.time - timeShoot > 0.5)
            {
                timeShoot = -1f;
            }
        }
        else
        {
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, 0f);

        }


    }

    public void scoreGoal()
    {
        score += 1;
    }

}
