using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yonetmen : MonoBehaviour
{

    [HeaderAttribute("Usüller")]
    public Usul[] Usüller;
    [HeaderAttribute("Animasyonlar")]
    public EasyTween getirme;
    public EasyTween götürme;
    public EasyTween cikarma;
    public EasyTween gizleme;

    [HeaderAttribute("Arayüz Referansları")]
    public RectTransform dem;
    public RectTransform ahir;

    public Text geriSayım;
    public Text kere;


    [HeaderAttribute("Ritim Değeri")]
    public float ritim;
    private Usul demUsul;
    private Usul ahirUsul;


    [HeaderAttribute("Metronom")]
    public AudioSource metronom;


    void Awake()
    {
        dem.gameObject.SetActive(false);
        ahir.gameObject.SetActive(false);
        geriSayım.gameObject.SetActive(true);
        kere.gameObject.SetActive(false);
    }
    IEnumerator Start()
    {
        TikTak();
        geriSayım.text = "3";
        yield return new WaitForSeconds(ritim);
        TikTak();
        geriSayım.text = "2";
        yield return new WaitForSeconds(ritim);
        TikTak();
        geriSayım.text = "1";
        yield return new WaitForSeconds(ritim);
        TikTak();
        geriSayım.gameObject.SetActive(false);
        Rastgele(dem, true);
        Rastgele(ahir, false);
        int kereInt = UnityEngine.Random.Range(1,4);
        kere.text = kereInt + " kere";
        dem.gameObject.SetActive(true);
        ahir.gameObject.SetActive(true);
        kere.gameObject.SetActive(true);
        Invoke("Degistir", (demUsul.zaman * ritim * kereInt) - 0.25f);
        InvokeRepeating("TikTak", ritim, ritim);
    }

    void TikTak()
    {
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
        gizleme.OpenCloseObjectAnimation();
        Invoke("Cikar", 0.25f);
    }

    void Cikar()
    {
        RectTransform tmp = dem;
        dem = ahir;
        ahir = tmp;
        demUsul = ahirUsul;
        Rastgele(ahir, false);
        cikarma.ChangeSetState(false);
        cikarma.rectTransform = ahir;
        cikarma.OpenCloseObjectAnimation();
        int kereInt = UnityEngine.Random.Range(1,4);
        kere.text = kereInt + " kere";
        Invoke("Degistir", (demUsul.zaman * ritim * kereInt) - 0.25f);
        gizleme.OpenCloseObjectAnimation();
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
