using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Rigidbody2D rb;
    public Sprite[] sprites;
    public SpriteRenderer render;
    public bool hasKey = false;
    public static PlayerController instance;

    public AudioSource soundEffects;
    public AudioClip itemCollect;
    public AudioClip doorEnter;
    public AudioClip doorExit;
    public AudioClip woodbreak;

    public AudioClip[] sounds;  

    void Start()
    {
        soundEffects = GetComponent<AudioSource>();

        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
          
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 pos = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            pos.y += speed;
            render.sprite = sprites[1];
            render.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed;
            render.sprite = sprites[2];
            render.flipX = false;

        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.y -= speed;
            render.sprite = sprites[0];
            render.flipX = false;

        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed;
            render.sprite = sprites[2];
            render.flipX = true;


        }
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if colliding with a game object with specific tag
        if (collision.gameObject.tag.Equals("Door1"))
        {
            Debug.Log("change scene");
            soundEffects.PlayOneShot(doorEnter, 0.7f);
            gameObject.transform.position = new Vector2(0, 0);
            SceneManager.LoadScene(2);
        }
        if (collision.gameObject.tag.Equals("Door3"))
        {
            Debug.Log("change scene");
            soundEffects.PlayOneShot(doorEnter, 0.7f);
            gameObject.transform.position = new Vector2(0, 0);
            SceneManager.LoadScene(1);
        }

        if (collision.gameObject.tag.Equals("Key"))
        {
            soundEffects.PlayOneShot(itemCollect,0.7f);
            Debug.Log("obtained key");
            hasKey = true; //player has the key now
        }

        if (collision.gameObject.tag.Equals("Door2") && hasKey == true)
        {
            Debug.Log("unlocked door!");
            soundEffects.PlayOneShot(woodbreak, 0.7f);
            gameObject.transform.position = new Vector2(0, 0);

            SceneManager.LoadScene(3);

            //take to new scene
        }
    }
}
