using System;
using System.IO;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {
    public Text RankTxt;
    public Text ScoreTxt;
    public Text NameTxt;
    public int num_scores = 10;

    public void Awake() {
        string path = "Assets/Misc/Scores.txt";
        string line;
        string[] fields;
        string[] playerNames = new string[num_scores];
        int[] playerScores = new int[num_scores];
        int scores_read = 0;

        ScoreTxt.text = "";
        NameTxt.text = "";
        RankTxt.text = "";
        StreamReader reader = new StreamReader(path);
        int rank = 1;
        if (reader.Peek() == -1) {
            // Scores.txt file is empty
            RankTxt.text = "None";
            ScoreTxt.text = "None";
            NameTxt.text = "None";
        }
        else {
            while (!reader.EndOfStream && scores_read < num_scores) {
                line = reader.ReadLine();
                fields = line.Split(',');
                NameTxt.text += fields[0] + "\n";
                ScoreTxt.text += fields[1] + "\n";
                RankTxt.text += "#"+ rank.ToString() + "\n";
                scores_read += 1;
                rank += 1;

            }
        }
    }

    public void ResetScores() {
        string path = "Assets/Misc/Scores.txt";
        string defaultScoreLine = "AAA,0\n";
        StreamWriter writer = new StreamWriter(path);
        for(int i = 0; i < num_scores; i++) {
            writer.Write(defaultScoreLine);
        }
        writer.Close();
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("scores");
        SceneManager.LoadScene("3Exit");
    }

    public void ReplayGame() {
        GameScript.pointsPassed = 0;
        IntroScript.PassLives = 1;
        IntroScript.inputtedTime = 200.0f;
        SceneManager.LoadScene("2Game");
    }

    public void QuitGame() {
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("I quit...");
    }
}
