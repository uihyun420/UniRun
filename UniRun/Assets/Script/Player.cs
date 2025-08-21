using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 100f;
    public int jumpCountMax = 2;
    public int jumpCount = 0;

    private Animator animator;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public AudioClip dieAudioClip;

    private bool isGrounded = true; // 애니메이터의 파라미터  설정 
    private bool isDead = false;

    public GameManager gameManager;
    private void Awake()
    {
        animator = GetComponent<Animator>(); // 여기다가 해라 이유는 업데이트에서 해도 되지만 업데이트에서는 순간순간 계속 불러오는것이지만 여기다가 설정하면 한번만 해도 ㄱㅊ
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        //isDead = false;
        //isGrounded = true;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    gameManager.AddScore(10);
        //}
        if (isDead)
            return;

        if (Input.GetMouseButtonDown(0) && jumpCount < jumpCountMax) // 좌버튼
        {
            rb.linearVelocity = Vector2.zero; // 속도가 y값에 남아있게 된다면 AddForce를 하면 더 높게 뛰기 때문에 0으로 초기화
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            ++jumpCount;
            audioSource.Play();

        }

        if (Input.GetMouseButtonUp(0) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity *= 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);

    }

    private void OnCollisionEnter2D(Collision2D collision) // 충돌처리 함수
    {
        if (collision.collider.CompareTag("Platform") && collision.contacts[0].normal.y > 0.7f)
        {
            jumpCount = 0;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) // 충돌처리 함수
    {
        if (collision.collider.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("DeadZone"))
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;

        gameManager.OnPlayerDead();
        audioSource.PlayOneShot(dieAudioClip);
    }
}


