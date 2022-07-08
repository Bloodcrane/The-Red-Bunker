using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe_system : MonoBehaviour
{
    public Animator AxeAnimator;
    public target_object targetobj;

    public float timer;
    public float maxTimer;
    public float timerIncreaseRate;

    public GameObject HitParticle;

    public float damage;

    public float maxDistance = 10f;

    public bool startTimer;
    public bool canHit;
    public AudioSource hitSource;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Hit();
        }

        if (timer >= maxTimer)
        {
            AxeAnimator.SetBool("Hit", false);
            canHit = true;
            startTimer = false;
            timer = 0;
        }

        if (startTimer == true)
        {
            timer += timerIncreaseRate * Time.deltaTime;
            canHit = false;
        }
    }

    public void Hit()
    {
        if (canHit == true)
        {

            AxeAnimator.SetBool("Hit", true);
            startTimer = true;


            // Raycast

            RaycastHit hit;

            bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance);

            if (isHit)
            {
                {
                    Instantiate(HitParticle, hit.point, transform.rotation);
                    target_object target = hit.transform.GetComponent<target_object>();
                    hitSource.Play();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                    }
                }
            }

        }
    }
}
