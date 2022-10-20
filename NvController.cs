using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static Unity.VisualScripting.Member;

public class NvController : MonoBehaviour
{
    [SerializeField] private Color defaultLightColor;
    [SerializeField] private Color boostedLightColor;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource source;

    private Renderer mesh;

    private bool isNightVisionEnabled;
    private PostProcessVolume volume;
    private Collider col;

    private void Start()
    {
        RenderSettings.ambientLight = defaultLightColor;        //Reset ambient light
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.weight = 0;  //hide PostFx

        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) //Checking Input
        {
            col.enabled = !col.enabled;
            AudioSource.PlayClipAtPoint(clip, transform.position, 1);
            ToggleNightVision();
        }
    }

    private void ToggleNightVision()
    {
        isNightVisionEnabled = !isNightVisionEnabled;
        if (isNightVisionEnabled)
        {
            RenderSettings.ambientLight = boostedLightColor;
            volume.weight = 1;
        }
        else
        {
            RenderSettings.ambientLight = defaultLightColor;
            volume.weight = 0;
        }
    }

    private void OnTriggerStay(Collider collision)
    {


        if (collision.gameObject.CompareTag("Enemy"))
        {
            mesh = collision.GetComponent<Renderer>();
            mesh.enabled = true;
            Debug.Log("Ghost Visible");

        }
        else
        {
            mesh.enabled = false;
            Debug.Log("Ghost hidden");
        }




    }
}

