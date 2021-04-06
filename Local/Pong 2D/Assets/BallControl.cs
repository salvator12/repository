using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    //Besarnya gaya awal yang diberkan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;
    // Titik lintasan bola saat ini
    private Vector2 trajectoryOrigin;
    private float constantSpeed = 10.0f;
    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = rigidBody2D.velocity = Vector2.zero;
        // Reset kecepatan menjadi (0,0)
    }

    void PushBall()
    {
        // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        float yRandomIntialForce = Random.Range(-yInitialForce, yInitialForce);
        //yInitialForce = 2.0f;
        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        // Jika nilainya di bawah 1, bola bergerak ke kiri.
        // Jika tidak, bola bergerak ke kanan.
        if(randomDirection < 1.0f)
        {
            // Gunakan gaya untuk menggerakan bola ini.
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yInitialForce));
        } else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yInitialForce));
        }
    }

    void RestartGame()
    {
        // kembalikan bola ke posisi semula
        ResetBall();
        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
        // Mulai Game
        RestartGame();
    }
    
    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut.
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }
    
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }

    private void Update()
    {
        Vector2 unit = GetComponent<Rigidbody2D>().velocity.normalized;
        GetComponent<Rigidbody2D>().velocity = unit * constantSpeed;
    }
}
