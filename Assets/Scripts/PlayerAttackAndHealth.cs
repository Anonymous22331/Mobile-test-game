using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAttackAndHealth : MonoBehaviour
{
    public int ammoCount = 20;
    private float playerHealth = 100;
    private GameObject closestEnemy;
    private float distanceToClosestEnemy = Mathf.Infinity;
    private float shootCoolDown;
    [SerializeField] private HealthBar healthBar;

    private void Awake()
    {
        GameObject.Find("ShootButton").GetComponentInChildren<Text>().text = ammoCount.ToString();
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        return closestEnemy;
    }
    public void GetDamage()
    {
        playerHealth -= 10;
        healthBar.UpdateHealth(playerHealth, 100);
    }

    private void Death()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Shoot()
    {
        FindClosestEnemy();
        if (closestEnemy != null)
        {
            if (Vector3.Distance(transform.position, closestEnemy.transform.position) < 5 & shootCoolDown > 1)
            {
                if (ammoCount > 0)
                {
                    closestEnemy.GetComponent<EnemyAI>().GetDamage(Random.Range(15, 25));
                    ammoCount -= 1;
                    GameObject.Find("ShootButton").GetComponentInChildren<Text>().text = ammoCount.ToString();
                }
            }
        }
    }

    void Update()
    {
        shootCoolDown += Time.deltaTime;
        if (playerHealth < 0)
        {
            Death();
        }
    }
}
