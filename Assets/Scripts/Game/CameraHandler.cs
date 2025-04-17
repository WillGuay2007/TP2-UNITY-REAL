using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private Vector3 m_Offset;

    void Update()
    {
        transform.position = m_Player.transform.position + m_Offset;
    }
}
