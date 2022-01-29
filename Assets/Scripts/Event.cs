using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    [SerializeField]
    EventInfo eventInfo;

    [SerializeField]
    float triggerDistance = 1f;

    private bool isTriggered = false;

    private void Awake() {

    }

    private void FixedUpdate() {
        if (isTriggered)
            return;

        Vector3 myPosition = transform.position;

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            Vector3 playerPosition = player.transform.position;

            float distance = Vector3.Distance(myPosition, playerPosition);

            if (distance < triggerDistance) {
                TriggerEvent();
            }
        }
    }

    private void TriggerEvent() {
        isTriggered = true;
        Debug.Log("TriggerEvent");
        //TODO create popup
        UIManager.Instance.CreatePopup(eventInfo.option1, eventInfo.option2);
    }
}
