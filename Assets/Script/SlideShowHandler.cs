using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideShowHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] slides;
    [SerializeField] private float displayTime = 3;
    int currentSlide = 0;
    float lastTurnedTime;
    // Start is called before the first frame update
    void Start()
    {
        StartSlideShow(currentSlide);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTurnedTime + displayTime < Time.time)
        {
            currentSlide += 1;
            currentSlide %= slides.Length;
            TurnSlide();
        }
    }
    public void StartSlideShow(int startIndex)
    {

    }
    public void TurnSlide()
    {
        for (int i = 0; i < slides.Length; i += 1)
        {
            if (i == currentSlide) slides[i].SetActive(true);
            else slides[i].SetActive(false);
        }
        lastTurnedTime = Time.time;
    }
}
