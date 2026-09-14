using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Player Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float critRate;
    [SerializeField] private float critDamage;

    [Header("Temporary values")]
    public float finalDamage;
    public bool isCrit;

    [SerializeField] private Animator animator;

    void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.Playing)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            transform.position += direction * moveSpeed * Time.deltaTime;

            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    public void DamageEnemy()
    {
        var decimalCrit = critRate / 100f;
        var decimalCritDamage = critDamage / 100f;

        if (UnityEngine.Random.value < decimalCrit)
        {
            finalDamage = damage * (1f + decimalCritDamage);
            isCrit = true;
        }
        else
        {
            finalDamage = damage;
            isCrit = false; // Fixes persistent crit state
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.killedEnemyCount++;
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            GameManager.Instance.AddScore(GameManager.Instance.levelController.giveScore);

            DamageEnemy();

            GameManager.Instance.uimanager.DamageUI(finalDamage, isCrit, enemy.transform);

            enemy.OnDied();
        }
    }
}