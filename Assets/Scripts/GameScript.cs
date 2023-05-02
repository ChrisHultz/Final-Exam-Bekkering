using System;
using System.IO;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {
    //Modifying text on awake
    public Text playerTxt;
    public Text livesTxt;
    public Text pointsTxt;

    //setting array size
    public int num_scores = 10;

    //Setting points
    private int points;
    public static int pointsPassed = 0;

    //Timer Stuff
    public Text remainingTime;
    public float timeCountdown;

    private bool isFinished = false;



    public void Awake() {
        playerTxt.text = IntroScript.pass;
        livesTxt.text = IntroScript.PassLives.ToString();
        pointsTxt.text = pointsPassed.ToString();
        timeCountdown = IntroScript.inputtedTime;
    }

    public void Update() {
        timeCountdown -= Time.deltaTime;

        if (timeCountdown >= 0) {
            remainingTime.text = timeCountdown.ToString("F2") + " seconds";
        }
        else { 
            if (isFinished == false) {
                Invoke("GameOver", 0f);
                isFinished = true;
            }
        }
    }

    public void GameOver() {
        string path = "Assets/Misc/Scores.txt";
        string line;
        string[] fields;
        int scores_written = 0;
        string newName = IntroScript.pass;
        string newScore = pointsPassed.ToString();
        string[] writeNames = new string[10];
        string[] writeScores = new string[10];
        bool newScoreWritten = false;
        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream ) {
            line = reader.ReadLine();
            fields = line.Split(',');
			if (!newScoreWritten && scores_written < num_scores) {
				if(Convert.ToInt32(newScore) > Convert.ToInt32(fields[1])) {
					writeNames[scores_written] = newName;
                    writeScores[scores_written] = newScore;
                    newScoreWritten = true;
                    scores_written += 1;
				}
			}
			if(scores_written < num_scores) { // we have not written enough lines yet
                writeNames[scores_written] = fields[0];
                writeScores[scores_written] = fields[1];
                scores_written += 1;
			}
		}
        reader.Close();
        StreamWriter writer = new StreamWriter(path);
		for(int x = 0; x < scores_written; x++) {
			writer.WriteLine(writeNames[x] + ',' + writeScores[x]);
		}
		writer.Close();
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("scores");
        Invoke("ToExit", 0.5f);
    }

    void ToExit() {
        SceneManager.LoadScene("3Exit");
    }

    public void NextButton() {
        Invoke("GameOver", 0f);
    }

    
    public void addPoints() {
        points++;
        pointsPassed = points;
        pointsTxt.text = pointsPassed.ToString();
        Debug.Log(points);
    }

    public void decreasePoints() {
        if (points <= 0) 
            Debug.Log("Can't go below 0!");
        else
            points--;

        pointsPassed = points;
        pointsTxt.text = pointsPassed.ToString();
        Debug.Log(points);
    }

    public void addLives() {
        IntroScript.PassLives++;
        Debug.Log(IntroScript.PassLives);
        livesTxt.text = IntroScript.PassLives.ToString();
    }

    public void decreaseLives() {
        if (IntroScript.PassLives <= 1)
            Debug.Log("Can't go below 1!");
        else
            IntroScript.PassLives--;
        livesTxt.text = IntroScript.PassLives.ToString();
        Debug.Log(IntroScript.PassLives);
    }

    public void GamerOver() {
        string path = "Assets/Misc/scores.txt";
        string line;
        string[] fields;
        int scores_written = 0;
        string newName = IntroScript.pass;
        string newScore = pointsPassed.ToString();
        string[] writeNames = new string[10];
        string[] writeScores = new string[10];
        bool newScoreWritten = false;
		StreamReader reader = new StreamReader(path);
		    while (!reader.EndOfStream ) {
                line = reader.ReadLine();
            	fields = line.Split(',');
				if (!newScoreWritten && scores_written < num_scores) {
					if(Convert.ToInt32(newScore) > Convert.ToInt32(fields[1])) {
						writeNames[scores_written] = newName;
                    	writeScores[scores_written] = newScore;
                    	newScoreWritten = true;
                    	scores_written += 1;
					}
				}
				if(scores_written < num_scores) { // we have not written enough lines yet
                	writeNames[scores_written] = fields[0];
                	writeScores[scores_written] = fields[1];
                	scores_written += 1;
				}
			}
		reader.Close();
		StreamWriter writer = new StreamWriter(path);
		for(int x = 0; x < scores_written; x++) {
			writer.WriteLine(writeNames[x] + ',' + writeScores[x]);
		}
		writer.Close();
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("scores");

    }
}
