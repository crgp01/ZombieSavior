using UnityEngine;
using System.Collections.Generic;

public class ZombieCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        Tank,
        Direct
    }

    public Animator m_animator;
    public Rigidbody m_rigidBody;
    public bool zombieIsMoving;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Tank;

    void Awake()
    {
        if(!m_animator) { gameObject.GetComponent<Animator>(); }
        if(!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

	void FixedUpdate ()
    {
        switch(m_controlMode)
        {
            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }
    }

    private void TankUpdate()
    {
        /*float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);*/
    }
}
