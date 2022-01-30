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
    private bool isConsequenced = false;

    public enum EventOption { A, B };
    public enum EventConsequence { AA, BB, AB, NoneYet };
    private EventConsequence eventState;
    Dictionary <Racoon, EventOption> racoonChoices;

    GameObject originalBG;
    GameObject currentBG;

    private void Awake() {
        if (eventInfo.backgroundStart_day) {
            originalBG = Instantiate(eventInfo.backgroundStart_day, transform.position, Quaternion.identity, transform);
            currentBG = originalBG;
        }
        eventState = EventConsequence.NoneYet;

        racoonChoices = new Dictionary<Racoon, EventOption>();
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

    public void RacoonChoose(Racoon racoon, EventOption choice) {
        //If already in Dictionary, change value
        if (racoonChoices.ContainsKey(racoon)) {
            racoonChoices[racoon] = choice;
        } 
        //If not yet in Dictionary, add value
        else {
            racoonChoices.Add(racoon, choice);
        }
    }

    private void TriggerEvent() {
        isTriggered = true;
        Debug.Log("TriggerEvent");
        //TODO create popup
        UIManager.Instance.CreatePopup(eventInfo.option1, eventInfo.option2);
        GameMaster.Instance.SetCurrentEvent(this);
    }

    public void ShowConsequences() {
        isConsequenced = true;

        GameObject prefab = null;
        if (eventState == EventConsequence.AA) {
            prefab = eventInfo.backgroundAA_night;
        } else if (eventState == EventConsequence.BB) {
            prefab = eventInfo.backgroundBB_night;
        } else if (eventState == EventConsequence.AB) {
            prefab = eventInfo.backgroundAB_night;
        }

        if (prefab) {
            originalBG.SetActive(false);
            currentBG = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        }
    }
}
