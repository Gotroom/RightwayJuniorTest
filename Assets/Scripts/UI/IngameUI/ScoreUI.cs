using UnityEngine;
using UnityEngine.UI;

namespace UI.IngameUI
{
    public class ScoreUI : MonoBehaviour
    {
        // класс отвечающий за отображение счета игрока
        private Text _scoreText;

        private void Awake()
        {
            _scoreText = GetComponent<Text>();
        }

        public void IncreaseScore(int score)
        {
            if (_scoreText)
            {
                _scoreText.text = $"Score: {score}";
            }
        }
    }
}