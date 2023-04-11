using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle : MonoBehaviour
{
    public float speed;
    private float hInput;
    private float xLimit = 8f;
    private Vector3 baseSize;
    private bool hasPowerup2 = false;
    private bool hasMalus = false;
    public GameObject ball;

    // Update is called once per frame
    void Update()
    {
        // Movement of the Paddle
        hInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * hInput * speed * Time.deltaTime);

        // To prevent the compenetration between the paddle and the wall

        if (transform.position.x >= xLimit)
        {
            if (hasPowerup2)
            {
                transform.position = new Vector3(7.5f, -3.8f, 0f);
            }
            else if (hasMalus)
            {
                transform.position = new Vector3(8.5f, -3.8f, 0f);
            }
            else
            {
                transform.position = new Vector3(xLimit, -3.8f, 0f);
            }
        }
        else if (transform.position.x <= -xLimit)
        {
            if (hasPowerup2)
            {
                transform.position = new Vector3(-7.5f, -3.8f, 0f);
            }
            else if (hasMalus)
            {
                transform.position = new Vector3(-8.5f, -3.8f, 0f);
            }
            else
            {
                transform.position = new Vector3(-xLimit, -3.8f, 0f);
            }            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Powerup2") && !hasPowerup2)
        {
            Destroy(other.gameObject);
            hasPowerup2 = true;
            transform.localScale = new Vector3(2, 1, 1);
            StartCoroutine(Powerup2());
        }

        if (other.gameObject.CompareTag("Powerup3"))
        {
            Destroy(other.gameObject);
            Instantiate(ball, transform.position + Vector3.up, Quaternion.identity);
        }

        if (other.gameObject.CompareTag("Malus") && !hasMalus)
        {
            Destroy(other.gameObject);
            hasMalus = true;
            transform.localScale = new Vector3(.5f, 1, 1);
            StartCoroutine(Malus());
        }
    }

    IEnumerator Powerup2()
    {
        yield return new WaitForSeconds(5);
        transform.localScale = new Vector3(1, 1, 1);
        hasPowerup2 = false;
    }

    IEnumerator Malus()
    {
        yield return new WaitForSeconds(5);
        transform.localScale = new Vector3 (1, 1, 1);
        hasMalus = false;
    }
}
