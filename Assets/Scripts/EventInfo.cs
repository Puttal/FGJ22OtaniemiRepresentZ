using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "RacoonGame/Event", order = 1)]
public class EventInfo : ScriptableObject
{
    //Identity key
    public string eventKey;

    //Visuals (prefab GameObjects instead? as allows animation more easily)
    public Sprite backgroundStart_day;
    
    public Sprite backgroundAA_day; //One player / both players option A
    public Sprite backgroundBB_day; //One player / both players option B
    public Sprite backgroundAB_day; //Mixed

    public Sprite backgroundAA_night; //One player / both players option A
    public Sprite backgroundBB_night; //One player / both players option B
    public Sprite backgroundAB_night; //Mixed


    //Option texts
    public string option1;
    public string option2;

    //Sounds
    //Ambient_Start
    //Ambient_AA_day
    //Ambient_BB_day
    //Ambient_AB_day
    //Ambient_AA_night
    //Ambient_BB_night
    //Ambient_AB_night

    //SoundOption1
    //SoundOption2
}