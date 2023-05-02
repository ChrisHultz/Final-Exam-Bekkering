using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {
    public Text pauseText;
    public Toggle musicToggle;
    public Text musicText;
    public GameObject objectToEnable;
    public GameObject objectToDisable1;
    public GameObject objectToDisable2;
    public GameScript gameScript;
    private bool isPaused = false;

    public void Awake() {
        if (Music.instance != null) {
            AudioSource audioSource = Music.instance.GetComponent<AudioSource>();
            musicText.text = audioSource.mute ? "Off" : "On";
            
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused == false) {
                objectToEnable.SetActive(true);
                Pause();
            }
            else {
                objectToEnable.SetActive(false);
                ResumeGame();
            }
        }
    }

    public void ContinueBtn() {
        objectToEnable.SetActive(false);
        ResumeGame();
    }

    public void NewGame() {
        IntroScript.inputtedTime = 200.0f;
        IntroScript.PassLives = 1;
        GameScript.pointsPassed = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("2Game");
    }

    public void ToggleMusic() {
        if (Music.instance != null) {
            AudioSource audioSource = Music.instance.GetComponent<AudioSource>();
            audioSource.mute = !audioSource.mute;
            musicText.text = audioSource.mute ? "Off" : "On";
        }
    }

    void Pause() {
        Time.timeScale = 0;
        isPaused = true;
        gameScript.enabled = false; // Disable the GameScript
    }
    public void ResumeGame() {
        Time.timeScale = 1;
        isPaused = false;
        gameScript.enabled = true; // Enable the GameScript
    }

    //Attempt at save script, didn't do this on practice. Apologies if not finished.

    [Serializable]
    public class TestGameData {
        public int lives;
        public int points;
        public float timeRemaining;
    }

    public void SaveData() {
        TestGameData testSaveData = new TestGameData();
        testSaveData.lives = IntroScript.PassLives;
        testSaveData.points = GameScript.pointsPassed; 
        testSaveData.timeRemaining = gameScript.timeCountdown;

        string json = JsonUtility.ToJson(testSaveData);
        PlayerPrefs.SetString("TestGameData", json);
        PlayerPrefs.Save();
    }

    public void LoadData() {
        if(PlayerPrefs.HasKey("TestGameData")) {
            string json = PlayerPrefs.GetString("TestGameData");
            TestGameData testSaveData = JsonUtility.FromJson<TestGameData>(json);
            IntroScript.PassLives = testSaveData.lives;
            GameScript.pointsPassed = testSaveData.points;
            gameScript.timeCountdown = testSaveData.timeRemaining;
        }
    }

    public void SaveJson() {
        TestGameData testSaveData = new TestGameData();
        testSaveData.lives = IntroScript.PassLives;
        testSaveData.points = GameScript.pointsPassed;
        testSaveData.timeRemaining = gameScript.timeCountdown;

        string json = JsonUtility.ToJson(testSaveData);
        Debug.Log("Saving as JSON: " + json);
    }

}
