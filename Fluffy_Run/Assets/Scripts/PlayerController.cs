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
        if (SwipeCalculator(End_Touch_Position.x, Start_Touch_Position.x, 300f))
        {
            Debug.Log("Left");
            Vector3 move = Vector3.left * 2f;
            _cc.Move(move);
        }
    }
    private void SwipeRight()
    {
        if (SwipeCalculator(Start_Touch_Position.x, End_Touch_Position.x, 300f))
        {
            Debug.Log("Right");
            Vector3 move = Vector3.right * 2f;
            _cc.Move(move);
        }
    }
    private void SwipeUp()
    {
        if (SwipeCalculator(Start_Touch_Position.y, End_Touch_Position.y, 300f) && isGrounded)
        {
            //Jump here
            Debug.Log("Jump");
            Velocity.y = Mathf.Sqrt(2f * -gravity * JumpHeight);
            _cc.Move(Velocity * Time.deltaTime);
        }
    }
    private void SwipeDown()
    {
        if (SwipeCalculator(End_Touch_Position.y, Start_Touch_Position.y, 300f))
        {
            if (isGrounded)
            {
                //Couch here
                Debug.Log("Down");
                transform.localScale = new Vector3(3f, 1.7f, 1.7f);
                StartCoroutine(ResetCouchScale());
            }
            else
            {
                Debug.Log("ToGround");
                Vector3 move = Vector3.down * 4f;
                _cc.Move(move);
            }
        }
        
    }
    IEnumerator ResetCouchScale()
    {
        yield return new WaitForSeconds(1f);
        transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
    }
    private bool SwipeCalculator(float StartPos, float EndPos, float SwipeThresHold)
    {
        return StartPos < EndPos && Mathf.Abs((StartPos - EndPos)) > SwipeThresHold;
    }
}
