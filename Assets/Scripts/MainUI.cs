﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Michał Czemierowski
 * https://github.com/michalczemierowski
*/
namespace VoxelTG.UI
{
    /// <summary>
    /// Class capable of handling UI in main menu
    /// </summary>
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private string gameSceneName = "Game";
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Image progressBar;
        [SerializeField] private TMP_Text progressText;

        [Header("TEXT SETTINGS")]
        public string loadingSceneMessage = "LOADING SCENE";
        public string buildingTerrainMessage = "BUILDING TERRAIN";

        private void Start()
        {
            if (loadingScreen.activeSelf) { loadingScreen.SetActive(false); }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// UI Listener for 'START GAME' button
        /// </summary>
        public void StartGame()
        {
            StartCoroutine(LoadGameAsync());
        }

        private IEnumerator LoadGameAsync()
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(gameSceneName);

            loadingScreen.SetActive(true);
            progressText.text = loadingSceneMessage;

            float progress = 0;
            // scene loading progress
            while (!load.isDone)
            {
                progress = load.progress / 2;
                progressBar.fillAmount = progress;
                yield return null;
            }

            // chunk loading progress
            while (World.LoadedChunks < World.TotalChunks)
            {
                progressText.text = $"{buildingTerrainMessage} {World.LoadedChunks}/{World.TotalChunks}";
                float buildProgress = progress + ((float)World.LoadedChunks / World.TotalChunks) / 2;
                progressBar.fillAmount = buildProgress;
                yield return null;
            }

            World.OnFirstLoadDone();
            Destroy(gameObject);
        }
    }
}
