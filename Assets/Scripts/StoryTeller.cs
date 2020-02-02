using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTeller : MonoBehaviour
{
    public GameObject templateGameObject;
    public List<Sprite> sprites;

    int currentIndex;

    private List<GameObject> pictures = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sprite sprite in sprites)
        {
            GameObject pictureGO = Instantiate(templateGameObject);
            pictureGO.transform.SetParent(templateGameObject.transform.parent);

            pictureGO.GetComponent<Image>().sprite = sprite;
            pictureGO.GetComponent<Image>().SetNativeSize();

            pictureGO.transform.localPosition = Vector3.zero;

            pictures.Add(pictureGO);
        }

        pictures[0].SetActive(true);
    }

    public void OnNextButtonPressed()
    {
        pictures[currentIndex].SetActive(false);

        currentIndex++;

        if(currentIndex+1 == sprites.Count)
            GameManager.Instance.NewGame();

        pictures[currentIndex].SetActive(true);
    }
}
