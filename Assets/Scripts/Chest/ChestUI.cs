using UnityEngine;
using UnityEngine.Events;

public class ChestUI : MonoBehaviour
{
    public GameObject[] slots = new GameObject[4];
    public UnityAction<int> _chestSlotOnClickAction;

    public void OnButtonClick(int i) {
        _chestSlotOnClickAction(i);
    }
}
