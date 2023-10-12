using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private GameObject _chest;
    [SerializeField] private PlayerInventory _inventory;
    public GameObject[] slots = new GameObject[4];

    [Header("Chest Items")]
    public GameObject woodPrefab;
    public GameObject pickaxePrefab;
    public GameObject oreItemPrefab;

    [Space(20)]

    public bool canOpenChest = true;
    [HideInInspector] public bool firstChest = true;
    [HideInInspector] public bool secondChest = true;
    [HideInInspector] public string lastChest;

    public void FillChest(string name) {
        if (name == "Chest 1") {
            Instantiate(woodPrefab, slots[1].transform);
        } else if (name == "Chest 2") {
            Instantiate(woodPrefab, slots[0].transform);
            Instantiate(pickaxePrefab, slots[2].transform);
        }
    }

    public void OpenChest() {
        if (canOpenChest) {
            if (lastChest == "Chest 1" && firstChest) {
                firstChest = false;
                FillChest("Chest 1");
            } else if (lastChest == "Chest 2" && secondChest) {
                secondChest = false;
                FillChest("Chest 2");
            }
            GameObject.Find("Panel").GetComponent<Animator>().SetBool("isOn", true);
            _chest.GetComponent<Animator>().SetBool("ChestOpen", true);
            GameObject.Find("Floating GUI").GetComponent<Animator>().SetBool("StartOpen", false);
        }
    }

}
