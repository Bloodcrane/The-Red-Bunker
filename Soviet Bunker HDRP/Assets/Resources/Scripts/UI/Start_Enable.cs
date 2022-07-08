using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Enable : MonoBehaviour
{
    public GameObject[] objects;
    void Start()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }
}
