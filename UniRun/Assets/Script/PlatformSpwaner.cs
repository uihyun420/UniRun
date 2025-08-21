using Unity.Properties;
using UnityEngine;

public class PlatformSpwaner : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public int poolSize = 10;
    private GameObject[] platforms;

    private int currentIndex = 0;
    private float interval;
    private float timer = 0;

    public float yMin = -1f;
    public float yMax = 1f;


    public float intervalMin = 1.5f;
    public float intervalMax = 2f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        interval = Random.Range(intervalMin, intervalMax);
        Spawn();
    }
    private void Awake()
    {
        platforms = new GameObject[poolSize];
        for(int i = 0; i< platforms.Length; i++)
        {
            platforms[i] = Instantiate(PlatformPrefab);
            platforms[i].SetActive(false);
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(interval < timer)
        {
            timer = 0f;
            var position = transform.position;
            position.y = Random.Range(yMin, yMax);
            platforms[currentIndex].transform.position = position;
            platforms[currentIndex].SetActive(true);
            currentIndex = (currentIndex + 1) % platforms.Length;
            interval = Random.Range(intervalMin, intervalMax);
        }
        
    }


    private void Spawn()
    {
        // 발판을 하나 생성하여 시작 위치에 배치
        var position = transform.position;
        position.y = Random.Range(yMin, yMax);
        platforms[currentIndex].transform.position = position;
        platforms[currentIndex].SetActive(true);
        currentIndex = (currentIndex + 1) % platforms.Length;
    }
}
