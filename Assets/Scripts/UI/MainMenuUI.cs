using UnityEngine;
using RPG.SceneManagement;
using RPG.Utils;
using TMPro;
using RPG.Control;
using System.Collections;
using System.Text.RegularExpressions;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        LazyValue<SavingWrapper> savingWrapper;

        [SerializeField] TMP_InputField newGameNameField;

        private void Awake()
        {
            savingWrapper = new LazyValue<SavingWrapper>(GetSavingWrapper);
        }

        private void Start()
        {
            newGameNameField.onValueChanged.AddListener(OnValueChanged);
        }

        public void OnValueChanged(string value)
        {
            string filteredValue = Regex.Replace(value, "[^a-zA-Z0-9 ]", "");
            if (filteredValue != value)
            {
                newGameNameField.text = filteredValue;
            }
        }

        private SavingWrapper GetSavingWrapper()
        {
            return FindObjectOfType<SavingWrapper>();
        }

        public void ContinueGame()
        {
            StartCoroutine(ContinueGameRoutine());
        }

        public void NewGame()
        {
            StartCoroutine(NewGameRoutine());
        }

        private IEnumerator ContinueGameRoutine()
        {
            if (!savingWrapper.value.CheckForSaves()) yield break;
            GetComponent<ShowHideUI>().Toggle();
            GameObject.FindGameObjectWithTag("Civilian").GetComponent<AIPlayerInMenu>().StandupRoutine();
            yield return new WaitForSeconds(5);
            savingWrapper.value.ContinueGame();
        }

        private IEnumerator NewGameRoutine()
        {
            GetComponent<ShowHideUI>().Toggle();
            GameObject.FindGameObjectWithTag("Civilian").GetComponent<AIPlayerInMenu>().StandupRoutine();
            yield return new WaitForSeconds(5);
            savingWrapper.value.NewGame(newGameNameField.text);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}