using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour
{

    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Tarkistetaan ongo Gamemanagerilla tietoa, ett‰ tullaanko jostain tasosta takaisin mappiin
        // Jos on, niin etsit‰‰n scenest‰ t‰m‰ leveltrigger ja sijoitetaan mapcharacter spawnpointtiin
        if (GameManager.manager.currentLevel != "")
        {
            // jokin taso on p‰‰sty l‰pi ja tullaan takaisin mappiin
            GameObject.Find(GameManager.manager.currentLevel).GetComponent<LoadLevel>().Cleared(true);

            transform.position = GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(1).transform.position;

        }
    }

    // Update is called once per frame
    void Update()
    {
        // make character more with WASD
        float horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(horizontalMove, verticalMove, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LevelTrigger")
        {
            GameManager.manager.currentLevel = collision.gameObject.name;

            SceneManager.LoadScene(collision.gameObject.GetComponent<LoadLevel>().levelToLoad);
        }
    }
}

