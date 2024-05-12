using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSequence : MonoBehaviour
{
    [SerializeField] RawImage player1View;
    [SerializeField] RawImage player2View;
    [SerializeField] Text gameOverText;
    [SerializeField] Button exitButton;

    bool gameTerminatedOnce = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TerminateGame(bool player1Win)
    {
        if (!gameTerminatedOnce)
        {
            gameTerminatedOnce = true;

            StartCoroutine(GameOverSeq(player1Win));
        }
    }

    IEnumerator GameOverSeq(bool player1Win)
    {
        ColorManager colorManager = ColorManager.instance;
        if(player1Win)
        {
            colorManager.layer0Red = true;
            colorManager.layer0Green = true;
            colorManager.layer0Blue = true;
        }
        else
        {
            colorManager.layer0Red = false;
            colorManager.layer0Green = false;
            colorManager.layer0Blue = false;
        }
        colorManager.onColorChanged.Invoke();

        // Killing all enemies and disabling spawning
        var enemySpawners = FindObjectsOfType<EnemySpawn>();
        enemySpawners[0].keepSpawning = false;
        enemySpawners[1].keepSpawning = false;

        var enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            enemy.Die();
        }

        // Fading the loser player's view
        RawImage fadingImg = player1Win ? player2View : player1View;
        RawImage growingImg = player1Win ? player1View : player2View;
        for (float t = 5; t > 0; t -= Time.unscaledDeltaTime)
        {
            fadingImg.color = new Color(t / 5, t / 5, t / 5, t / 5);

            if(t < 3)
            {
                Vector3 imgPos = growingImg.rectTransform.localPosition;
                growingImg.rectTransform.localPosition *= 1 - (Time.unscaledDeltaTime * 10);

                growingImg.transform.localScale = Vector3.one * ((6 - t) / 3);
            }

            yield return null;
        }

        Time.timeScale = 0;

        string winnerName = player1Win ? new PlayerNames().GetNames().Item1 : new PlayerNames().GetNames().Item2;
        gameOverText.text = winnerName.ToUpper() + " IS THE POSSESSOR OF ALL THE COLORS FROM NOW ON!";
        for(float t = 0; t < 1; t += Time.unscaledDeltaTime)
        {
            gameOverText.color = new Color(0, 0, 0, t);
            yield return null;
        }

        exitButton.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
