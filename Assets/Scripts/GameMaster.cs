using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : Singleton<GameMaster>
{
    public enum State { Menus, InGame, Paused, Other};
    private State _gameState;
    public State GameState { get { return _gameState;} }

    private Event currentEvent = null;

    private List<Racoon> racoons;

    #region UI
    [SerializeField]
    private GameObject mainMenuPanel;

    private void HideMainMenu () {
        mainMenuPanel.SetActive(false);
    }
    #endregion

    public GameObject racoonPrefab;

    private void Awake() {
        _gameState = State.Menus;
        racoons = new List<Racoon>();
    }

    public void AddRacoon(Racoon racoon) {
        racoons.Add(racoon);
    }

    public List<Racoon> Racoons() {
        return racoons;
    }

    public void SwitchState(State state) {
        _gameState = state;
    }


    public void SetCurrentEvent(Event newEvent) {
        currentEvent = newEvent;
    }

    public Event GetCurrentEvent() {
        return currentEvent;
    }

    public void SwitchDayNight() {
        foreach (Event ev in GameObject.FindObjectsOfType<Event>()) {
            ev.ShowConsequences();
        }
    }

    public void Quit() {
        // save any game data here
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    #region unused
    public void StartGame(int playerCount = 1) {
        Debug.Log("Start Game");
        //HideMainMenu();
        SpawnPlayers(playerCount);
    }

    private void SpawnPlayers(int playerCount = 1) {
        for (int i = 0; i < playerCount; i++) {
            Debug.Log("Spawning player");

            GameObject newRacoon = Instantiate(racoonPrefab, Vector3.zero, Quaternion.identity);
        }
    }
    #endregion
}
