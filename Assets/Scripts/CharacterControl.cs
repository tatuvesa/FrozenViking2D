using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public Animator animator; // Animaattori animaatioille 
    public Rigidbody2D rb2D; // T‰ll‰ laitetaan hahmo hypp‰‰m‰‰n

    public Transform GroundCheckPosition; // Tarkistaa onko hahmo maassa
    public float GroundCheckRadius; // Tarkistusalueen s‰de
    public LayerMask GroundCheckLayer; // Maa layer
    public bool isGrounded; // Onko hahmo maassa

    public Image filler; // T‰ynn‰ kun hahmo ei ole ottanut damagea
    public float Health; // Hahmon health
    public float previousHealth; // Hahmon edellinen health
    public float maxHealth; // Hahmon maksimi health

    public float counter; // Laskee nollasta kahteen ja alottaa alusta
    public float maxCounter; // Counterin nopeus




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
            previousHealth = Health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(previousHealth / maxHealth, Health / maxHealth, counter / maxCounter);

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
            AddMaxHealth(50);
        }
    }
    void AddMaxHealth(float amt)
    {
        maxHealth += amt;
    }

    void Heal(float amt)
    {
        previousHealth = filler.fillAmount * maxHealth;
        counter = 0;
        Health += amt;
        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
    }

    void TakeDamage(float dmg)
    {
        previousHealth = filler.fillAmount * maxHealth;
        counter = 0;
        Health -= dmg;
    }
}
