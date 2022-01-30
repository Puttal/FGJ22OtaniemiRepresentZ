using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    GameObject popupPanel;

    [SerializeField]
    Text option1_text;

    [SerializeField]
    Text option2_text;

    [SerializeField]
    GameObject vote_A_1;

    [SerializeField]
    GameObject vote_A_2;

    [SerializeField]
    GameObject vote_B_1;

    [SerializeField]
    GameObject vote_B_2;

    void Awake()
    {
        vote_A_1.SetActive(false);
        vote_A_2.SetActive(false);
        vote_B_1.SetActive(false);
        vote_B_2.SetActive(false);

        popupPanel.SetActive(false);
    }

    public void CreatePopup (string option1, string option2) {
        popupPanel.SetActive(true);

        option1_text.text = option1;
        option2_text.text = option2;
    }

    public void CloseEventPopup()  {
        popupPanel.SetActive(false);
    }

    public void VisualizeVote(int racoonNumber, Event.EventOption option) {
        vote_A_1.SetActive(false);
        vote_A_2.SetActive(false);
        vote_B_1.SetActive(false);
        vote_B_2.SetActive(false);

        if (racoonNumber == 0) {
            vote_A_1.SetActive(false);
            vote_B_1.SetActive(false);

            if (option == Event.EventOption.A) {
                vote_A_1.SetActive(true);
            } else {
                vote_B_1.SetActive(true);
            }
        } else if (racoonNumber == 1) {
            vote_A_2.SetActive(false);
            vote_B_2.SetActive(false);

            if (option == Event.EventOption.A) {
                vote_A_2.SetActive(true);
            } else {
                vote_B_2.SetActive(true);
            }
        }
    } 
}
