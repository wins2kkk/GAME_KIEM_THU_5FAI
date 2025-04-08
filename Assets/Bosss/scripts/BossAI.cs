using UnityEngine;

public class BossAI : MonoBehaviour
{
    private GameObject player; // Thay đổi từ Transform thành GameObject
    public float speed = 2f;
    public float chaseDistance = 10f;
    public float aniAttack = 5f;
    public GameObject bulletPrefab; // Prefab viên đạn
    public Transform firePoint; // Điểm xuất phát của đạn
    public float fireRate = 1f; // Tốc độ bắn
    private float nextFireTime = 0f;

    private bool facingRight = true;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player"); // Tìm GameObject theo tag "Player"
    }

    void Update()
    {
        if (player == null) return; // Kiểm tra xem player có tồn tại hay không

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < chaseDistance && distanceToPlayer > aniAttack)
        {
            animator.SetBool("isChasing", true);
            animator.SetBool("isAttacking2", false);
            animator.SetBool("isShooting", false);

            // Di chuyển Boss về phía người chơi
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            // Kiểm tra hướng và lật mặt Boss nếu cần thiết
            FlipTowardsPlayer();
        }
        else if (distanceToPlayer <= aniAttack)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking2", true);

            // Bắn khi trong phạm vi tấn công và đến thời gian bắn tiếp theo
            if (Time.time >= nextFireTime)
            {
                animator.SetBool("isShooting", true);
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
            else
            {
                animator.SetBool("isShooting", false);
            }
        }
        else
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking2", false);
            animator.SetBool("isShooting", false);
        }
    }

    // Hàm này sẽ được gọi từ Animation Event sau khi Attack2 hoàn thành
    public void OnAttack2Complete()
    {
        animator.SetBool("isAttacking2", false);
        animator.SetBool("isAttacking1", true);
    }

    void FlipTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Đảo ngược giá trị scale của trục X để lật mặt
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Shoot()
    {
        // Tạo viên đạn tại firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Lấy hướng từ Boss đến người chơi
        Vector2 direction = (player.transform.position - firePoint.position).normalized;

        // Thêm lực đẩy cho viên đạn về hướng người chơi
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<Bullet>().speed;
    }
}
