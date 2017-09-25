using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yonetmen : MonoBehaviour
{

    public Usul[] Usüller;
    public EasyTween getirme;
    public EasyTween götürme;
    public EasyTween cikarma;
    public RectTransform dem;
    public RectTransform ahir;
    // Use this for initialization

    public Vector3 orta;
    public Vector3 altGorunmez;
    public Vector3 ustGorunmez;
    public Vector3 bekleme;

    private Usul demUsul;
    private Usul ahirUsul;
	
	public float ritim;

	public AudioSource metronom;
	
    void Start()
    {
        Rastgele(dem, true);
        Rastgele(ahir, false);
        Invoke("Degistir", demUsul.zaman * ritim);
		InvokeRepeating("TikTak",ritim,ritim);
    }

	void TikTak(){
		metronom.Play();
	}

    void Degistir()
    {
        götürme.ChangeSetState(false);
        getirme.ChangeSetState(false);

        götürme.rectTransform = dem;
        götürme.OpenCloseObjectAnimation();
        getirme.rectTransform = ahir;
        getirme.OpenCloseObjectAnimation();

        Invoke("Cikar", 0.25f);
    }

    void Cikar()
    {
        RectTransform tmp = dem;
        dem = ahir;
        ahir = tmp;
        Rastgele(ahir, false);
        cikarma.ChangeSetState(false);
        cikarma.rectTransform = ahir;
        cikarma.OpenCloseObjectAnimation();
        Invoke("Degistir", (demUsul.zaman * ritim) - 0.25f);
    }

    void Rastgele(RectTransform usl, bool demUsulMu)
    {
        Usul yeni = Usüller[UnityEngine.Random.Range(0, this.Usüller.Length)];

        while (yeni == demUsul || yeni == ahirUsul)
        {
            yeni = Usüller[UnityEngine.Random.Range(0, this.Usüller.Length)];
        }
        if (demUsulMu)
        {
            demUsul = yeni;
        }
        else
        {
            ahirUsul = yeni;
        }

        usl.transform.Find("Resim").GetComponent<Image>().sprite = yeni.resim;
        usl.transform.Find("Usül").GetComponent<Text>().text = yeni.name;
    }
}

[System.Serializable]
public class Usul
{
    public string name;
    public Sprite resim;
    public int zaman;
    public int birim;
    public int vurus;
}
