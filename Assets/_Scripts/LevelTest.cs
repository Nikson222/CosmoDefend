using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Levels
{

    public class LevelTest : MonoBehaviour
    {
        [SerializeField] LevelsController levelsController;
        [Space]
        [Header("Test Level")]
        [SerializeField] LevelConfig levelConfig;
        public void LaunchLevel()
        {
            levelsController.LaunchLevel(levelConfig);
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(LevelTest))]
    public class LevelTestEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelTest myScript = (LevelTest)target;

            // Создаем кнопку для вызова метода MyMethod
            if (GUILayout.Button("Launch Level"))
            {
                myScript.LaunchLevel();
            }
        }
    }
#endif
}
