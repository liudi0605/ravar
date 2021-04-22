using Itsdits.Ravar.Character;
using Itsdits.Ravar.Core.Signal;
using Itsdits.Ravar.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Itsdits.Ravar.Core
{
    /// <summary>
    /// Controller class for the core game scene. Controls player, camera and event functions.
    /// </summary>
    public class CoreController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Controller object for the player.")]
        [SerializeField] private PlayerController _playerController;
        [Tooltip("Camera attached to the player prefab.")]
        [SerializeField] private Camera _playerCamera;

        [Header("Event System")]
        [Tooltip("Event System for the core scene.")]
        [SerializeField] private EventSystem _eventSystem;

        private void Awake()
        {
            DisablePlayer();
            GameSignals.NEW_GAME.AddListener(LoadGame);
            GameSignals.LOAD_GAME.AddListener(LoadGame);
        }

        private void OnDestroy()
        {
            GameSignals.NEW_GAME.RemoveListener(LoadGame);
            GameSignals.LOAD_GAME.RemoveListener(LoadGame);
        }

        private void LoadGame(string sceneName)
        {
            EnablePlayer();
            string sceneToLoad = GameData.PlayerData.currentScene;
            string previousScene = PlayerPrefs.GetString("previousMenu");
            if (previousScene == "UI.Menu.Main")
            {
                StartCoroutine(SceneLoader.Instance.LoadScene(sceneToLoad));
            }
            else if (previousScene == "UI.Menu.Pause")
            {
                StartCoroutine(SceneLoader.Instance.UnloadScene("UI.Menu.Load"));
                GameSignals.RESUME_GAME.Dispatch(true);
            }
        }

        private void EnablePlayer()
        {
            _eventSystem.enabled = true;
            _playerCamera.enabled = true;
            _playerController.GetComponent<SpriteRenderer>().enabled = true;
        }

        private void DisablePlayer()
        {
            _eventSystem.enabled = false;
            _playerCamera.enabled = false;
            _playerController.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}