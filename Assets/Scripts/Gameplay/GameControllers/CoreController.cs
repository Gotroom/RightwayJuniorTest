using UnityEngine;

namespace Gameplay.GameControllers
{
    public class CoreController : MonoBehaviour
    {
        //класс отвечающий за связь игровой логики и UI

        private UIController _uiController; // контроллер UI
        private SceneController _sceneController; // контроллер сцен

        private int _currentScore = 0; // текущий результат игры

        private void Awake()
        {
            Time.timeScale = 1.0f; // выставляем течение времени в 1, т.к. у нас при поражении время фризится

            _uiController = FindObjectOfType<UIController>(); // UIController создается один раз на всю игру
            if (_uiController == null)
            {
                var prefab = Resources.Load("UI/UICanvas");
                Instantiate(prefab, Vector3.zero, Quaternion.identity);
                _uiController = FindObjectOfType<UIController>();
            }

            _sceneController = FindObjectOfType<SceneController>();

            // подписываемся на события
            EnemySpaceship.IncreaseScore += OnIncreaseScore;
            PlayerSpaceship.EnduranceChanged += OnEnduranceChanged;
            PlayerSpaceship.Die += OnDie;
            UIController.Restart += OnRestart;
        }

        private void OnDisable()
        {
            // отписываемся
            EnemySpaceship.IncreaseScore -= OnIncreaseScore;
            PlayerSpaceship.EnduranceChanged -= OnEnduranceChanged;
            PlayerSpaceship.Die -= OnDie;
        }

        // при увеличении счета
        private void OnIncreaseScore(int points)
        {
            _currentScore += points;
            _uiController.IncreaseScore(_currentScore);
        }

        // при изменении здоровья
        private void OnEnduranceChanged(float endurance)
        {
            _uiController.ChangeEndurance(endurance);
        }

        // при поражении
        private void OnDie()
        {
            Time.timeScale = 0.0f;
            _uiController.Die(_currentScore);
        }

        // при нажатии кнопки "New Game"
        private void OnRestart()
        {
            _sceneController.LoadScene();
        }

    }
}