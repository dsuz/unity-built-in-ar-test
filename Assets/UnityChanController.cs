using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnityChanController : MonoBehaviour
{
    Animator m_animator;
    float m_counter;
    int m_maxState = 8;
    int m_state = 0;
    Quaternion m_originalQuaternion;
    Vector3 m_originalPosition;

    [SerializeField] float m_stateChangeIntervalMinimum = 3f;
    float m_animationLength = 0f;

	void Start ()
    {
        m_originalPosition = transform.localPosition;
        m_originalQuaternion = transform.localRotation;
        m_animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        m_counter += Time.deltaTime;
        m_animationLength = (m_animationLength > m_stateChangeIntervalMinimum) ? m_animationLength : m_stateChangeIntervalMinimum;
        if (m_counter > m_animationLength)
        {
            transform.localPosition = m_originalPosition;
            transform.localRotation = m_originalQuaternion;
            m_counter = 0f;

            m_state = GetRamdomNewState(m_state);
            Debug.Log("State Change: " + m_state);
            m_animator.SetInteger("State", m_state);
            AnimatorStateInfo animInfo = m_animator.GetCurrentAnimatorStateInfo(0);
            m_animationLength = animInfo.length;
        }
	}

    int GetRamdomNewState(int oldState)
    {
        int newState = Random.Range(1, m_maxState + 1);
        if (newState == oldState)
        {
            return GetRamdomNewState(oldState);
        }
        else
            return newState;
    }
}
