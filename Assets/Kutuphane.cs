using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kutuphane : MonoBehaviour
{


    public UsulSablon sablon;
    public Usul[] usuller;

    private int usulIndex = 0;
    private int vurusIndex = 0;

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

    public void OnDisable(){
        CancelInvoke("Darp");
    }

    public void Vur()
    {
        yonetmen.MetronomDurdur();
        CancelInvoke("Darp");
        yonetmen.MetronomBaslat();
        vurusIndex = 0;
        Invoke("Darp", yonetmen.ritim*2);
    }

    float lastVurus = 0;
    public void Darp(){
        VurusTipi vurustipi = usuller[usulIndex].vuruslar[vurusIndex];
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
        //Debug.Log(Time.time);
        sablon._vurusObjeleri[vurusIndex].GetComponent<Image>().color = Color.green;
        int oncekiVurus = vurusIndex == 0 ? usuller[usulIndex].vuruslar.Length-1 : vurusIndex -1;
        sablon._vurusObjeleri[oncekiVurus].GetComponent<Image>().color = Color.white;

        
        Debug.Log(Time.time - lastVurus);
        lastVurus = Time.time;

        if(vurusIndex == usuller[usulIndex].vuruslar.Length - 1){
            if(tekrar){
                float bekleme = yonetmen.ritim * ((usuller[usulIndex].birim * 1f) / (vurus.zaman * 1f));
                Invoke("Darp", bekleme);
            } else {
                Invoke("Sondur",yonetmen.ritim);
                yonetmen.MetronomDurdur();
            }
        } else {
            float bekleme = yonetmen.ritim * ((usuller[usulIndex].birim * 1f) / (vurus.zaman * 1f));
            Invoke("Darp", bekleme);
        }   

        vurusIndex = vurusIndex == usuller[usulIndex].vuruslar.Length - 1 ? 0 : vurusIndex + 1;   
    }

    public void Sondur(){
        sablon._vurusObjeleri[usuller[usulIndex].vuruslar.Length - 1].GetComponent<Image>().color = Color.white;
    }

    public void Degistir(bool ileri)
    {
        CancelInvoke("Darp");
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



