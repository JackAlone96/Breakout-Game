using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public int combo = 1;
    public Vector2 velocity = new Vector2(4, 4);
    private CircleCollider2D circleCollider;
    private GameObject lastObject;
    private AudioController audioController;
    private GameManager gameManagerScript;
    public AudioClip paddleHitClip;
    public AudioClip wallHitClip;

    private void Start()
    {
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * gameManagerScript.ballSpeed * Time.deltaTime);

        // Call the BallLost method whenever the ball leaves the screen
        if (transform.position.y < -Camera.main.orthographicSize)
        {
            gameManagerScript.BallLost();
        }

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, circleCollider.radius, velocity, (velocity * Time.deltaTime).magnitude);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != circleCollider && hit.transform.gameObject != lastObject)
            {
                velocity = Vector2.Reflect(velocity, hit.normal);
                lastObject = hit.transform.gameObject;

                if (hit.transform.GetComponent<MovePaddle>())
                {
                    velocity.y = Mathf.Abs(velocity.y);
                    audioController.PlayClip(paddleHitClip);
                }
                else if (hit.transform.GetComponent<DestroyBlock>())
                {
                    hit.transform.GetComponent<DestroyBlock>().HitBlock();
                    combo++;
                }
                else if (hit.transform.gameObject.CompareTag("Wall"))
                {
                    audioController.PlayClip(wallHitClip);
                }
            }
        }
    }

    /* private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject != lastObject)
        {
            velocity = Vector2.Reflect(velocity, collision.GetContact(0).normal);
            lastObject = collision.gameObject;
            
            if (collision.gameObject.CompareTag("Block"))
            {
                collision.gameObject.GetComponent<DestroyBlock>().HitBlock();
                combo++;
            }
            else if (collision.gameObject.CompareTag("Paddle"))
            {
                velocity.y = Mathf.Abs(velocity.y);
                audioController.PlayClip(paddleHitClip);
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                audioController.PlayClip(wallHitClip);
            }
        }
        
    } */



    // Method to reset the velocity of the ball whenever it goes off screen
    public void ResetVelocity()
    {
        Vector3 currentVelocity = velocity;
        currentVelocity.y = Mathf.Abs(currentVelocity.y);
        velocity = currentVelocity;
    }


}
