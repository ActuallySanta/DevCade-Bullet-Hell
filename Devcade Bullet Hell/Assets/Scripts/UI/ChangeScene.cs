using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    
    public void LoadNewScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
