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
    private bool isChoiceMade = false;
    private bool isConsequenced = false;

    public enum EventOption { A, B };
    public enum EventConsequence { AA, BB, AB, NoneYet };
    private EventConsequence eventState;
    Dictionary <Racoon, EventOption> racoonChoices;
    Dictionary <Racoon, bool> racoonConfirmed;

    GameObject originalBG;
    GameObject currentBG;

    [SerializeField]
    AudioSource ambientAudio;

    [SerializeField]
    AudioSource effectAudio;

    private void Awake() {
        if (eventInfo.backgroundStart_day) {
            originalBG = Instantiate(eventInfo.backgroundStart_day, transform.position, Quaternion.identity, transform);
            currentBG = originalBG;
        }

        //Remove placeholder
        if (GetComponent<SpriteRenderer>()) {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        eventState = EventConsequence.NoneYet;

        racoonChoices = new Dictionary<Racoon, EventOption>();
        racoonConfirmed = new Dictionary<Racoon, bool>();


        if (eventInfo.ambient_Start) {
            ambientAudio.clip = eventInfo.ambient_Start;
            ambientAudio.Play();
        }
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
        Debug.Log(choice);
        if (!racoonConfirmed.ContainsKey(racoon)) {
            //If already in Dictionary, change value
            if (racoonChoices.ContainsKey(racoon)) {
                racoonChoices[racoon] = choice;
            } 
            //If not yet in Dictionary, add value
            else {
                racoonChoices.Add(racoon, choice);
            }
        }
    }

    public void RacoonConfirm(Racoon racoon) {
        //Set confirmed to true.
        if (!racoonConfirmed.ContainsKey(racoon)) {
            racoonConfirmed.Add(racoon, true);
        } 

        if (EventReady()) {
            int A_votes = 0;
            int B_votes = 0;

            foreach (EventOption choice in racoonChoices.Values) {
                if (choice == EventOption.A) {
                    A_votes++;
                } else if (choice == EventOption.B) {
                    B_votes++;
                }
            }

            //2 players
            if (A_votes == 1 && B_votes == 1) {
                eventState = EventConsequence.AB;
            } else if (A_votes == 2) {
                eventState = EventConsequence.AA;
            } else if (B_votes == 2) {
                eventState = EventConsequence.BB;
            } 
            // 1 player
            else if (A_votes == 1) {
                eventState = EventConsequence.AA;
            } else if (B_votes == 1) {
                eventState = EventConsequence.BB;
            }

            ShowImmediateEffect();
        }
    }

    public bool EventReady() {
        int racoonCount = GameMaster.Instance.Racoons().Count;
        int voteCount = racoonConfirmed.Count;

        if (voteCount >= racoonCount) {
            return true;
        }
        return false;
    }

    private void TriggerEvent() {
        isTriggered = true;
        Debug.Log("TriggerEvent");
        //TODO create popup
        UIManager.Instance.CreatePopup(eventInfo.option1, eventInfo.option2);
        GameMaster.Instance.SetCurrentEvent(this);
    }

    private void ShowImmediateEffect() {
        Debug.Log("ShowImmediateEffect");
        if (!isChoiceMade) {
            isChoiceMade = true;

            GameObject prefab = null;
            if (eventState == EventConsequence.AA) {
                prefab = eventInfo.backgroundAA_day;
            } else if (eventState == EventConsequence.BB) {
                prefab = eventInfo.backgroundBB_day;
            } else if (eventState == EventConsequence.AB) {
                prefab = eventInfo.backgroundAB_day;
            }

            if (prefab) {
                if (originalBG.activeSelf) {
                    originalBG.SetActive(false);
                }
                
                currentBG = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            }

            //Trigger Sounds
            if (eventState == EventConsequence.AA) {
                effectAudio.clip = eventInfo.sound_effect_AA;
                ambientAudio.clip = eventInfo.ambient_AA_day;
            } else if (eventState == EventConsequence.BB) {
                effectAudio.clip = eventInfo.sound_effect_BB;
                ambientAudio.clip = eventInfo.ambient_BB_day;
            } else if (eventState == EventConsequence.AB) {
                effectAudio.clip = eventInfo.sound_effect_AB;
                ambientAudio.clip = eventInfo.ambient_AB_day;
            }
            effectAudio.Play();
            ambientAudio.Play();

            UIManager.Instance.CloseEventPopup();
        }
    }

    public void ShowConsequences() {
        if (!isConsequenced){
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
                if (originalBG.activeSelf) {
                    originalBG.SetActive(false);
                }

                currentBG = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            }

            //Trigger Sounds
            if (eventState == EventConsequence.AA) {
                ambientAudio.clip = eventInfo.ambient_AA_night;
            } else if (eventState == EventConsequence.BB) {
                ambientAudio.clip = eventInfo.ambient_BB_night;
            } else if (eventState == EventConsequence.AB) {
                ambientAudio.clip = eventInfo.ambient_AB_night;
            }
            ambientAudio.Play();
        }
    }
}
