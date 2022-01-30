using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSwitch : MonoBehaviour
{
    [SerializeField]
    float triggerDistance = 2f;

    bool isTriggered = false;

    private void Awake() {
    }

    private void FixedUpdate() {
        if (isTriggered)
            return;

        Vector3 myPosition = transform.position;

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            Vector3 playerPosition = player.transform.position;

            float distance = Vector3.Distance(myPosition, playerPosition);

            if (distance < triggerDistance && !isTriggered) {
                isTriggered = true;
                GameMaster.Instance.SwitchDayNight();
            }
        }
    }
}
