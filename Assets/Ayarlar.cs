using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ayarlar : MonoBehaviour {

    public EasyTween ayarlarAnimation;
    public GameObject Asıl;
    public Yonetmen yonetmen;
    public Slider tempoSlider;
	public InputField tempoText;
    public Toggle vuruslarToggle;
	public Toggle metronomToggle;


	public int tempo{
		get{
            return PlayerPrefs.GetInt("tempo", 60);
        }
		set{
            PlayerPrefs.SetInt("tempo", value);
        }
	}

	public bool vuruslariGoster {
		get{
            return (PlayerPrefs.GetInt("vuruslariGoster", 1) == 1);
        }
		set{
            PlayerPrefs.SetInt("vuruslariGoster", value ? 1 : 0);
        }
	}



	public bool metronom
    {
        get
        {
            return (PlayerPrefs.GetInt("metronom", 1) == 1);
        }
        set
        {
            PlayerPrefs.SetInt("metronom", value ? 1 : 0);
        }
    }

	public void AyarlarButton(){
        Asıl.SetActive(false);
		yonetmen.CancelInvoke("Degistir");
		yonetmen.CancelInvoke("TikTak");
		yonetmen.CancelInvoke("Cikar");
		ayarlarAnimation.OpenCloseObjectAnimation();        
    }

	public void Kaydet(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Awake(){
        tempoSlider.value = tempo;
		tempoText.text = tempo.ToString();
        vuruslarToggle.isOn = vuruslariGoster;
        metronomToggle.isOn = metronom;
    }


	public void TempoDegisti(){
        tempo = (int)tempoSlider.value;
        tempoText.text = tempo.ToString();
    }

	public void vuruslarGoster(){
		vuruslariGoster = vuruslarToggle.isOn;
	}

	public void metronomDegisti(){
        metronom = metronomToggle.isOn;
    }

}
