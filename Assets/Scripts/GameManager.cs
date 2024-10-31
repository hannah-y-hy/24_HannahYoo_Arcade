using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchStars2D : MonoBehaviour
{
    public GameObject[] stars; // Array of star GameObjects
    public float spawnRange = 5f; // Range for new positions
    private int starsCollected = 0; // Count of stars collected

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

                // Make it reappear after a short delay
                StartCoroutine(Reappear(star));
            }
        }
    }

    System.Collections.IEnumerator Reappear(GameObject star)
    {
        yield return new WaitForSeconds(1f); // Wait time before reappearing
        star.SetActive(true);
    }
}