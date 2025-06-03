using UnityEngine;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    [Header("Paneles del menú principal")]
    public CanvasGroup panelMenuPrincipal;
    public CanvasGroup panelSettings;
    public CanvasGroup panelAboutUs;

    [Header("Canvases con CanvasGroup")]
    public CanvasGroup canvasMenus;
    public CanvasGroup canvasScanner;

    [Header("Objetos del scanner")]
    public GameObject scanner;

    [Header("Animación")]
    public float animDuration = 0.5f;
    public Ease easing = Ease.InOutQuad;

    private CanvasGroup panelActual;
    private RectTransform canvasMenusRect;
    private RectTransform canvasScannerRect;
    private float canvasWidth;

    void Start()
    {
        canvasMenusRect = canvasMenus.GetComponent<RectTransform>();
        canvasScannerRect = canvasScanner.GetComponent<RectTransform>();

        // Usamos el ancho del padre (pantalla adaptativa)
        canvasWidth = canvasMenusRect.rect.width;

        // Posiciones iniciales
        canvasMenusRect.anchoredPosition = Vector2.zero;
        canvasScannerRect.anchoredPosition = new Vector2(canvasWidth, 0);

        canvasMenus.gameObject.SetActive(true);
        canvasScanner.gameObject.SetActive(false);

        canvasMenus.alpha = 1;
        canvasMenus.interactable = true;
        canvasMenus.blocksRaycasts = true;

        MostrarPanel(panelMenuPrincipal);

        if (scanner != null)
            scanner.SetActive(false);
    }

    public void MostrarPanel(CanvasGroup nuevoPanel)
    {
        if (panelActual != null && panelActual != nuevoPanel)
        {
            panelActual.DOFade(0, animDuration).SetEase(easing)
                .OnComplete(() => panelActual.gameObject.SetActive(false));
        }

        nuevoPanel.gameObject.SetActive(true);
        nuevoPanel.alpha = 0;
        nuevoPanel.DOFade(1, animDuration).SetEase(easing);

        panelActual = nuevoPanel;
    }

    public void IrASettings() => MostrarPanel(panelSettings);
    public void IrAAboutUs() => MostrarPanel(panelAboutUs);
    public void IrAPanelPrincipal() => MostrarPanel(panelMenuPrincipal);

    public void IrAlScanner()
    {
        scanner.SetActive(true);
        canvasScanner.gameObject.SetActive(true);

        canvasMenusRect.DOAnchorPosX(-canvasWidth, animDuration).SetEase(easing);
        canvasScannerRect.anchoredPosition = new Vector2(canvasWidth, 0); // Reinicio por si acaso
        canvasScannerRect.DOAnchorPosX(0, animDuration).SetEase(easing)
            .OnComplete(() =>
            {
                canvasMenus.gameObject.SetActive(false);

                canvasMenus.interactable = false;
                canvasMenus.blocksRaycasts = false;

                canvasScanner.interactable = true;
                canvasScanner.blocksRaycasts = true;
            });
    }

    public void VolverAlMenu()
    {
        scanner.SetActive(false);
        canvasMenus.gameObject.SetActive(true);

        canvasScannerRect.DOAnchorPosX(canvasWidth, animDuration).SetEase(easing);
        canvasMenusRect.anchoredPosition = new Vector2(-canvasWidth, 0); // lo colocamos a la izquierda
        canvasMenusRect.DOAnchorPosX(0, animDuration).SetEase(easing)
            .OnComplete(() =>
            {
                canvasScanner.gameObject.SetActive(false);

                canvasScanner.interactable = false;
                canvasScanner.blocksRaycasts = false;

                canvasMenus.interactable = true;
                canvasMenus.blocksRaycasts = true;

                MostrarPanel(panelMenuPrincipal);
            });
    }
}
