using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    public float patrolSpeed = 0.5f;
    //public float chaseSpeed = 3f;
    public float chaseSpeed = 6f;
    public float detectDistance = 16f;
    public Transform[] patrols;
    private int patrolIndex = 0;

    private Animator animator;
    private NavMeshAgent agent;
    private Player player;
    private CapsuleCollider col;

    private bool seenPlayer = false;
    private bool dead = false;

    public float attackDistance = 2;
    public float attackDamage = 10;
    public float attackCooldown = 3;
    private float attackCurrent = 0;

    public bool passive;
    public bool horde;
    public bool quest = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        col = GetComponent<CapsuleCollider>();

        if (Difficulty.difficultyScript.diff == 1)
        {
            maxHealth = 20;
            health = maxHealth;
            chaseSpeed = 3f;
            attackDamage = 5;
        }
        else if(Difficulty.difficultyScript.diff == 2)
        {
            maxHealth = 100;
            health = maxHealth;
            chaseSpeed = 6f;
            attackDamage = 10;
        }
        else if (Difficulty.difficultyScript.diff == 3)
        {
            maxHealth = 200;
            health = maxHealth;
            chaseSpeed = 10f;
            attackDamage = 20;
        }
        else
        {
            maxHealth = 100;
            health = maxHealth;
            chaseSpeed = 6f;
            attackDamage = 10;
        }
    }

    void Update()
    {
        if (!dead)
        {
            if (horde)
            {
                seenPlayer = true;
            }

            attackCurrent -= Time.deltaTime;

            UpdateState();
            UpdateSpeed();
        }
        else
        {
            agent.speed = 0;
            Destroy(col);
        }
    }

    private void UpdateState()
    {
        if(health < maxHealth || horde)
        {
            Chase();
            //seenPlayer = true;
        }

        //if (horde) return;
        //if (health < maxHealth) return;
        
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist > detectDistance && !seenPlayer)
        {
            Patrol();
        }
        else
        {
            seenPlayer = true;
            if (dist > attackDistance)
            {
                Chase();
            }
            else
            {
                if(attackCurrent <= 0)
                {
                    AttackPlayer();
                }
            }
            
            
        }
        
    }

    private void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.transform.position);
    }

    private void Patrol()
    {
        if (!seenPlayer)
        {

            agent.speed = patrolSpeed;
            if (patrols.Length == 0) return;

            agent.SetDestination(patrols[patrolIndex].position);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                patrolIndex = (patrolIndex + 1) % patrols.Length;
                agent.SetDestination(patrols[patrolIndex].position);
            }
        }
    }

    private void UpdateSpeed()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speed", speed);
    }

    public void TakeDamage(float damage)
    {
        if (dead) return;

        health -= damage;
        if(health <= 0)
        {
            dead = true;
            animator.SetTrigger("die");

            if (quest)
            {
                player.ProgressQuest(1);
            }
        }
    }

    private void AttackPlayer()
    {
        animator.SetTrigger("attack");
        attackCurrent = attackCooldown;
        player.TakeDamage(attackDamage);
    }
}
