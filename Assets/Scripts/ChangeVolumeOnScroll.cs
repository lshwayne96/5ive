using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeOnScroll : MonoBehaviour {

    void Start() {

    }

    void Update() {

    }

    public void changeVolume() {
        AudioListener.volume = GetComponent<Slider>().value;
    }
}
