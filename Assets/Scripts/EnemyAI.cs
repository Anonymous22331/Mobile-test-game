using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    public float enemyDetectRange;
    public float enemyAttackRange;
    public GameObject[] itemPrefabs;
    private float speed = 1.5f;
    private float distanceToPlayer;
    private float coolDownAttack;
    private float enemyHealth = 100;
    [SerializeField] HealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    public void GetDamage(float damage)
    {
        enemyHealth -= damage;
        healthBar.UpdateHealth(enemyHealth, 100);
    }

    void EnemyDied()
    {
        Instantiate(itemPrefabs[Random.Range(0, 3)], transform.position, itemPrefabs[Random.Range(0, 3)].transform.rotation);
        Destroy(gameObject);
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        coolDownAttack += Time.deltaTime;
        if (distanceToPlayer < enemyDetectRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (transform.position.x - player.transform.position.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x - player.transform.position.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (distanceToPlayer < enemyAttackRange & coolDownAttack > 2)
        {
            player.GetComponent<PlayerAttackAndHealth>().GetDamage();
            coolDownAttack = 0;
        }
        if (enemyHealth < 0)
        {
            EnemyDied();
        }
    }
}
