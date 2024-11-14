using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject[] starPrefabs; // 별 프리팹 배열 / Array of star prefabs
    public EdgeCollider2D respawnArea; // 리스폰 영역 (Edge Collider 2D 사용) / Respawn area using Edge Collider 2D
    public float minReappearTime = 1f; // 최소 리스폰 시간 (인스펙터에서 설정 가능) / Minimum respawn time (adjustable in inspector)
    public float maxReappearTime = 3f; // 최대 리스폰 시간 (인스펙터에서 설정 가능) / Maximum respawn time (adjustable in inspector)
    public int starsToCollect = 5; // 수집해야 하는 별의 개수 (인스펙터에서 설정 가능) / Number of stars to collect (adjustable in inspector)

    private int starsCollected = 0; // 현재 수집한 별의 개수 / Current number of collected stars

    void Start()
    {
        // 초기 별 생성 / Initial star spawning
        SpawnStars();
    }

    void SpawnStars()
    {
        foreach (GameObject starPrefab in starPrefabs)
        {
            Vector2 randomPosition = GetRandomPositionInArea();
            GameObject star = Instantiate(starPrefab, randomPosition, Quaternion.identity);
            StartCoroutine(Reappear(star));
        }
    }

    Vector2 GetRandomPositionInArea()
    {
        Vector2 randomPoint = Vector2.zero;
        int maxAttempts = 10; // 무한 루프 방지를 위한 최대 시도 횟수 / Maximum attempts to avoid infinite loop
        int attempts = 0;

        // Edge Collider 2D 내의 점을 이용하여 랜덤 위치 생성 / Generate random position within Edge Collider 2D
        while (attempts < maxAttempts)
        {
            Bounds bounds = respawnArea.bounds;
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            randomPoint = new Vector2(randomX, randomY);

            // 점이 Edge Collider 2D 내부에 있는지 확인 / Check if point is inside Edge Collider 2D
            if (respawnArea.OverlapPoint(randomPoint))
            {
                break;
            }
            attempts++;
        }

        return randomPoint;
    }

    System.Collections.IEnumerator Reappear(GameObject star)
    {
        // 랜덤 시간 대기 후 다시 나타나게 함 / Wait for a random time before respawning the star
        float randomDelay = Random.Range(minReappearTime, maxReappearTime);
        yield return new WaitForSeconds(randomDelay);

        // 별의 위치를 다시 설정하고 활성화 / Set star position and reactivate it
        star.transform.position = GetRandomPositionInArea();
        star.SetActive(true);
    }

    public void OnStarCollected()
    {
        // 플레이어가 별을 수집했을 때 호출 / Called when a player collects a star
        starsCollected++;
        if (starsCollected >= starsToCollect)
        {
            EndGame(); // 수집한 별의 개수가 목표치에 도달하면 게임 종료 / End game when collected stars reach target
        }
    }

    void EndGame()
    {
        // 게임 종료 로직 / Logic for ending the game
        Debug.Log("게임 종료! 목표 별을 모두 수집했습니다."); // "Game Over! Collected all target stars."
        // 게임 종료 후의 로직 추가 가능 (씬 전환, 결과 창 표시 등) / Add post-game logic here (e.g., scene transition, show results, etc.)
    }
}
