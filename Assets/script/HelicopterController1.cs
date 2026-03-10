using UnityEngine;
using System.Collections;

public class HelicopterController1 : MonoBehaviour
{
    [Header("Positioning")]
    public float startHeight = 30f;      // baþlangýç Y (Inspector deðiþtirilebilir)
    public float groundHeight = 0f;      // zemin Y
    public float delayBeforeLanding = 60f; // saniye
    [Tooltip("Ýniþ hýzý, birim/saniye (MoveTowards)")]
    public float descendSpeed = 2f;

    [Header("Optional visual/rotor")]
    public Transform rotorTransform;     // rotor rotation için opsiyonel
    public float rotorSpeed = 200f;      // RPM tarzý deðer

    [Header("Doors (optional)")]
    public Transform doorRightTransform; // opsiyonel, eðer kapý mesh'i varsa baðla
    public Transform doorLeftTransform;  // opsiyonel
    public Vector3 doorOpenEuler = new Vector3(0, 90, 0); // örnek açýlma rotasyonu
    public float doorOpenTime = 0.5f;

    bool isLanding = false;
    bool doorsOpened = false;

    void Start()
    {
        // Baþlangýç yüksekliðini zorla
        Vector3 p = transform.position;
        p.y = startHeight;
        transform.position = p;

        // Landing'i tetikle
        Invoke(nameof(StartLanding), delayBeforeLanding);
    }

    void StartLanding()
    {
        isLanding = true;
        Debug.Log("HelicopterController: Landing started.");
    }

    void Update()
    {
        // Rotor döndür (varsa)
        if (rotorTransform != null)
        {
            rotorTransform.Rotate(Vector3.up, rotorSpeed * Time.deltaTime, Space.Self);
        }

        if (isLanding)
        {
            Vector3 pos = transform.position;
            // y deðerini hedefe doðru hareket ettir
            pos.y = Mathf.MoveTowards(pos.y, groundHeight, descendSpeed * Time.deltaTime);
            transform.position = pos;

            // eðer yere ulaþtýysa
            if (!doorsOpened && Mathf.Approximately(pos.y, groundHeight))
            {
                doorsOpened = true;
                isLanding = false;
                StartCoroutine(OpenDoorsCoroutine());
                Debug.Log("HelicopterController: Landed.");
            }
        }
    }

    IEnumerator OpenDoorsCoroutine()
    {
        // Eðer kapý transform'larý atandýysa, açma animasyonu yap
        if (doorRightTransform != null)
        {
            Vector3 start = doorRightTransform.localEulerAngles;
            Vector3 target = doorOpenEuler;
            float t = 0f;
            while (t < doorOpenTime)
            {
                t += Time.deltaTime;
                doorRightTransform.localEulerAngles = Vector3.Lerp(start, target, t / doorOpenTime);
                yield return null;
            }
            doorRightTransform.localEulerAngles = target;
        }

        if (doorLeftTransform != null)
        {
            Vector3 start = doorLeftTransform.localEulerAngles;
            Vector3 target = new Vector3(doorOpenEuler.x, -doorOpenEuler.y, doorOpenEuler.z);
            float t = 0f;
            while (t < doorOpenTime)
            {
                t += Time.deltaTime;
                doorLeftTransform.localEulerAngles = Vector3.Lerp(start, target, t / doorOpenTime);
                yield return null;
            }
            doorLeftTransform.localEulerAngles = target;
        }

        // Eðer kapý transform'ý yoksa, sadece log at (PDF'deki "kapýlar açýldý" durumu saðlanmýþ olur)
        Debug.Log("HelicopterController: Doors opened (or simulated).");
        yield break;
    }
}
