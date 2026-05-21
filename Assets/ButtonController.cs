using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public AudioClip clickClip;
    public string targetScene = "GameScene";

    public void OnButtonClick()
    {
        PlayerPrefs.SetInt("FinalScore", 0);
        PlayerPrefs.Save();

        GameObject manager = GameObject.Find("MusicController");
        if (manager != null)
        {
            AudioSource source = manager.GetComponent<AudioSource>();
            if (source != null && clickClip != null)
            {
                source.PlayOneShot(clickClip);
            }
        }

        SceneManager.LoadScene(targetScene);
    }
}