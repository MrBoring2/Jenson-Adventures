using UnityEngine;

//Класс-родитель для наследуемых классов противников
public class EnemiesScript : MonoBehaviour
{
   
    public int healph;
    public int damage;
    public float speed;
    private float startSpeed;
    public float normalSpeed;
    private float startNormalSpeed;
    public bool isMoveToRight = true;
    private float stopTime;
    public float startStopTime;
    public Animator animator;
    public float cooldown;
 

    public float StopTime { get => stopTime; set => stopTime = value; }
    public float StartNormalSpeed { get => startNormalSpeed; set => startNormalSpeed = value; }
    public float StartSpeed { get => startSpeed; set => startSpeed = value; }


    //Установка скорости врага
    public void SetSpeed(float speed)
    {
        this.speed = speed;
        normalSpeed = speed;
    }

    void Awake()
    {
        StartSpeed = speed;
        StartNormalSpeed = normalSpeed;
        animator = GetComponent<Animator>();     
        normalSpeed = speed;
        
    }

    //Переопределяемый метод смерти
    virtual public void Dead()
    {       
    }

    //Переопределяемый метод получения урона
    virtual public void TakeDamage(int damage)
    {     
    }




}
