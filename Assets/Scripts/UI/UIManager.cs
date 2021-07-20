using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Maze
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI time;
        private int elapsedTime;
        // Start is called before the first frame update
        void Start()
        {
            elapsedTime = 0;
            StartCoroutine(Timer());
        }
        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
        IEnumerator Timer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                elapsedTime++;
                time.text = "Time: " + elapsedTime;
            }
        }
        public void PauseButton()
        {
            Time.timeScale = 0;
        }
        public void Play()
        {
            Time.timeScale = 1;
        }


    }
}

