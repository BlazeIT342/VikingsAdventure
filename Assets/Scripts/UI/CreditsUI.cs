using RPG.SceneManagement;
using UnityEngine;

namespace RPG.UI
{
    public class CreditsUI : MonoBehaviour
    {    
        public void ReloadGame()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.LoadMenu();
        }
    }
}