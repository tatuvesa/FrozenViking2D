using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public GameObject axePrefab; // T‰m‰ on prefab, joka on asetettu Unityss‰
    public Transform throwPoint; // T‰m‰ on tyhj‰ GameObject, joka on asetettu Unityss‰
    public float throwForce; // T‰m‰ on heittovoima

    public Animator animator; // Animaattori animaatioille 
    public Rigidbody2D rb2D; // T‰ll‰ laitetaan hahmo hypp‰‰m‰‰n

    public Transform GroundCheckPosition; // Tarkistaa onko hahmo maassa
    public float GroundCheckRadius; // Tarkistusalueen s‰de
    public LayerMask GroundCheckLayer; // Maa layer
    public bool isGrounded; // Onko hahmo maassa

    public Image filler; // T‰ynn‰ kun hahmo ei ole ottanut damagea
    public float counter; // Laskee nollasta kahteen ja alottaa alusta
    public float maxCounter; // Counterin nopeus

    public bool isInFireHeal; // Onko hahmo tulen p‰‰ll‰


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ground testi, ollaanko maassa vai ilmassa?
        if (Physics2D.OverlapCircle(GroundCheckPosition.position, GroundCheckRadius, GroundCheckLayer))
        {
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        }

        // Liikuttaa hahmoa vasemmalle ja oikealle
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f);

        // K‰‰nt‰‰ hahmon oikealle
        if(Input.GetAxisRaw("Horizontal") != 0) 
        {
            // Meill‰ on joko A tai D pohjassa
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("walk", true);
        }

        else
        {
            // Else ajetaan vain ku ei liikuta
            animator.SetBool("walk", false);
        }

        if (isGrounded && Input.GetButtonDown("Jump")) // Hypp‰‰minen
        {
            // V‰lilyˆnti painettu t‰ll‰ framella
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("jump");
        }

        if (counter > maxCounter)
        {
            GameManager.manager.previousHealth = GameManager.manager.Health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth / GameManager.manager.maxHealth, GameManager.manager.Health / GameManager.manager.maxHealth, counter / maxCounter);

        // check if player is in fireheal trigger and holding F
        if (isInFireHeal && Input.GetKey(KeyCode.F))
        {
            Heal(1);
        }

        if (Input.GetKeyDown(KeyCode.T)) // Heitt‰‰ kirveen
        {
            ThrowAxe();
        }

    } // Update ends

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Ouch!");
            TakeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AddHealth"))
        {
            Destroy(collision.gameObject);
            Heal(10);
        }

        if (collision.gameObject.CompareTag("AddMaxHealth"))
        {
            Destroy(collision.gameObject);
            AddMaxHealth(20);
        }

        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            SceneManager.LoadScene("Map");
        }

        if (collision.gameObject.CompareTag("FireHeal"))
        {
            isInFireHeal = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireHeal"))
        {
            isInFireHeal = false;
        }
    }

    void AddMaxHealth(float amt)
    {
        GameManager.manager.maxHealth += amt;
    }

    void Heal(float amt)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.Health += amt;
        if (GameManager.manager.Health > GameManager.manager.maxHealth)
        {
            GameManager.manager.Health = GameManager.manager.maxHealth;
        }
    }

    void TakeDamage(float dmg)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.Health -= dmg;
    }

    void ThrowAxe()
    {
        GameObject axe = Instantiate(axePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        rb.AddForce(throwPoint.right * throwForce, ForceMode2D.Impulse);
    }
}
