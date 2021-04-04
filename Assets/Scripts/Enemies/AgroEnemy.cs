using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroEnemy : EnemiesScript
{

    public HeroStats player;
    public CircleCollider2D col;
    bool isAttacking = true;
    public AnimationClip deadAnimation;
    public GameController game;
    private bool isDead = false;
 
    public Transform defendPoint;
    public Transform MaxPoint;
    public Transform MinPoint;

    public Transform GroundCheck;
    Transform playerPos;
    public float positionofpatrol;
    public float stoppingDistance;

    bool agro=false;
    bool goback=false;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        isDead = false;
        animator.SetBool("IsRun", false);
    }

    void Update()
    {
        var heading = transform.position - player.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; 

        if(direction.x<0)
        {
            isMoveToRight = true;
        }
        else if(direction.x>0)
        {
            isMoveToRight = false;
        }
        if(isMoveToRight)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
            if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance)
            {
                agro = true;
                goback = false;
            }
            else if (Vector2.Distance(transform.position, player.transform.position) > stoppingDistance)
            {
                agro = false;
                goback = true;
            }

            if (agro == true)
            {
                Agro();
            }
            else if (goback == true)
            {
                GoBack();
            }

            if (transform.position.x == defendPoint.position.x)
            {
                animator.SetBool("IsRun", false);

            }
        ////Остановка врага при получении урона на StopTime секунд
        if (StopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            StopTime -= Time.deltaTime;
        }
        animator.SetInteger("Health", healph);

        if (healph <= 0)
        {
            Dead();

        }
    }
    void Agro()
    {
        animator.SetBool("IsRun", true);
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
        
        if (Physics2D.Raycast(GroundCheck.position, Vector2.down, 1).collider == false)
        {
            animator.SetBool("IsRun", false);
            agro = false;
            GoBack();
            
        }
    }
    void GoBack()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(defendPoint.transform.position.x, transform.position.y), speed * Time.deltaTime);
        animator.SetBool("IsRun", true);
        if (Physics2D.Raycast(GroundCheck.position, Vector2.down, 1).collider == false)
        {
            animator.SetBool("IsRun", false);
        }
    }
    void FixedUpdate()
    {

    }

    //Метод получения урона
    override public void TakeDamage(int damage)
    {
        StopTime = startStopTime;
        if (healph > 0)
        {
            healph -= damage;
            animator.SetTrigger("TakeDamage");

        }
        if (healph <= 0)
        {
            isDead = true;
            healph = 0;
            animator.SetInteger("Health", healph);
            game.KillsOfEnemies++;
        }
    }

    
    //Когда игрок попадает в радиус атаки врага и если атака не была совершена и персонаж жив, то совершается атака
    public void OnTriggerStay2D(Collider2D other)
    {
        if (animator.GetBool("Attack") == false && player.IsDead == false)
        {

            if (other.CompareTag("Player"))
            {
                isAttacking = true;
                animator.SetTrigger("Attack");
            }
        }
    }

    //Когда игрок выходит из радиуса действия атаки, урон не нанесётся
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isAttacking == true)
        {
            isAttacking = false;
        }


    }

    //Метод атаки
    public void OnEnemyAttack()
    {
        animator.SetBool("Attack", true);
        if (isAttacking == true)
        {
            player.GetComponent<HeroStats>().TakeDamage(damage);
        }
        //Запуск корутины кулдауна
        StartCoroutine(Cooldown());

    }

    //Корутина кулдаун
    IEnumerator Cooldown()
    {
        if (animator.GetBool("Attack"))
        {
            yield return new WaitForSeconds(cooldown);
            animator.SetBool("Attack", !animator.GetBool("Attack"));
            isAttacking = true;
        }
    }

    //Метод смерти
    override public void Dead()
    {
        if (isDead == true)
        {
            SetSpeed(0);
            animator.Play(deadAnimation.name);
            animator.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Collider2D[] coliders = animator.gameObject.GetComponents<Collider2D>();
            for (int i = 0; i < coliders.Length; i++)
            {
                if (coliders[i].GetType() != typeof(EdgeCollider2D))
                {
                    coliders[i].enabled = false;
                }
            }

            Destroy(gameObject, 2);

        }
    }
}
