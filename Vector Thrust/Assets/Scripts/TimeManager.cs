using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

    public GameObject PauseUI;
    public bool paused;
    public bool slowMo;
    public float slowdownFactor;
    public float slowdownLength;

    private void Start() {
        paused = false;
        slowMo = false;
    }

    private void Update() {
        if (Input.GetButtonDown("Pause")) {
            if (paused)
                Time.timeScale = 1;
            paused = !paused;
        }

        if (paused)
            Time.timeScale = 0;
        else {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        PauseUI.SetActive(paused);
    }

    public void DoSlowMotion() {
        Time.timeScale = slowdownFactor;
    }

    public void Resume() { paused = false; }

    public void Restart() { SceneManager.LoadScene(0); }

    public void MainMenu() { SceneManager.LoadScene(0); }

    public void Exit() { Application.Quit(); }
}
