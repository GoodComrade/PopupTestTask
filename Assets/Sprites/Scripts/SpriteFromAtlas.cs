using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteFromAtlas : MonoBehaviour
{
    [SerializeField] private SpriteAtlas atlas;
    [SerializeField] private string spriteName;
    void Start()
    {
        GetComponent<Image>().sprite = atlas.GetSprite(spriteName);
    }

}
