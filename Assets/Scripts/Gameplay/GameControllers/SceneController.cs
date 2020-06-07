using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.GameControllers
{
    public class SceneController : MonoBehaviour
    {
        //класс отвечающий за перезапуск игры в случае проигрыша
        private const int DEFAULT_LEVEL_INDEX = 0;

        public void LoadScene()
        {
            SceneManager.LoadScene(DEFAULT_LEVEL_INDEX);
        }
    }
}