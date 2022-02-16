using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizManager : MonoBehaviour
{
    // variable settings
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestions;

    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI ScoreTxt;
    int totalQuestion = 0;
    public int score;
    public int questionsAnswered;

    public GameObject quizPanel;
    public GameObject GameOverPanel;

    // counting question and generating steps
    private void Start()
    {
        totalQuestion = QnA.Count;
        GameOverPanel.SetActive(false);
        generateQuestions();
    }

    // restart the game function
    public void restart()
    {
        //SceneManager.LoadScene("Quiz");
    }

    // when game ends, lead players to game over scene
    void GameOver()
    {
        quizPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestion;
    }

    // when players answer the question correctly
    public void Correct()
    {
        score += 1;
        questionsAnswered += 1;
        QnA.RemoveAt(currentQuestions);
        generateQuestions();

    }

    // when players answers the question wrongly
    public void wrong()
    {
        questionsAnswered += 1;
        QnA.RemoveAt(currentQuestions);
        generateQuestions();
    }

    // setting of answers for questions 
    void SetAnswers()
    {
        for ( int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestions].Answers[i];

            if (QnA[currentQuestions].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }

        }
    }

    // loading to mainmenu with the back to main menu button 
    public void MainMenu()
    {
        //SceneManager.LoadScene("MainMenu");
    }

    // generating the questions 
    void generateQuestions()
    {
        if (QnA.Count > 0)
        {
        currentQuestions = Random.Range(0, QnA.Count);

        QuestionTxt.text = QnA[currentQuestions].Questions;

        SetAnswers();

        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
        
    }
}
