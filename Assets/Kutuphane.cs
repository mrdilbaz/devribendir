using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kutuphane : MonoBehaviour
{


    public UsulSablon sablon;
    public Usul[] usuller;

    private int usulIndex = 0;


    public AudioSource dum;
    public AudioSource tek;
    public Yonetmen yonetmen;

    public Image tekrarImage;
    public Text tekrarText;

    // Use this for initialization

    public bool tekrar = true;

    public void OnEnable()
    {
        usulIndex = 0;
        Uygula(0);
    }

    public void Vur()
    {
        StopAllCoroutines();
        yonetmen.MetronomDurdur();
        StartCoroutine(UsulVur(usuller[usulIndex]));
    }

    float lastVurus = 0;
    IEnumerator UsulVur(Usul usul)
    {
        yonetmen.MetronomBaslat();
        yield return new WaitForSeconds(yonetmen.ritim * 3);
        
        do
        {
			int i = 0;
            foreach (VurusTipi vurustipi in usul.vuruslar)
            {
                Vurus vurus = sablon.vurusListesi[vurustipi];
                switch (vurus.ses)
                {
                    case VurusSesi.Dum:
                        dum.Play();
                        break;
                    case VurusSesi.Tek:
                        tek.Play();
                        break;
                    case VurusSesi.Hek:
                        dum.Play();
                        tek.Play();
                        break;
                }
                float vurusTime = Time.time;
                //Debug.Log("Vurus:" + vurusTime);
                //Debug.Log("Son vurustan beri:" + (vurusTime - lastVurus));
                lastVurus = vurusTime;
                sablon._vurusObjeleri[i].GetComponent<Image>().color = Color.green;
                float bekleme = yonetmen.ritim * ((usul.birim * 1f) / (vurus.zaman * 1f));
                yield return new WaitForSeconds(bekleme);
                
                sablon._vurusObjeleri[i].GetComponent<Image>().color = Color.white;
                i++;
            }
        } while (tekrar);

        yonetmen.MetronomDurdur();
    }

    public void Degistir(bool ileri)
    {
        StopAllCoroutines();
        yonetmen.MetronomDurdur();
        usulIndex = ileri ? (usulIndex == usuller.Length - 1 ? 0 : usulIndex + 1) :
                            (usulIndex == 0 ? usuller.Length - 1 : usulIndex - 1);

        Uygula(usulIndex);
    }

    void Uygula(int index)
    {
        Usul usul = usuller[index];
        sablon.UsulAdı = usul.name;
        sablon.zaman = usul.zaman;
        sablon.birim = usul.birim;
        sablon.vuruslar = usul.vuruslar;
        sablon.Olustur();
    }

    public void Tekrarla()
    {
        if (tekrar)
        {
            tekrarText.color = new Color(0.15f, 0.15f, 0.15f, 1);
            tekrarImage.color = new Color(0.15f, 0.15f, 0.15f, 1);
            tekrar = false;
        }
        else
        {
            tekrarText.color = Color.white;
            tekrarImage.color = Color.white;
            tekrar = true;
        }
    }


}



