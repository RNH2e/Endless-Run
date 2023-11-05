using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // public UnityEngine.UI.Button button;
    // public UnityEngine.UI.Image image;

   // public Text highscoretext, scoreText, diamondText, highScoreText;
    int diamond;
    float highscore;
    public float speed = 7.0f;
    public float toFall = 8.0f;
    private CharacterController myCharacterContreller;
    public bool gameResume = true;
    public GameObject finishParticle;
    public Image gameOverScreen;
 //   public Animator m_animator;
    public float jumpSpeed = 10f;
    Rigidbody rb;
    public SpawnTile spawnTile;
    public Vector3 lastCorner, direction;
    bool Ground = false;
    public ButtonControl rebuton;
    public float distance;
    bool highscoreBool = true;
    public int counter = 3;
    bool jump = true;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    float jumpCounter;

    //singleton

    void Start()
    {
        // button btn = button.GetComponent<button>();

        highscore = PlayerPrefs.GetFloat("highscore", 0);
     //   highscoretext.text = highscore.ToString("#.##");

        myCharacterContreller = GetComponent<CharacterController>();
        //  GameObject tileToSpawn = GetComponent<SpawnTile>().gameObject;
        rb = GetComponent<Rigidbody>();
    }


    Vector3 mouseStart;
    bool hsShow;
    bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jump = true;
            jumpCounter = 0;
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            mouseStart = Input.mousePosition;
            // jump = true;
            // rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

        }

        if (Input.GetMouseButtonDown(0))
        {
            //jump = true;
            //jumpCounter = 0;

        }

        if (jump)
        {
            DoubleClick();
            transform.position += Vector3.up * Time.deltaTime * 20 * jumpCounter;

            jumpCounter += 2 * Time.deltaTime;
            if (jumpCounter >= 1)
            {
                jump = false;
            }
        }


        if (Input.GetMouseButton(0))
        {

            if (direction.z > 0.5f)
            {

                transform.position = new Vector3(lastCorner.x + (mouseStart - Input.mousePosition).x * -0.01f, transform.position.y, transform.position.z);
            }

            else if (direction.x > 0.5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, lastCorner.z + (mouseStart - Input.mousePosition).x * 0.01f);
            }

            else if (direction.z < -0.5f)
            {

                transform.position = new Vector3(lastCorner.x + (mouseStart - Input.mousePosition).x * 0.01f, transform.position.y, transform.position.z);
            }

            else if (direction.x < -0.5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, lastCorner.z + (mouseStart - Input.mousePosition).x * -0.01f);
            }

        }

        // if (direction.y < 0.5)
        // {

        //     transform.position = new Vector3(transform.position.x, lastCorner.y + (mouseStart - Input.mousePosition).y * -0.01f, transform.position.z  );
        //}





        if (!gameResume)
        {
            return;
        }


        if (Input.touchCount > 0)
        {
            //Debug.Log(gameObject.name);
            Touch parmak = Input.GetTouch(0);
        }

       // distance += speed * Time.deltaTime; scoreText.text = distance.ToString("#.##");
        transform.position += transform.forward * speed * Time.deltaTime;



        if (distance > highscore)
        {
          //  highscore = distance;
          //  highscoretext.text = highscore.ToString("#.##");
          //
          //  if (!hsShow)
          //  {
          //      highScoreText.gameObject.SetActive(true);
          //      hsShow = true;
          //      Invoke("hidehs", 2);
          //  }




        }
        ///RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down,/*out hit*/ .1f, 1 << 8))
        {
            Ground = true;
        }
        else
        {
            Ground = false;
        }

        if (!Ground)
        {

            transform.position += Vector3.down * Time.deltaTime * 6.0f;
        }

    }

    void hidehs()
    {
       // highScoreText.gameObject.SetActive(false);

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TurnControl>())
        {
            transform.rotation = Quaternion.LookRotation(other.gameObject.GetComponent<TurnControl>().direction);
            direction = other.gameObject.GetComponent<TurnControl>().direction;
            Vector3 yeni = other.transform.position;
            yeni.y = transform.position.y;
            transform.position = yeni;
            lastCorner = yeni;
            //   Debug.Log("çarptý"  + gameObject.name);

        }

        if (other.transform.tag == "cali")
        {
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "GG")
        {
            GetComponentInChildren<Animator>().SetTrigger("death");

            Invoke("GameOver", 2);

        }

        if (other.transform.tag == "ball")
        {

            Destroy(other.gameObject);
            diamond += 1;
            //diamondText.text = diamond.ToString("+ #.##");
            GetComponentInChildren<Animator>().SetTrigger("diamond");
        }

        if (other.transform.tag == "bomb")
        {
            Invoke("GameOver", 2);

            //  transform.rotation = Quaternion.AngleAxis(-90, Vector3.left);
            // stop.gameObject.GetComponent<Animator>().enabled = false;

            // other.transform.GetChild(0).parent = null;
            // other.transform.GetChild(0).gameObject.SetActive(true);
            gameResume = false;
            GetComponentInChildren<Animator>().SetTrigger("death");
            other.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            PlayerPrefs.SetFloat("highscore", highscore);

            Destroy(other.gameObject,.5f);


            //if (gameOverScreen.gameObject)
            //{
            //    Point.score = 0;
            //    PlayerPrefs.Save();
            //}


        }
    }
    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
    }
}

