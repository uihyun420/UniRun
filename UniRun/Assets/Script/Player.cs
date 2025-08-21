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

    private bool isGrounded = true; // �ִϸ������� �Ķ����  ���� 
    private bool isDead = false;

    public GameManager gameManager;
    private void Awake()
    {
        animator = GetComponent<Animator>(); // ����ٰ� �ض� ������ ������Ʈ���� �ص� ������ ������Ʈ������ �������� ��� �ҷ����°������� ����ٰ� �����ϸ� �ѹ��� �ص� ����
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

        if (Input.GetMouseButtonDown(0) && jumpCount < jumpCountMax) // �¹�ư
        {
            rb.linearVelocity = Vector2.zero; // �ӵ��� y���� �����ְ� �ȴٸ� AddForce�� �ϸ� �� ���� �ٱ� ������ 0���� �ʱ�ȭ
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

    private void OnCollisionEnter2D(Collision2D collision) // �浹ó�� �Լ�
    {
        if (collision.collider.CompareTag("Platform") && collision.contacts[0].normal.y > 0.7f)
        {
            jumpCount = 0;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) // �浹ó�� �Լ�
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


