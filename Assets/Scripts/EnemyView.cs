using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyView : MonoBehaviour
{
    public Rigidbody2D enemyRb;
    public float speed = 5f;
    private bool isMovingToPlayer = false;
    private Rigidbody2D playerRb;
    private float distance;
    
    void Update()
    {
        if (playerRb == null) return; // evita erro de NullReference
        distance = Vector2.Distance(enemyRb.position, playerRb.position);

        if (isMovingToPlayer)
        {
            if (distance > 1.5f)
            {
                MoveToPlayer(playerRb, enemyRb);
            }
            else
            {
                isMovingToPlayer = false;
                Debug.Log("Inimigo alcançou o jogador!");
                SceneManager.LoadScene("Batting");
            }
        }
    }
    
    private void StopPlayerMovement(Rigidbody2D plyrRb, PlayerMovement playerMovement)
    {
        plyrRb.linearVelocity = Vector2.zero;
        plyrRb.angularVelocity = 0f;
        plyrRb.constraints = RigidbodyConstraints2D.FreezeAll;

        playerMovement.isFrozen = true;
    }

    private void MoveToPlayer(Rigidbody2D plyrRb, Rigidbody2D objectToMove)
    {
        float step = speed * Time.deltaTime;
        Debug.Log(objectToMove.transform.position + " "+ plyrRb.transform.position);
        objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position, plyrRb.transform.position, step);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (!other.CompareTag("Player")) return;
        playerRb = other.GetComponent<Rigidbody2D>();
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
        Debug.Log("Player entered");
        StopPlayerMovement(playerRb, playerMovement);
        isMovingToPlayer = true;
    }
}
