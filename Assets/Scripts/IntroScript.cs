using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {
    //Variables for lives
    private int lives;
    public static int PassLives = 1;

    //Music
    public Toggle musicToggle;
    public Text musicText;

    //Timer Slider
    public Slider timeSlider;
    
    public Text timeText;
    private float tracked;
    public static float inputtedTime = 200.0f;

    //Variables for name
    public Text NameTxt;
    public InputField input;
    public static string pass = "Player";

    //Dropdown
    public Dropdown LivesDropDown;

    public void Awake() {
        timeText.text = "Time: " + timeSlider.value.ToString("F2") + " seconds";
    }

    //Lives Dropdown
    public void ChooseLives() {
        switch (LivesDropDown.value) {
            case 1:
                lives = 1;
                break;
            case 2:
                lives = 2;
                break;
            case 3:
                lives = 3;
                break;
            case 4:
                lives = 4;
                break;
            case 5:
                lives = 5;
                break;
            case 6:
                lives = 6;
                break;
            case 7:
                lives = 7;
                break;
            case 8:
                lives = 8;
                break;
            case 9:
                lives = 9;
                break;
        }
        PassLives = lives;
        Debug.Log(PassLives);
    }

    //Button for next scene
    public void NextScene() {
        SceneManager.LoadScene("2Game");
    }

    //Name Input
    public void inputName() {
        pass = input.text.ToString();
        NameTxt.text = pass.ToUpper();
    }

    //Music Toggle
    public void ToggleMusic() {
    if (Music.instance != null) {
        AudioSource audioSource = Music.instance.GetComponent<AudioSource>();
        audioSource.mute = !audioSource.mute;
        musicText.text = audioSource.mute ? "Off" : "On";
        }
    }

    //Timer
    public void TimerAdjust() {
        timeText.text = timeSlider.value.ToString("F2") + " seconds";
        tracked = timeSlider.value;
        inputtedTime = tracked;
    }
}
