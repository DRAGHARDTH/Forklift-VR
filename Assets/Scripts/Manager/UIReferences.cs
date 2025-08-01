using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIReferences : MonoBehaviour
{
    // === MAIN MENU UI ===
    [Header("Main Menu UI")]
    public GameObject canvas_MainMenu;
    public Button btn_StartButton;
    public Button btn_ExitButton;


    // === GENERAL TOOLTIP UI ===
    [Header("General Tooltip UI")]
    public GameObject canvas_GeneralToolTip;
    public TextMeshProUGUI txt_GeneralToolTip;
    public Button btn_ActionButton1;
    public Button btn_ActionButton2;
    public TextMeshProUGUI txt_ActionButton1Text;
    public TextMeshProUGUI txt_ActionButton2Text;


    // === OBJECT TOOLTIP UI ===
    [Header("Object Tooltip UI")]
    public GameObject canvas_ObjectTooltip;
    public TextMeshProUGUI txt_ObjectToolTip;


    public static UIReferences Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        { 
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
