using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getwarm : MonoBehaviour
{
     // Ｖａｒｉａｂｌｅｓ

    [Header("Floats")]
    public float radius = 5f;

    [Header("Bools")]
    public bool inTrigger;

    [Header("Classes")]
    public FreezeMeter frostMeter;
    public string GameObjectToFind;

    // Ｆｕｎｃｔｉｏｎｓ

    private void Awake() {
        frostMeter = GameObject.Find(GameObjectToFind).GetComponent<FreezeMeter>();
    }

    private void Update() {
        TriggerWarmUp();
    }

    private void TriggerWarmUp()
    {
        if (inTrigger == true)
        {
            decreaseFrost();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.yellow;
    }

    private void OnTriggerEnter(Collider other) {
        other.tag = "Player";
        frostMeter.warmTrigger = true;
        inTrigger = true;
    }

    private void OnTriggerExit(Collider other) {
        other.tag = "Player";
        frostMeter.warmTrigger = false;
        inTrigger = false;
    }

    public void decreaseFrost()
    {
        frostMeter.freeze -= frostMeter.freezeDecreaseRate;
    }
}
