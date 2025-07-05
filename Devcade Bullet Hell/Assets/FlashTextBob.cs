using UnityEngine;

public class FlashTextBob : MonoBehaviour
{
    [SerializeField] float bobAmplitude;
    [SerializeField] float bobFreq;
    [SerializeField] float bobMidline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float val = bobMidline + (Mathf.Sin(Time.time * bobFreq) * bobAmplitude);

        transform.localScale = new Vector3(val, val, 0);
    }
}
