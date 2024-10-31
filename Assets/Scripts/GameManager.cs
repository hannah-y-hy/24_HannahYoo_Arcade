using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchStars2D : MonoBehaviour
{
    public GameObject[] stars; // Array of star GameObjects
    public float spawnRange = 5f; // Range for new positions
    private int starsCollected = 0; // Count of stars collected
    public float minReappearTime = 1f; // Minimum time to reappear
    public float maxReappearTime = 3f; // Maximum time to reappear

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (GameObject star in stars)
        {
            if (other.gameObject == star && starsCollected < 5)
            {
                starsCollected++; // Increment count
                star.SetActive(false); // Hide the star

                // Move to a new random position within the range
                Vector2 newPos = new Vector2(
                    Random.Range(-spawnRange, spawnRange),
                    Random.Range(-spawnRange, spawnRange));

                star.transform.position = newPos;

                // Make it reappear after a random delay
                float randomDelay = Random.Range(minReappearTime, maxReappearTime);
                StartCoroutine(Reappear(star, randomDelay));
            }
        }
    }

    System.Collections.IEnumerator Reappear(GameObject star, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for random time
        star.SetActive(true);
    }
}