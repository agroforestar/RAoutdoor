using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject loadingPrefab;
    public float timeToWait = 0.01f;

    private Image _backgroundImage;
    private Text _text;
    private float _currentAlpha = 0;
    private GameObject _currentPanel;
    private Global.LoadingType _type;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Global.loading = this;
    }

    public void startShowPanel(Global.LoadingType type)
    {
        _type = type;
        StartCoroutine("ShowPanel");
    }

    private IEnumerator ShowPanel()
    {
        InstanciateLoadingPanel();
        _backgroundImage.color = new Color(0, 0, 0, 0);
        _text.color = new Color(1, 1, 1, 0);
        while (_currentAlpha < 1)
        {
            yield return new WaitForSeconds(timeToWait);
            _currentAlpha += 0.5f;
            _backgroundImage.color = new Color(0, 0, 0, _currentAlpha);
            _text.color = new Color(1, 1, 1, _currentAlpha);
        }
        DoAction();
    }

    private IEnumerator HidePanel()
    {
        while (_currentAlpha > 0)
        {
            yield return new WaitForSeconds(timeToWait);
            _currentAlpha -= 0.5f;
            _backgroundImage.color = new Color(0, 0, 0, _currentAlpha);
            _text.color = new Color(1, 1, 1, _currentAlpha);
        }
        Destroy(_currentPanel);
    }

    private void InstanciateLoadingPanel()
    {
        _currentPanel = Instantiate(loadingPrefab) as GameObject ;
        if (_currentPanel == null) return;

        _backgroundImage = _currentPanel.transform.Find("Image").GetComponent<Image>();
        _text = _currentPanel.transform.Find("Text").GetComponent<Text>();
        DontDestroyOnLoad(_currentPanel);
    }

    private void DoAction()
    {
        switch (_type)
        {
            case Global.LoadingType.LoadLevel1:
                SceneManager.LoadScene("main");
                break;
        }
        StartCoroutine("HidePanel");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
