using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperModeSwitch : MonoBehaviour
{
    public build_raycast build;
    public axe_system axe;

    public bool DeveloperModeOn;
    public bool StartDeveloperMode;
    public bool settings;

    public bool D;
    public bool E;
    public bool V;
    public bool O;
    public bool N;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartDeveloperMode = !StartDeveloperMode;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settings = !settings;
        }

        if (settings)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (!settings) 
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (StartDeveloperMode == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                D = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                E = true;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                V = true;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                O = true;
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                N = true;
            }            
        }

        if (StartDeveloperMode == false)
        {
            D = false;
            E = false;
            V = false;
            O = false;
            N = false;
            DeveloperModeOn = false;
        }

        CheckDev();
        DeveloperMode();
    }

    void CheckDev()
    {
        if (D && E && V && O && N)
        {
            DeveloperModeOn = true;
        }
    }

    void DeveloperMode()
    {
        if (DeveloperModeOn == true)
        {
            build.timerIncreaseRate = 150f;
            axe.damage = 100f;        
        }
    }
}
