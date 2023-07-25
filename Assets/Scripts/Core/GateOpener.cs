using UnityEngine;

namespace RPG.Core
{
    public class GateOpener : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                GetComponent<Animation>().PlayQueued("OpenGates");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GetComponent<Animation>().PlayQueued("CloseGates");
            }
        }
    }
}