using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class build_raycast : MonoBehaviour
{
    // Ｖａｒｉａｂｌｅｓ

    [Header("Transforms & Objects")]
    public Transform BlueprintTransform;
    public GameObject ObjectPrefab;
    public GameObject BlueprintObject;
    public GameObject TextUI;

    [Header("Bools")]
    public bool canPlace;
    public bool canBuild;
    public bool buildingEnabled;

    [Header("Floats")]
    public float timer;
    public float maxTimer; 
    // // // // // // // // // // // // // // // //
    [Tooltip("How many objects can player place.")]
    public float ObjectAmount;
    public float timerIncreaseRate;

    [Header("UI Elements")]
    public Slider buildSlider; 
    public TMP_Text StatusText;
    [Header("Colors")]
    public Color lightGreen;
    public Color lightRed;
    
    [Header("Sound Effects")]
    public AudioSource PlaceAudioSource;
    [Header("Animators")]
    public Animator AxeAnimator;
    [Header("Scripts")]
    public axe_system axe;


    // Ｆｕｎｃｔｉｏｎｓ
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            buildingEnabled = !buildingEnabled;
        }

        if (buildingEnabled) 
        {
            if (canBuild == true)
            {
                if (canPlace == true)
                {
                    BlueprintObject.SetActive(true);
                    TextUI.SetActive(true);
                    BuildingSystem();
                }



                if (timer >= maxTimer)
                {
                    canPlace = true;
                    timer = 0;
                }                   
            }            
        }


        if (canPlace == false)
        {
            TextUI.SetActive(false);
            timer += timerIncreaseRate * Time.deltaTime;
        }     

        if (ObjectAmount == 0)
        {
            canBuild = false;
            AxeAnimator.SetBool("PickUp", true);
            axe.enabled = true;
        }

        if (ObjectAmount == 1 && ObjectAmount >= 1)
        {
            canBuild = true;
            AxeAnimator.SetBool("PickUp", false);
        }

        if (buildingEnabled == false)
        {
            BlueprintObject.SetActive(false);
            TextUI.SetActive(false);
            StatusText.text = "Off";
            StatusText.color = lightRed;
        }
        if (buildingEnabled == true)
        {
            StatusText.text = "On";
            StatusText.color = lightGreen;
        }

        buildSlider.value = timer;
    }

    public void BuildingSystem()
    {
        BuildRaycast();
        RotateSystem();

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlaceObject();
        }        
    }

    public void PlaceObject()
    {
            Instantiate(ObjectPrefab, BlueprintTransform.transform.position, BlueprintTransform.transform.rotation);
            ObjectAmount--;
            BlueprintObject.SetActive(false);
            canPlace = false;

            // Ｐｌａｙ　Ｓｏｕｎｄ

            PlaceAudioSource.Play();
    }

    public void BuildRaycast()
    {
        Ray ray = new Ray (transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast (ray, out hitInfo))
        {
            BlueprintTransform.position = hitInfo.point;
        }        
    }

    public void RotateSystem()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            BlueprintTransform.transform.Rotate(Vector3.up * 2f, Space.Self);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            BlueprintTransform.transform.Rotate(Vector3.down * 2f, Space.Self);
        }        
    }
}
