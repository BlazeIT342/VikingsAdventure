using RPG.Control;
using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematiControlRemover : MonoBehaviour, ISaveable
    {
        GameObject player;

        bool playOneTime = true;
        bool stopped = false;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void Start()
        {
            if (playOneTime)
            {
                playOneTime = false;
                GetComponent<PlayableDirector>().Play();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !stopped)
            {
                stopped = true;
                GetComponent<PlayableDirector>().time = 80f;
            }
        }

        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }

        private void DisableControl(PlayableDirector pd)
        {
            
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        public object CaptureState()
        {
            return playOneTime;
        }

        public void RestoreState(object state)
        {
            playOneTime = (bool)state;
        }
    }
}