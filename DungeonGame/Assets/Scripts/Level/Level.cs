using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelLogic
{
    [RequireComponent(typeof(LevelStateMachine))]
    public class Level : MonoBehaviour
    {
        private static string _contextSceneName;

        private static Level _instance;

        private LevelStateMachine _currentStateMachine;
        public static Level Instance => _instance;

        public LevelStateMachine CurrentStateMachine => _currentStateMachine;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_contextSceneName))
            {
                _contextSceneName = SceneManager.GetActiveScene().name;
            }
            else if(SceneManager.GetActiveScene().name != _contextSceneName)
            {
                Debug.LogError($"Component Level cant's exist out of scene {_contextSceneName}");
                return;
            }
            if(_instance != null)
            {
                Debug.LogError("Component Level already exist in current scene");
                return;
            }
            _instance = this;
            _currentStateMachine ??= GetComponent<LevelStateMachine>();
        }
    }
}