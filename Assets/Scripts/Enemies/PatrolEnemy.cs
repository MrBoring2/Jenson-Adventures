using System.Collections;
using UnityEngine;

public class PatrolEnemy : EnemiesScript
{
    public Transform point;   
    public HeroStats player;
    public CircleCollider2D col;
    bool isAttacking=true;
    public AnimationClip deadAnimation;
    public int positionOfPatrol;
    public GameController game;
    private bool isDead = false;
    

    // Start is called before the first frame update
    void Start()
    {

        isDead = false;
      
    }
    //void Awaike()
    //{
    //    game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    //}
    // Update is called once per frame
    void Update()
    {
        //Остановка врага при получении урона на StopTime секунд
;       if (StopTime<=0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            StopTime -= Time.deltaTime;
        }

        //Вычисление дистанции потрулирования и выполнение метода патруля
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol)
        {
            Patrol();
        }

        //Обновление переменной аниматора
        animator.SetInteger("Health", healph);

        if (healph <= 0)
        {
            Dead();

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



    void Patrol()
    {
        //Поворот потрульного
        if (transform.position.x > point.position.x + positionOfPatrol - 0.1)
        {
            isMoveToRight = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < point.position.x - positionOfPatrol + 0.1)
        {
            isMoveToRight = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        //Движение патрульного
        if (isMoveToRight)
        {
            transform.position = new Vector2(transform.position.x + speed * 1 * Time.deltaTime, transform.position.y);
        }
        else transform.position = new Vector2(transform.position.x + speed * -1 * Time.deltaTime, transform.position.y);
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
        if (isDead==true)
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
