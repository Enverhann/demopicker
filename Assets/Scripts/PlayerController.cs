using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Rigidbody rb;
    [SerializeField] private float speed = default;
    private Vector3 _direction;
    Vector2 firstPos, targetPos;
    [SerializeField] private RectTransform rect;
    [SerializeField] private float playerSpeed;
    public bool _canMove = false;
    private Buttons _buttons;

    void Initialized()
    {
        EventManager.PointerDragged += Move;
    }
    public void OnDisable()
    {
        EventManager.PointerDragged -= Move;
    }
    private void Awake()
    {
        Instance = this;
        Initialized();
        rb = GetComponent<Rigidbody>();
        _buttons = GameObject.FindGameObjectWithTag("Buttons").GetComponent<Buttons>();
    }
    private void FixedUpdate()
    {
        if (_canMove) { 
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, playerSpeed * Time.fixedDeltaTime);
        }else if (!_canMove)
        {
            rb.velocity = Vector3.zero;
        }
    }
    public void Move(PointerEventData pointerEventData)
    {
        //Player movement with event.
        if (_canMove) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, pointerEventData.position, pointerEventData.pressEventCamera, out targetPos);

            Vector2 dragPos = targetPos - firstPos;
            _direction = new Vector3(dragPos.x / Screen.width, 0, dragPos.y / Screen.height);

            if (pointerEventData.dragging)
            {
                rb.velocity = _direction * speed;
            }
        }      
    }
    public void Touch(PointerEventData pointerEventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, pointerEventData.position, pointerEventData.pressEventCamera, out firstPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _buttons.nextLevel.SetActive(true);
            _canMove = false;
        }
    }
}
