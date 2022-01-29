using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "RacoonGame/Event", order = 1)]
public class EventInfo : ScriptableObject
{
    public string eventKey;

    public Sprite backgroundStart;
    public Sprite backgroundAA; //One player / both players option A
    public Sprite backgroundBB; //One player / both players option B
    public Sprite backgroundAB; //Mixed
    public string option1;
    public string option2;
}