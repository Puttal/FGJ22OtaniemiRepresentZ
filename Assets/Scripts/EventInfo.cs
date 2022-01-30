using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "RacoonGame/Event", order = 1)]
public class EventInfo : ScriptableObject
{
    //Identity key
    public string eventKey;

    //Visuals (prefab GameObjects instead? as allows animation more easily)
    public GameObject backgroundStart_day;
    
    public GameObject backgroundAA_day; //One player / both players option A
    public GameObject backgroundBB_day; //One player / both players option B
    public GameObject backgroundAB_day; //Mixed

    public GameObject backgroundAA_night; //One player / both players option A
    public GameObject backgroundBB_night; //One player / both players option B
    public GameObject backgroundAB_night; //Mixed


    //Option texts
    public string option1;
    public string option2;

    //Sounds
    public AudioClip ambient_Start;
    public AudioClip ambient_AA_day;
    public AudioClip ambient_BB_day;
    public AudioClip ambient_AB_day;
    public AudioClip ambient_AA_night;
    public AudioClip ambient_BB_night;
    public AudioClip ambient_AB_night;

    //Option pressed / activated
    public AudioClip sound_option1;
    public AudioClip sound_option2;
}