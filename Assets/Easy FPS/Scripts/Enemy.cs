using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public RaycastHit hit;
    public float rotationSpeed;
    public float shootCooldown;
    public Transform head;

    private float timeFromLastShot = 0;
    public Transform rifle;
    private float maxSpeed;

    private UnityEngine.AI.NavMeshAgent agent;

    private Animator animator;

    public string shootingAnimation;
    public string idleAnimation;
    public string runningAnimation;
    public float animatorSpeedModifier = 1;

    // Start is called before the first frame update
    void Start()
    {
        // rifle = gameObject.transform.Find("Bip001 L Hand");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        maxSpeed = agent.speed;
        if (!target)
        {
            target = GameObject.FindWithTag("Player");
        }

        animator = GetComponent<Animator>();
    }

    bool targetInSight()
    {
        Vector3 targetDirection= target.transform.position - head.position;
        Debug.DrawRay(rifle.position, targetDirection, Color.yellow);

        if (!Physics.Raycast(head.position, targetDirection, out hit))
        {
            return false;
        }
        
        return hit.collider.gameObject == target;
    }

    bool targetInFront()
    {
        Debug.DrawRay(rifle.position, transform.forward*10000, Color.red);

        if (!Physics.Raycast(head.position, transform.forward, out hit))
        {
            return false;
        }
        
        return hit.collider.gameObject == target;
    }

    bool isPlaying(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    // Update is called once per frame
    void Update()
    {

        animator.speed = animatorSpeedModifier * Timekeeper.tempo;

        if(targetInSight())
        {
            agent.isStopped = true;
            if (targetInFront())
            {
                Shoot();
            }
            else
            {
                if (!isPlaying(runningAnimation))
                {
                    animator.Play(runningAnimation);
                }
                Vector3 targetDirection= target.transform.position - transform.position;
                float singleStep = rotationSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        } else
        {
            agent.isStopped = false;
            agent.destination = target.transform.position;
            agent.speed = maxSpeed * Timekeeper.tempo;
        }

    }

    public GameObject bullet;

    void Shoot()
    {
        if (!isPlaying(shootingAnimation))
            animator.Play(shootingAnimation);

        if (timeFromLastShot > 0)
            timeFromLastShot -= Time.deltaTime * Timekeeper.tempo;
        else
        {
            timeFromLastShot = shootCooldown;
            Instantiate(bullet, rifle.position, transform.rotation);
        }
    }

};
       
