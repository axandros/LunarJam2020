using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonController : MonoBehaviour
{
    [SerializeField]
    Image _image = null;

    [SerializeField]
    Sprite[] MoonPhases;

    GameManager gm = null;

    // Start is called before the first frame update
    void Start()
    {
        if (!_image) { _image = GetComponent<Image>(); }
        gm = GameManager.Instance();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm && _image)
        {
            switch (gm.CurrentPhase)
            {
                case (LunarPhase.Full):
                    _image.sprite = MoonPhases[0];
                    break;
                case (LunarPhase.Waning):
                    _image.sprite = MoonPhases[1];
                    break;
                case (LunarPhase.New):
                    _image.sprite = MoonPhases[2];
                    break;
                case (LunarPhase.Waxing):
                    _image.sprite = MoonPhases[3];
                    break;
            }
        }
    }
}
