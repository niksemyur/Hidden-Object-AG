using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HiddenObject.UI
{
    public class UI_ItemSlot : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private Image iconeImg;

        public void LoadSlot(Sprite sprite)
        {
            iconeImg.gameObject.SetActive(true);
            iconeImg.sprite = sprite;
            gameObject.SetActive(true);
        }

        public void LoadSlot(string name)
        {
            nameTxt.gameObject.SetActive(true);
            nameTxt.text = name;
            gameObject.SetActive(true);
        }

        public void ClearSlot()
        {
            iconeImg.gameObject.SetActive(false);
            nameTxt.gameObject.SetActive(false);

            iconeImg.sprite = null;
            nameTxt.text = "";

            gameObject.SetActive(false);
        }
    }
}
