using UnityEngine;

public class DieAnimation : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Animator>().SetTrigger("die");
    }
}
