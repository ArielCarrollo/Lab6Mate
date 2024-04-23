using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerControls : MonoBehaviour
{
    [SerializeField] private GameObject GM;
    [SerializeField] private Rigidbody myRB;
    [SerializeField] public int HP = 3;
    [SerializeField] private float x_Movement;
    [SerializeField] private float y_Movement;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float invulnerabilityTime = 1.0f;
    [SerializeField] private float lastHitTime;
    public TextMeshProUGUI HPtxt;
    private float rotationAngle = 35f;

    // Start is called before the first frame update
    void Start()
    {
        lastHitTime = -invulnerabilityTime;
    }
    void FixedUpdate()
    {
        myRB.velocity = new Vector3(x_Movement * velocityModifier, y_Movement * velocityModifier, myRB.velocity.z);
    }
    // Update is called once per frame
    void Update()
    {
        HPtxt.text = "Vida: " + HP.ToString();
        if (x_Movement < 0 && y_Movement != 0)
        {
            myRB.MoveRotation(Quaternion.Euler(0, 0, rotationAngle));
        }
        else if (x_Movement > 0 && y_Movement != 0)
        {
            myRB.MoveRotation(Quaternion.Euler(0, 0, -rotationAngle));
        }
        else 
        {
            myRB.MoveRotation(Quaternion.Euler(0, 0, 0));
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        x_Movement = context.ReadValue<Vector3>().x;
        y_Movement = context.ReadValue<Vector3>().y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy" && Time.time - lastHitTime > invulnerabilityTime)
        {
            HP -= 1;
            lastHitTime = Time.time;

            if (HP <= 0)
            {
                GM.GetComponent<GameManager>().PlayerDied();
                Destroy(this.gameObject);
            }
        }
    }
}
