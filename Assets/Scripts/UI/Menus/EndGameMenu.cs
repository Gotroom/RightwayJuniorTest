using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI.Menus
{
    public class EndGameMenu : MonoBehaviour, IBaseMenu
    {
        // меню, появляющееся после поражения

        [SerializeField]
        private GameObject _menuPanel;

        [SerializeField]
        private Text _resultText;

        public static Action Restart; // действие при нажатии кнопки New Game

        private void Awake()
        {
            HideMenu();
        }

        public void OnRestartClicked() // метод вызываемый при нажатии кнопки (EventSystem)
        {
            Restart.Invoke();
        }

        // показываем меню
        public void ShowMenu()
        {
            _menuPanel.SetActive(true);
        }

        // прячем меню
        public void HideMenu()
        {
            _menuPanel.SetActive(false);
        }

        public void SetResults(int score)
        {
            _resultText.text = $"Game Over. You earned {score} points!";
        }
    }
}