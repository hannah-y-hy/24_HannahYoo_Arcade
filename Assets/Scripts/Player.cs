using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed; // 이동 속도 / Player movement speed
    public float jumpForce; // 점프 힘 / Jump force

    public KeyCode left; // 왼쪽 이동 키 / Key for moving left
    public KeyCode right; // 오른쪽 이동 키 / Key for moving right
    public KeyCode jump; // 점프 키 / Key for jumping

    private Rigidbody2D theRB;

    public StarSpawner starSpawner; // StarSpawner 스크립트 참조 / Reference to StarSpawner script

    void Start()
    {
        // Rigidbody2D 컴포넌트 가져오기 / Get Rigidbody2D component
        theRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 키보드 입력에 의한 이동 처리 / Handle movement with keyboard input
        if (Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y); // 왼쪽 이동 / Move left
        }
        else if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y); // 오른쪽 이동 / Move right
        }
        else
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y); // 이동이 없으면 속도 0 / Stop when no input
        }

        // 조이스틱 X축 이동 처리 / Handle joystick movement
        float moveInput = Input.GetAxis("joystick_1_X");
        if (Mathf.Abs(moveInput) > 0.1f) // 조이스틱 민감도를 고려하여 이동 처리 / Handle movement considering joystick sensitivity
        {
            theRB.velocity = new Vector2(moveInput * moveSpeed, theRB.velocity.y);
        }

        // 점프 처리 (키보드 입력) / Handle jump with keyboard input
        if (Input.GetKeyDown(jump) && Mathf.Abs(theRB.velocity.y) < 0.01f)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce); // 점프 / Jump
        }

        // 조이스틱 점프 버튼 입력 처리 / Handle jump button press on joystick
        if (Input.GetButtonDown("joystick_button") && Mathf.Abs(theRB.velocity.y) < 0.01f)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce); // 점프 / Jump
        }
    }

    // 플레이어가 다른 오브젝트와 충돌했을 때 호출 / Called when player collides with another object
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            other.gameObject.SetActive(false); // 별을 비활성화 / Deactivate the star
            starSpawner.OnStarCollected(); // 별 수집 처리 / Handle collected star
        }
    }
}
