using UnityEngine;

public class FloatingButtonSettings : MonoBehaviour
{
    public string action;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private Animator _floatingGUIAnimator;

    [Header("Мини-игра: Maze")]
    [SerializeField] private GameObject _mazePrefab;
    [SerializeField] private GameObject _mazeButtons;

    public void ButtonClick() {
        if (action == "ChestOpen") {
            this.GetComponent<ChestController>().OpenChest();
        } else if (action == "Stump") {
            GameObject.Find("Wood-Stump").GetComponent<Animator>().SetTrigger("StumpOn");
        } else if (action == "Maze") {
            if (!this.GetComponent<ChestController>().canOpenChest) return;
            _floatingGUIAnimator.SetBool("StartOpen", false);
            Vector3 centerScreenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1));
            Instantiate(_mazePrefab, centerScreenPosition, Quaternion.identity);
            _mazeButtons.SetActive(true);
            _controller.SetSpeed("Speed", 0);
            _controller.SetSpeed("Jump", 0);
        } else if (action == "Bridge") {

        }
    }
}
