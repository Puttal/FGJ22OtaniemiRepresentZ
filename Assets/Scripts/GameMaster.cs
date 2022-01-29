using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : Singleton<GameMaster>
{
    public enum State { Menus, InGame, Paused, Other};
    private State _gameState;
    public State GameState { get { return _gameState;} }

    #region UI
    private GameObject mainMenuPanel;

    private void HideMainMenu () {
        mainMenuPanel.SetActive(false);
    }
    #endregion

    public GameObject racoonPrefab;

    private void Awake() {
        _gameState = State.Menus;
    }

    public void SwitchState(State state) {
        _gameState = state;
    }

    public void StartGame(int playerCount = 1) {
        Debug.Log("Start Game");
        HideMainMenu();
        SpawnPlayers(playerCount);
    }

    private void SpawnPlayers(int playerCount = 1) {
        for (int i = 0; i < playerCount; i++) {
            Debug.Log("Spawning player");

            GameObject newRacoon = Instantiate(racoonPrefab, Vector3.zero, Quaternion.identity);
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
}
