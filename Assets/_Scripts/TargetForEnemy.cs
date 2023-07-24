using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetForEnemy : MonoBehaviour
{
    [SerializeField] private Transform _menuTarget;
    void Start()
    {
        GameManager.Instance.SetMenuTarget(_menuTarget); 
    }
}

//[Serializable]
//public class TargetForWaveData
//{
//    [SerializeField] private Transform target;
//    [SerializeField] private int configIndex;

//    [SerializeField] public Transform Target => target;
//    [SerializeField] public int ConfigIndex => configIndex;
//}