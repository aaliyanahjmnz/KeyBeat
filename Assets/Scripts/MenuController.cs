using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider difficultySlider;
    public TMP_Text difficultyText;

    public float maxDifficulty;
    public float minDifficulty;

    private void Start()
    {
        difficultySlider.minValue = 0f;
        difficultySlider.maxValue = 1f;
        difficultySlider.value = 1f;

        UpdateDifficulty();
    }

    public void UpdateDifficulty()
    {
        GameSettings.NoteSpawnRate =
            Mathf.Lerp(maxDifficulty, minDifficulty, difficultySlider.value);

        difficultyText.text =
            $"Difficulty: {difficultySlider.value:P0}";
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("RandomGame");
    }

    public void QuitGame()
    {
        Application.Quit();

        // Works in editor
        Debug.Log("Quit Game");
    }
}
