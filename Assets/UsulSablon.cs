using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(UsulSablon))]
public class UsulSablonEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (GUILayout.Button("Olustur"))
        {
            ((UsulSablon)target).Olustur();
        }
    }
}

#endif


public class UsulSablon : MonoBehaviour
{
    [HeaderAttribute("Gerekli Seyler")]
    public RectTransform ustCizgi;
    public RectTransform altCizgi;
    public Text zamanText;
    public Text birimText;
    public Text usulText;
    public Image vurusSablon;
    public Vurus[] vurusBilgileri;



    [HeaderAttribute("Usul Bilgileri")]
    public string UsulAdı;
    public int birim;
	public int zaman;
    public VurusTipi[] vuruslar;


    private Dictionary<VurusTipi, Vurus> vurusListesi = new Dictionary<VurusTipi, Vurus>();
    private List<GameObject> _vurusObjeleri = new List<GameObject>();
    void ListeyiOlustur()
    {
        vurusListesi = new Dictionary<VurusTipi, Vurus>();
        foreach (Vurus v in vurusBilgileri)
        {
            vurusListesi.Add(v.vurus, v);
        }
    }

    const int baslangic = 50;
    const int bitis = 310;

    [ExecuteInEditMode]
    public void Olustur()
    {

        ListeyiOlustur();

        foreach(GameObject v in _vurusObjeleri){
            DestroyImmediate(v);
        }

        _vurusObjeleri.Clear();

		int aralik = (bitis - baslangic) / ( vuruslar.Length+1);
        int pozisyon = baslangic;
		

        foreach (VurusTipi v in vuruslar)
        {
            GameObject vrs = GameObject.Instantiate(vurusSablon.gameObject);

            if (v.ToString().StartsWith("Dum") || v.ToString().StartsWith("Hek"))
            {
				pozisyon += aralik / 2;
                vrs.transform.SetParent(ustCizgi);
            }
            else
            {
                vrs.transform.SetParent(altCizgi);
            }

            vrs.name = v.ToString();

            vrs.GetComponent<Image>().sprite = vurusListesi[v].sprite;

            vrs.transform.localScale = Vector3.one;





            vrs.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            vrs.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            
            if(v.ToString().StartsWith("Hek"))
            {
                vrs.GetComponent<RectTransform>().pivot = new Vector2(vurusListesi[v].sprite.pivot.x / 128,
                                                                      vurusListesi[v].sprite.pivot.y / 256);
                vrs.GetComponent<RectTransform>().anchoredPosition = new Vector2(pozisyon, 0);
                vrs.GetComponent<RectTransform>().sizeDelta = new Vector2(64,128);

            }
            else
            {
                vrs.GetComponent<RectTransform>().pivot = vurusListesi[v].sprite.pivot / 128;
                vrs.GetComponent<RectTransform>().anchoredPosition = new Vector2(pozisyon, 0);
                vrs.GetComponent<RectTransform>().sizeDelta = new Vector2(64,90);
            }
			

            pozisyon += v.ToString().StartsWith("Dum") ? aralik / 2 : (aralik * 1) / 1;
            //pozisyon += aralik;// / 2;
            _vurusObjeleri.Add(vrs);
        }

		birimText.text = birim.ToString();
		zamanText.text = zaman.ToString();
        usulText.text = UsulAdı;
    }

}

[System.Serializable]
public class Vurus
{
    public string name
    {
        get
        {
            return vurus.ToString();
        }
    }
    public VurusTipi vurus;
    public Sprite sprite;
}

public enum VurusTipi
{
    Dum2,
    Dum4,
    Dum8,
    Dum16,
    Tek2,
    Tek4,
    Tek8,
    Tek16,
    Hek4,
    Hek8

}
