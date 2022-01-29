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

    void Awake()
    {
        popupPanel.SetActive(false);
    }

    public void CreatePopup (string option1, string option2) {
        popupPanel.SetActive(true);

        option1_text.text = option1;
        option2_text.text = option2;
    }
}
