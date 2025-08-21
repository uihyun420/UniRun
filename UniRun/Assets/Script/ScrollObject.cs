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
            return; // ���� ���� ���¿����� ��ũ���� ����
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}
