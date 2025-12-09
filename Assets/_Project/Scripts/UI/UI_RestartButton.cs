using UnityEngine;
using UnityEngine.SceneManagement;

namespace HiddenObject.UI
{
    public class SceneReloader : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //осознанный костыль для дебага, по ТЗ не предусмотрено
        }
    }
}
