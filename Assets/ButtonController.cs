using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void onButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
