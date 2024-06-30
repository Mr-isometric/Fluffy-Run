using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] CharacterController _cc;
    [SerializeField] Transform _groundCheck;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float gravity = -80f;
    [SerializeField] private float JumpHeight = 5f;
    [SerializeField] private float _SwipeVerticalThreshold = 50f;
    [SerializeField] private float _SwipeHorizontalThreshold = 50f;
    [SerializeField] LayerMask groundMask;
    private bool isGrounded;
    private Vector3 _move;
    private Vector3 velocity;
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private void Start()
    {
    }
    private void Update()
    {

        isGrounded = Physics.CheckSphere(_groundCheck.position, 0.1f, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {
                startTouchPosition = _touch.position;
            }
            if (_touch.phase == TouchPhase.Moved)
            {
                currentTouchPosition = _touch.position;
                SwipeLeft();
                SwipeRight();
            }
            if (_touch.phase == TouchPhase.Stationary)
            {
                
            }
            
            if (_touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = _touch.position;
                SwipeUp();
                SwipeDown();
                _move.x = 0f;
            }
        }

        _cc.Move(_move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        _cc.Move(velocity * Time.deltaTime);
    }

    private void SwipeUp()
    {
        float SwipeDistance = endTouchPosition.y - startTouchPosition.y;
        Debug.Log(SwipeDistance);
        if (endTouchPosition.y > startTouchPosition.y && Mathf.Abs(SwipeDistance) > _SwipeVerticalThreshold)
        {
            //Jump
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }
    private void SwipeDown()
    {
        float SwipeDistance = endTouchPosition.y - startTouchPosition.y;
        Debug.Log(SwipeDistance);
        if (endTouchPosition.y < startTouchPosition.y && Mathf.Abs(SwipeDistance) > _SwipeVerticalThreshold)
        {         
            //Duck
            transform.rotation = Quaternion.Euler(new Vector3(20f,0f,0f));
            StartCoroutine(UnDuckPlayer());
        }
    }
    private void SwipeLeft()
    {
        float SwipeDistance = currentTouchPosition.x - startTouchPosition.y;
        Debug.Log(SwipeDistance);
        if (currentTouchPosition.x > startTouchPosition.x && Mathf.Abs(SwipeDistance) > _SwipeHorizontalThreshold)
        {
            _move.x = 1f;
        }
    }
    private void SwipeRight()
    {
        float SwipeDistance = currentTouchPosition.x - startTouchPosition.y;
        Debug.Log(SwipeDistance);
        if (currentTouchPosition.x < startTouchPosition.x && Mathf.Abs(SwipeDistance) > _SwipeHorizontalThreshold)
        {
            _move.x = -1f;
        }
    }
    IEnumerator UnDuckPlayer()
    {
        yield return new WaitForSeconds(1f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }
}
