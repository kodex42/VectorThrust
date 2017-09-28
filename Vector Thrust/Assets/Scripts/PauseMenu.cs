using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private bool paused;
    public static bool isPaused;
    public GameObject PauseUI;

    private void Start() {
        paused = false;
    }

    private void Update() {
        if (Input.GetButtonDown("Pause"))
            paused = !paused;

        PauseUI.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
        PauseMenu.isPaused = paused;
    }

    public void Resume() { paused = false; }

    public void Restart() { SceneManager.LoadScene(0); }

    public void MainMenu() { SceneManager.LoadScene(0); }

    public void Exit() { Application.Quit(); }
}
