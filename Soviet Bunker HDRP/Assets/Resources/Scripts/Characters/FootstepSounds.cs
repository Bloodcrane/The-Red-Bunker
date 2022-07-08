using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource WalkAudio, RunAudio;

    [Header("Audio Clips")]

    public AudioClip Walk1, Run1;

    [Header("First Person Controller")]

    public FirstPersonController firstPersonController;

    [Header("Bools")]

    public bool WalkSwitch;

    private void Start()
    {
        WalkSwitch = true;

        firstPersonController = GetComponent<FirstPersonController>();
    }
    void Update()
    {
        WalkFootstep();
        RunFootstep();

        // Ground Check
        if (firstPersonController.cc.isGrounded == false)
        {
            WalkAudio.Stop();
            RunAudio.Stop();
        }
    }

    void WalkFootstep()
    {
        if(WalkSwitch == true)
        {
            if (!WalkAudio.isPlaying && firstPersonController.Walking == true && firstPersonController.Running == false)
            {
                WalkAudio.clip = Walk1;
                WalkAudio.Play();
                return;
            }
            if (WalkAudio.isPlaying && firstPersonController.Walking == false)
            {
                WalkAudio.Stop();
            }
        }
        else
        {
            WalkAudio.Stop();
        }
    }

    void RunFootstep()
    {
        if (!RunAudio.isPlaying && firstPersonController.Running == true)
        {
            RunAudio.clip = Run1;
            RunAudio.Play();
            WalkSwitch = false;
            return;
        }
        if (RunAudio.isPlaying && firstPersonController.Running == false)
        {
            RunAudio.Stop();
            WalkSwitch = true;
        }
    }
}
