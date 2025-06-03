using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ScreenRecorder : MonoBehaviour
{
    public Button compartirBtn;

    private void Start()
    {
        compartirBtn.onClick.AddListener(CompartirPantalla);
    }

    public void CompartirPantalla()
    {
        StartCoroutine(CapturarYPasarACompartir());
    }

    IEnumerator CapturarYPasarACompartir()
    {
        yield return new WaitForEndOfFrame();

        Texture2D captura = ScreenCapture.CaptureScreenshotAsTexture();

        string filePath = Path.Combine(Application.temporaryCachePath, "captura.png");
        File.WriteAllBytes(filePath, captura.EncodeToPNG());

        Destroy(captura); // Liberar memoria

        new NativeShare().AddFile(filePath)
                         .SetSubject("�Mira que ch�vereee!")
                         .SetText("Hice esto desde mi aplicaci�n de AR.")
                         .SetTitle("Compartir Captura")
                         .Share();
    }
}
