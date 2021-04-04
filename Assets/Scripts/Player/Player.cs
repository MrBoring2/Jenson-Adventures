using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float maxSpeed = 10f;
    private bool isMoveToRight = true;
    public int jumps;
    private int curJumps;
    public Animator anim;
    public bool isGrounded;
    public LayerMask whatIsGround;
    public float jumpForce;
    Rigidbody2D rb;
    public Transform groundCheck;
    HeroStats player;
    public float cooldown;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;
    private float startSpeed;
    float move;
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float StartSpeed { get => startSpeed; }
    public bool IsMoveToRight { get => isMoveToRight; set => isMoveToRight = value; }
    public float Move { get => move; set => move = value; }

    private bool isCooldown;
    public void SetSpeed(float speed)
    {
        maxSpeed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<HeroStats>();
        curJumps = jumps;
        startSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (player.IsDead==false)
        {
            //Атака
            if (isCooldown == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    anim.SetTrigger("Attack");
                    isCooldown = true;
                    StartCoroutine(Cooldown());
                }
            }

            //Прыжок
            if (Input.GetButtonDown("Jump") && curJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.SetBool("IsGround", false);
                isGrounded = false;
                curJumps--;
            }
            else if (Input.GetButtonDown("Jump") && curJumps == 0 && isGrounded)
            {
                anim.SetBool("IsGround", false);
                rb.velocity = Vector2.up * jumpForce;
            }
            //Обновление кол-ва прыжков
            if (isGrounded)
            {
                curJumps = jumps;

            }
        }
        
    }
    //Корутина кулдауна
    IEnumerator Cooldown()
    {
        if (isCooldown)
        {
            yield return new WaitForSeconds(cooldown);
            isCooldown = !isCooldown;
        }
    }

    void FixedUpdate()
    {
        if (player.IsDead==false)
        {
            //Проверка на земле персонаж или нет
            isGrounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);
            //Установка переменных дял аниматора
            anim.SetBool("IsGround", isGrounded);
            anim.SetFloat("VerticalSpeed", rb.velocity.y);

            //Обработка нажатия кнопки движения влево-вправо
            Move = Input.GetAxis("Horizontal");
            //Установка переменной дял аниматора
            anim.SetFloat("Speed", Mathf.Abs(Move));

            //Движение персонажа
            rb.velocity = new Vector2(Move * maxSpeed, rb.velocity.y);
            if (Move > 0 && !IsMoveToRight)
                Flip();
            else if (Move < 0 && IsMoveToRight)
                Flip();
        }
    }

    //Метод поворота персонажа
    private void Flip()
    {
        IsMoveToRight = !IsMoveToRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Метод для графического отображния радиуса атаки персонажа (только в редакторе)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    //Метод атаки, вызываемый анимацией
    public void OnAttack()
    {
        //Получание всех коллайдеров в радиусе атаки персонажа и нахождение полигонального коллайдера
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetType() == typeof(PolygonCollider2D))
            {
                enemies[i].GetComponent<EnemiesScript>().TakeDamage(damage);
            }
        }                          
    } 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="MovingPlatform")
        {
            this.transform.parent = collision.transform;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = null;
        }
    }
}
