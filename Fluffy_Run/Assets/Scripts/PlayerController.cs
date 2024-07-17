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

    private float ScreenWidth;
    private void Start()
    {
        ScreenWidth = Screen.width;
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
                Start_Touch_Position = _touch.position;
            }
            if (_touch.phase == TouchPhase.Moved)
            {
                
            }
            if (_touch.phase == TouchPhase.Stationary)
            {

            }
            if (_touch.phase == TouchPhase.Ended)
            {
                End_Touch_Position = _touch.position;
                SwipeHorizontal();
                SwipeUp();
                SwipeDown();
            }
        }

        Velocity.y += gravity * Time.deltaTime;
        _cc.Move(Velocity * Time.deltaTime);
    }
    private void SwipeHorizontal()
    {
        float swipeDistance = SwipeDistance(Start_Touch_Position.x, End_Touch_Position.x);
        float swipeDirection = SwipeDirection(Start_Touch_Position.x, End_Touch_Position.x);
        float swipeThreshold = ScreenWidth/4f;
        Debug.Log("SwipeThreshold: " + swipeThreshold.ToString() + "SwipeDistance: " + swipeDistance.ToString());
        if (swipeDistance > swipeThreshold)
        {
            Vector3 move = new Vector3(swipeDirection * SwipeAmount(swipeDistance,swipeThreshold+50f), 0f,0f);
            _cc.Move(move);
        }
    }
    
    private void SwipeUp()
    {
        float swipeDistance = SwipeDistance(Start_Touch_Position.y, End_Touch_Position.y);
        float swipeThreshold = 300f;
        if (swipeDistance > swipeThreshold && isGrounded)
        {
            //Jump here
            Debug.Log("Jump");
            Velocity.y = Mathf.Sqrt(2f * -gravity * JumpHeight);
            _cc.Move(Velocity * Time.deltaTime);
        }
    }
    private void SwipeDown()
    {
        if (SwipeDistance(End_Touch_Position.y, Start_Touch_Position.y) > 300f)
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
    private float SwipeAmount(float swipeDistance,float value1)
    {
        return swipeDistance < value1 ? 2f : 4f; 
    }


}
