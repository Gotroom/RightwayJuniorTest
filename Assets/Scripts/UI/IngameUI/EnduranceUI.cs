using UnityEngine;
using UnityEngine.UI;

namespace UI.IngameUI
{
    public class EnduranceUI : MonoBehaviour
    {
        // класс отвечающий за отображение кол-ва жизней игрока
        private Text _enduranceText;

        private void Awake()
        {
            _enduranceText = GetComponent<Text>();
        }

        public void ChangeEndurance(float endurance)
        {
            if (_enduranceText != null)
            {
                _enduranceText.text = $"Endurance: {endurance}";
            }
        }
        
    }
}

