using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float x = -20f;
    public GameObject[] obstacles;
    private GameManager gameManager;

    private bool stepped = false;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        stepped = false;

        foreach (var obstacle in obstacles)
        {
            obstacle.SetActive(Random.value < 0.3); // 30���� Ȯ���� Ȱ��ȭ  

        }
        //Destroy(gameObject, 10f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!stepped && collision.collider.CompareTag("Player"))
        {
            stepped = true;
            gameManager.AddScore(10);
        }
    }


    private void Update()
    {
        if (transform.position.x < x)
        {
            gameObject.SetActive(false);
        }
    }
}
