using System.Collections;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private CharacterController _cc;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float JumpHeight = 3f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector3 Velocity;
    [SerializeField] private Vector3 Start_Touch_Position;
    [SerializeField] private Vector3 End_Touch_Position;

    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, 0.2f, groundMask);

        //Reset Gravity on Ground
        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        
        //Touch Input
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {
                Start_Touch_Position = _camera.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y, 100f));
            }
            if (_touch.phase == TouchPhase.Moved)
            {
                
            }
            if (_touch.phase == TouchPhase.Stationary)
            {
                
            }
            if (_touch.phase == TouchPhase.Ended)
            {
                End_Touch_Position = _camera.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y, 100f));

                SwipeLeft();
                SwipeRight();
                SwipeUp();
                SwipeDown();
            }
        }

        Velocity.y += gravity * Time.deltaTime;
        _cc.Move(Velocity * Time.deltaTime);
    }

    private void SwipeLeft()
    {
        float swipeDistance = SwipeDistance(End_Touch_Position.x, Start_Touch_Position.x);
        float swipeThreshold = 10f;
        Debug.Log("SwipeDistance = "+swipeDistance.ToString());
        if (isSwipeDirection(End_Touch_Position.x, Start_Touch_Position.x) && swipeDistance > swipeThreshold)
        {
            Vector3 move = Vector3.left * SwipeAmount(swipeDistance,25f);
            _cc.Move(move);
        }
    }
    private void SwipeRight()
    {
        float swipeDistance = SwipeDistance(Start_Touch_Position.x, End_Touch_Position.x);
        float swipeThreshold = 10f;
        Debug.Log("SwipeDistance = " + swipeDistance.ToString());
        if (isSwipeDirection(Start_Touch_Position.x, End_Touch_Position.x) && swipeDistance > swipeThreshold)
        {
            Vector3 move = Vector3.right * SwipeAmount(swipeDistance, 25f);
            _cc.Move(move);
        }
    }
    private void SwipeUp()
    {
        float swipeDistance = SwipeDistance(Start_Touch_Position.y, End_Touch_Position.y);
        float swipeThreshold = 300f;
        if (isSwipeDirection(Start_Touch_Position.y, End_Touch_Position.y) && swipeDistance > swipeThreshold && isGrounded)
        {
            //Jump here
            Debug.Log("Jump");
            Velocity.y = Mathf.Sqrt(2f * -gravity * JumpHeight);
            _cc.Move(Velocity * Time.deltaTime);
        }
    }
    private void SwipeDown()
    {
        float swipeDistance = SwipeDistance(End_Touch_Position.y, Start_Touch_Position.y);
        float swipeThreshold = 300f;
        if (isSwipeDirection(End_Touch_Position.y,Start_Touch_Position.y) && swipeDistance > swipeThreshold)
        {
            if (isGrounded)
            {
                //Couch here
                Debug.Log("Down");
                transform.localScale = new Vector3(3f, 1.7f, 1.7f);
                StartCoroutine(ResetCrouchScale());
            }
            else
            {
                Debug.Log("ToGround");
                Vector3 move = Vector3.down * 4f;
                _cc.Move(move);
            }
        }
        
    }
    IEnumerator ResetCrouchScale()
    {
        yield return new WaitForSeconds(1f);
        transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
    }

    private float SwipeDistance(float StartPos, float EndPos)
    {
        return Mathf.Abs(StartPos - EndPos);
    }
    private float SwipeDirection(float StartPos, float EndPos)
    {
        return StartPos < EndPos ? 1f : -1f;
    }
    private bool isSwipeDirection(float StartPos, float EndPos)
    {
        return StartPos < EndPos;
    }
    private float SwipeAmount(float swipeDistance,float value1)
    {
        return swipeDistance < value1 ? 2f : 4f; 
    }


}
