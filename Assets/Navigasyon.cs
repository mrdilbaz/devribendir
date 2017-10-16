using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigasyon : MonoBehaviour {




    private bool navOpen = false;

    public GameObject Kapatici;
    public EasyTween menuAnimasyon;

    public UygulamaSayfa[] _sayfalar;

    private Dictionary<Sayfa, GameObject> sayfalar = new Dictionary<Sayfa, GameObject>();

    private Sayfa dem = Sayfa.KÜTÜPHANE;


	void Awake(){
		foreach(UygulamaSayfa syf in _sayfalar){
            sayfalar.Add(syf.sayfa, syf.obj);
        }
	}

    public void NavigasyonButton(){
		if(navOpen){
            menuAnimasyon.ChangeSetState(true);
            menuAnimasyon.OpenCloseObjectAnimation();
            Kapatici.SetActive(false);
            navOpen = false;
        } else {
            menuAnimasyon.ChangeSetState(false);
            menuAnimasyon.OpenCloseObjectAnimation();
            Kapatici.SetActive(true);
            navOpen = true;
		}
	}

	public void SayfaDegistir(int _sayfa){
        Sayfa sayfa = (Sayfa)_sayfa;
        sayfalar[dem].SetActive(false);
        dem = sayfa;
		sayfalar[dem].SetActive(true);
        transform.Find("Text").GetComponent<Text>().text = sayfa.ToString();
        NavigasyonButton();
    }
}

[System.Serializable]
public class UygulamaSayfa{
	public Sayfa sayfa;
	public GameObject obj;
}


public enum Sayfa{
	KÜTÜPHANE,
	PRATİK,
	KOMPOZİSYON,
	AYARLAR
}
