using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed = 10f;
    public GameManager gameManager;
    private void Update()
    {
        //if(gameManager.isGameOVer)
        //{
        //    return;
        //}
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}
