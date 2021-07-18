using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public RawImage imageDisplay, commentImage;
    public List<Texture> textures = new List<Texture>();
    public List<Texture> biggertexture = new List<Texture>();
    public List<AudioClip> audioclips = new List<AudioClip>();
    public Texture commenttexture;
    public AudioSource audio;
    public bool playTTS = false;
    private int texturecnt = 0;
    private bool inComment = false, Zoomed = false, engaged = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayNextImage(){
        if (textures.Capacity == 0 || engaged) return;
        ++texturecnt;
        imageDisplay.texture = textures[texturecnt % textures.Capacity];
        if (playTTS) audio.PlayOneShot(audioclips[texturecnt % textures.Capacity]);
        //if (playTTS) audio.Play();
    }

    public void DisplayPrevImage(){
        if (texturecnt <= 1 || engaged) return;
        --texturecnt;
        imageDisplay.texture = textures[texturecnt % textures.Capacity];
        //audio.clip = audioclips[texturecnt % textures.Capacity];
        //if (playTTS) audio.Play();
        if (playTTS) audio.PlayOneShot(audioclips[texturecnt % textures.Capacity]);
    }

    public void reComment()
    {
        if (engaged) return;
        if (!inComment)
        {
            imageDisplay.texture = commenttexture;
            inComment = true;
        }
        else
        {
            inComment = false;
            if (texturecnt == 0) return;
            imageDisplay.texture = textures[texturecnt % textures.Capacity];
        }
    }

    public void ZoomIn()
    {
        engaged = true;
        imageDisplay.texture = biggertexture[texturecnt % textures.Capacity];
        Zoomed = true;
    }

    public void ZoomOut()
    {
        engaged = false;
        imageDisplay.texture = textures[texturecnt % textures.Capacity];
        Zoomed = false;
    }

    public void Zoom()
    {
        if (Zoomed) ZoomOut(); else ZoomIn();
    }

    public void VoiceInput(bool status)
    {
        engaged = status;
        if (!inComment) return;
        commentImage.enabled = !status;
    }
}
