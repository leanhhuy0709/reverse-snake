using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Script
{

    public class MainMenu : MonoBehaviour
    {
        public void GoToGameScene()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}