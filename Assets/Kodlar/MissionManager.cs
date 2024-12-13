using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    #region G�revler tamamland� m�
        [Tooltip("Kasabaya ula�.")]
        public bool GetToTheVillage;//Implemented
        [Tooltip("�nsanlar�n garip davran��lar�n� g�zlemle.")]
        public bool InspectWeirdBehaviours;
        [Tooltip("�lk zombiyi etkisiz hale getir.")]
        public bool NeutrializeFirstZombie;
        [Tooltip("Evine ula�.")]
        public bool GetToHome;//Implemented
        [Tooltip("Kasaban�n farkl� b�lgelerinden erzak topla.")]
        public bool CollectGoodsFromTheVillage;
        [Tooltip("Sarah ile tan�� ve onun g�venini kazan.")]
        public bool EarnSarahsTrust;
        [Tooltip("Mark��n g�venini kazan.")]
        public bool EarnMarksTrust;
        [Tooltip("Dr. Brooks�un sinyalini takip et.")]
        public bool FollowDrBrooksSignal;
        [Tooltip("Laboratuvara ula� ve Dr. Brooks�u kurtar.")]
        public bool SaveDrBrooks;
        [Tooltip("Alfa Zombi�nin izini s�r.")]
        public bool TraceAlphaZombie;
        [Tooltip("Eski askeri �ste vir�sle ilgili belgeler bul.")]
        public bool FindVirusDocuments;//Implemented
        [Tooltip("Alfa Zombi�nin sald�r�lar�ndan kurtul.")]
        public bool DodgeAlphaZombie;
        [Tooltip("Alfa Zombi�yi etkisiz hale getir.")]
        public bool NeutrializeAlphaZombie;
        [Tooltip("Grubun geri kalan�n� kurtar.")]
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

    //B�lgeyi (�rnek olarak kasaba veya ev) temsil eden bir collider olu�turulacak ve oyuncunun i�ine girip girmedi�ine bak�lacak.
    #region Ula�ma G�revleri
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
