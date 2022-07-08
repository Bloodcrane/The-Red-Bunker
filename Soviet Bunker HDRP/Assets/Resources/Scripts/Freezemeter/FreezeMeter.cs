using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FreezeMeter : MonoBehaviour
{
    // Ｖａｒｉａｂｌｅｓ

    [Header("Floats")]
    public float maxFreeze;
    public float minFreeze;
    public float freezeIncreaseRate;
    public float freezeDecreaseRate;
    public float freeze;

    [Header("Bools")]
    public bool warmTrigger;

    [Header("UI Elements")]
    public Slider frostSlider;
    [Header("Strings")]
    public string Scene;


    // Ｆｕｎｃｔｉｏｎｓ
    public void Start()
    {

    }

    public void Update()
    {
        FreezeSettings();
        FreezeSlider();
    }

    public void FreezeSettings()
    {
        // Ｉｎｃｒｅａｓｅ　Ｆｒｅｅｚｅ

        if (warmTrigger == false)
        {
            if (freeze < maxFreeze)
            {
                freeze += freezeIncreaseRate * Time.deltaTime;
            }
        }

        // Ｉｆ　Ｇｒｅａｔｅｒ　Ｔｈａｎ　Ｍｉｎ　Ａｍｏｕｎｔ

        if (freeze < minFreeze)
        {
            freeze = 0;
        }


        // Ｉｆ　Ｇｒｅａｔｅｒ　Ｔｈａｎ　Ｍａｘ　Ａｍｏｕｎｔ

        if (freeze >= maxFreeze)
        {
            FreezeToDeath();
        }
    }

    public void FreezeSlider()
    {
        frostSlider.value = freeze;
    }

    public void FreezeToDeath()
    {
        print("Died of frostbite!");
        SceneManager.LoadScene(Scene);
    }
}
