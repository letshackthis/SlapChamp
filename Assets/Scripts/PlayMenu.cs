using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
   [SerializeField] private Button playOnline;
   [SerializeField] private Button playOffline;
   [SerializeField] private Button closeButton;
   [SerializeField] private GameObject playMenu;
   [SerializeField] private GameObject playBg;
   private void Awake()
   {
      closeButton.onClick.AddListener(() =>
      {
         playMenu.SetActive(false);
         playBg.SetActive(false);
      });

      playOnline.onClick.AddListener(() =>
      {
         SceneManager.LoadScene("Level1");
      });
      playOffline.onClick.AddListener(() =>
      {
         SceneManager.LoadScene("Level1");
      });
   }
}
