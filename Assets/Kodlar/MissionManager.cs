using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    #region Görevler tamamlandý mý
        [Tooltip("Kasabaya ulaþ.")]
        public bool GetToTheVillage;//Implemented
        [Tooltip("Ýnsanlarýn garip davranýþlarýný gözlemle.")]
        public bool InspectWeirdBehaviours;
        [Tooltip("Ýlk zombiyi etkisiz hale getir.")]
        public bool NeutrializeFirstZombie;
        [Tooltip("Evine ulaþ.")]
        public bool GetToHome;//Implemented
        [Tooltip("Kasabanýn farklý bölgelerinden erzak topla.")]
        public bool CollectGoodsFromTheVillage;
        [Tooltip("Sarah ile tanýþ ve onun güvenini kazan.")]
        public bool EarnSarahsTrust;
        [Tooltip("Mark’ýn güvenini kazan.")]
        public bool EarnMarksTrust;
        [Tooltip("Dr. Brooks’un sinyalini takip et.")]
        public bool FollowDrBrooksSignal;
        [Tooltip("Laboratuvara ulaþ ve Dr. Brooks’u kurtar.")]
        public bool SaveDrBrooks;
        [Tooltip("Alfa Zombi’nin izini sür.")]
        public bool TraceAlphaZombie;
        [Tooltip("Eski askeri üste virüsle ilgili belgeler bul.")]
        public bool FindVirusDocuments;//Implemented
        [Tooltip("Alfa Zombi’nin saldýrýlarýndan kurtul.")]
        public bool DodgeAlphaZombie;
        [Tooltip("Alfa Zombi’yi etkisiz hale getir.")]
        public bool NeutrializeAlphaZombie;
        [Tooltip("Grubun geri kalanýný kurtar.")]
        public bool SaveTheRestOfTheGroup;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Bölgeyi (örnek olarak kasaba veya ev) temsil eden bir collider oluþturulacak ve oyuncunun içine girip girmediðine bakýlacak.
    #region Ulaþma Görevleri
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ev"))
        {
            GetToHome = true;
        }
        if (other.CompareTag("kasaba"))
        {
            GetToTheVillage = true;
        }
    }
    #endregion
}
