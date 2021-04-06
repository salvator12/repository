using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;
    public KeyCode upButton = KeyCode.W; // bergerak ke atas
    public KeyCode downButton = KeyCode.S; // bergerak ke bawah
    public float speed = 10.0f; // kecepatan gerak 
    public float yBoundary = 9.0f;// batas atas dan batas bawah game scene
    
    private Rigidbody2D rigidBody2D; // rigidBody 2D raket ini
    private int score; // pengaturan score

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rigidBody2D.velocity; // dapatkan kecepatan raket sekarang

        
        if(Input.GetKey(upButton)) { // jika pemain menekan tombol atas, beri kecepatan positif ke komponen y (ke atas)
            velocity.y = speed;
        } else if(Input.GetKey(downButton))
        { // jika pemain menekan tombol bawah, beri kecepatan negatif ke komponen y (ke bawah)
            velocity.y = -speed;
        } else
        {
            velocity.y = 0.0f;
        }
        rigidBody2D.velocity = velocity;
        
        Vector3 position = transform.position; // dapatkan posisi raket sekarang.
        if(position.y > yBoundary)
        { // Jika posisi raket melewati batas atas (yBoundary), kembalikan ke batas atas tersebut.
            position.y = yBoundary;
        } else if(position.y < -yBoundary)
        { // Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tersebut.
            position.y = -yBoundary;
        }
        transform.position = position; // Masukkan kembali posisinya ke transform.
    }
    public void IncrementScore() // Menaikkan score sebanyak 1 point
    {
        score++;
    }

    public void ResetScore() // Mengembalikan menjadi 0
    {
        score = 0;
    }
    public int Score // mendapatkan nilai skor
    {
        get { return score; }
    }
}
