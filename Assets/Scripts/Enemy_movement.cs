using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] points;
    public Transform player;

    [Header("Movement")]
    [SerializeField] float speed = 4f;
    [SerializeField] float chaseDistance = 10f;
    [SerializeField] float stopChaseDistance = 15f;
    [SerializeField] float minDistance = 2.5f;

    [Header("Attack")]
    [SerializeField] float attackDistance = 5f;
    [SerializeField] float fireRate = 5f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    Rigidbody rb;
    PlayerMovement playerScript;

    int _currentIndex = 0;
    float nextFireTime = 0f;

    enum State { Patrol, Chase }
    State currentState = State.Patrol;


    [SerializeField]
    float _speedOfBullet = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerScript = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float distance = Vector3.Distance(rb.position, player.position);


        if (playerScript != null && playerScript.isSprinting)
        {
            currentState = State.Patrol;
        }
        else
        {
            if (distance <= chaseDistance)
                currentState = State.Chase;
            else if (distance >= stopChaseDistance)
                currentState = State.Patrol;
        }


        if (currentState == State.Chase)
            HandleChase(distance);
        else
            Patrol();
    }

    // PATROL 
    void Patrol()
    {
        if (points.Length == 0) return;

        if (Vector3.Distance(rb.position, points[_currentIndex].position) < 0.1f)
        {
            _currentIndex = (_currentIndex + 1) % points.Length;
        }

        MoveEnemy(points[_currentIndex].position);
    }

    // CHASE + ATTACK 
    void HandleChase(float distance)
    {

        Vector3 dir = (player.position - transform.position).normalized;
        if (dir != Vector3.zero)
            transform.forward = dir;

        if (distance > attackDistance)
        {
            // chase
            MoveEnemy(player.position);
        }
        else if (distance > minDistance)
        {
            // Chase + attack
            MoveEnemy(player.position);
            Attack();
        }
        else
        {
            // Stop + attack
            Attack();
        }
    }

    // ATTACK
    void Attack()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject obj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        obj.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * _speedOfBullet);
    }

    //  MOVE 
    void MoveEnemy(Vector3 target)
    {
        rb.position = Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime);
    }
}