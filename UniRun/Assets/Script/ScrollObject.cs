using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed = 10f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(gameManager.isGameOVer)
        {
            return; // 게임 오버 상태에서는 스크롤을 멈춤
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}
