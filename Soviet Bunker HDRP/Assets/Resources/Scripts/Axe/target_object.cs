using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_object : MonoBehaviour
{
    public float Health = 100f;
    public float MaxHealth = 100f;
    public build_raycast build;
    public GameObject deadTree, aliveTree;

    public float timer;
    public float maxTimer;
    public float timerIncreaseRate;
    public bool startTimer;
    public GameObject imUnder;
    void Start()
    {
        Health = MaxHealth;
        build = FindObjectOfType<build_raycast>();
    }

    private void Update() {
        if (timer >= maxTimer)
        {
            Destroy(imUnder);
            startTimer = false;
            timer = 0;
        }

        if (startTimer == true)
        {
            timer += timerIncreaseRate * Time.deltaTime;
        }
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            deadTree.SetActive(true);
            aliveTree.SetActive(false);
            startTimer = true;
            build.ObjectAmount++;
        }
    }

    float CalculateHealth()
    {
        return Health / MaxHealth;
    }
}
