using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOVer { get; private set; }

    private int score;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverUi;
   

    public void Awake()
    {
        score = 0;
        isGameOVer = false;
        gameOverUi.SetActive(false);
    }

    private void Update()
    {
        if (isGameOVer && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddScore(int add)
    {
        if(!isGameOVer)
        {
            score += add;
            scoreText.text = $"Score: {score}";
        }
    }
    public void OnPlayerDead()
    {
        isGameOVer = true;
        gameOverUi.SetActive(true);
    }
}
