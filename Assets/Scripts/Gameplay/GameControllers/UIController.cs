using UnityEngine;
using UI.IngameUI;
using UI.Menus;
using System;

namespace Gameplay.GameControllers
{
    public class UIController : MonoBehaviour
    {
        //класс отвечающий за работу с UI
        public static Action Restart;

        private ScoreUI _scoreUI;
        private EnduranceUI _enduranceUI;
        private EndGameMenu _endGameMenu;

        private void Awake()
        {
            // ищем элементы UI
            _scoreUI = FindObjectOfType<ScoreUI>();
            _enduranceUI = FindObjectOfType<EnduranceUI>();
            _endGameMenu = FindObjectOfType<EndGameMenu>();

            // подписываемся на нажатие кнопки
            EndGameMenu.Restart += OnRestart;

            // UI создается один раз
            DontDestroyOnLoad(this);
        }

        private void OnDisable()
        {
            // отписываемся от нажатия кнопки
            EndGameMenu.Restart -= OnRestart;
        }

        public void IncreaseScore(int points)
        {
            _scoreUI.IncreaseScore(points);
        }

        public void ChangeEndurance(float endurance)
        {
            _enduranceUI.ChangeEndurance(endurance);
        }

        public void Die(int points)
        {
            Time.timeScale = 0.0f; //останавливаем время
            _endGameMenu.SetResults(points); //записываем в меню результат (очки)
            _endGameMenu.ShowMenu();
        }

        private void OnRestart()
        {
            _endGameMenu.HideMenu();
            Restart.Invoke();
        }
    }
}

