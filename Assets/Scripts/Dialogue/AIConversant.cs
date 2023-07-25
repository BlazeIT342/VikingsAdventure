using RPG.Attributes;
using RPG.Control;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string conversantName;

        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null) return false;

            Health health = GetComponent<Health>();
            if (health && health.IsDead()) return false;

            if (Input.GetMouseButtonDown(0) && Vector3.Distance(transform.position, callingController.transform.position) < 3)
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }

            return true;
        }

        public string GetName()
        {
            return conversantName;
        }
    }
}