using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MegaScrypt;
using Random = UnityEngine.Random;

public class BreakoutGameController : MonoBehaviour
{
    [Header("MegaScrypt")]
    [SerializeField] private TextAsset programSource;

    [Header("Components")]
    [SerializeField] private GameObject canvasHolder;

    [Header("Prefabs")]
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject rectPrefab;
    [SerializeField] private GameObject textPrefab;

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;


    private Machine _machine;
    private string _program;
    private List<MegaScryptGameObject> _gameObjects;
    private List<object> _parameters;
    private List<TextData> _queuedText;
    private List<Text> _textDisplay;

    #region Unity Functions

    private void Start()
    {
        _machine = new Machine();
        _gameObjects = new List<MegaScryptGameObject>();
        _parameters = new List<object>();
        _queuedText = new List<TextData>();
        _textDisplay = new List<Text>();

        SetupMachine();
        LoadProgram();

        _machine.TryInvoke("Start");
    }

    private void Update()
    {
        _parameters.Clear();
        _parameters.Add(Time.deltaTime);

        _machine.TryInvoke("Update", _parameters);

        DrawTextOnCanvas();
    }

    #endregion

    #region External Functions

    public void CollisionTrigger(GameObject a, GameObject b)
    {
        MegaScryptGameObject gameObjectA = null;
        MegaScryptGameObject gameObjectB = null;

        for (int i = 0; i < _gameObjects.Count; i++)
        {
            if (_gameObjects[i].Target == a)
            {
                gameObjectA = _gameObjects[i];
            }
            else if (_gameObjects[i].Target == b)
            {
                gameObjectB = _gameObjects[i];
            }
        }

        _parameters.Clear();
        _parameters.Add(gameObjectA);
        _parameters.Add(gameObjectB);
        _parameters.Add(Time.deltaTime);

        _machine.TryInvoke("OnCollision", _parameters);
    }

    #endregion

    #region Utility Functions

    private void SetupMachine()
    {
        _machine.Declare(Log);
        _machine.Declare(SpawnCircle);
        _machine.Declare(SpawnRect);
        _machine.Declare(Min);
        _machine.Declare(Max);
        _machine.Declare(GetKey);
        _machine.Declare(PlaySfx);
        _machine.Declare(DestroyObject);
        _machine.Declare(DrawText);

        _machine.Declare(new NativeFunction("Random", (List<object> parameters) =>
        {
            return Random.Range(
                Convert.ToSingle(parameters[0]),
                Convert.ToSingle(parameters[1])
            );
        }));
    }

    private void LoadProgram()
    {
        if (programSource != null)
        {
            _program = programSource.text;
        }

        _machine.Execute(_program);
    }

    private void DrawTextOnCanvas()
    {
        if (_textDisplay.Count < _queuedText.Count)
        {
            for (int i = _textDisplay.Count; i < _queuedText.Count; i++)
            {
                GameObject textDisplay = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, canvasHolder.transform);
                Text text = textDisplay.GetComponent<Text>();
                _textDisplay.Add(text);
            }
        }

        for (int i = 0; i < _textDisplay.Count; i++)
        {
            _textDisplay[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _queuedText.Count; i++)
        {
            TextData textData = _queuedText[i];
            _textDisplay[i].gameObject.transform.localPosition = new Vector3(textData.xPosition, textData.yPosition, 0);
            _textDisplay[i].text = textData.text;

            _textDisplay[i].gameObject.SetActive(true);
        }

        _queuedText.Clear();
    }

    #endregion

    #region Bindings

    private object SpawnCircle(List<object> parameters)
    {
        int radius = (int)parameters[0];
        int x = (int)parameters[1];
        int y = (int)parameters[2];
        int z = 0;
        object prototype = parameters[3];
        string objectColor = (string)parameters[4];

        GameObject circleInstance = Instantiate(circlePrefab);
        circleInstance.transform.position = new Vector3(x, y, z);
        circleInstance.transform.localScale = Vector3.one * radius;

        circleInstance.GetComponent<CollisionNotifier>().SetController(this);

        if (ColorUtility.TryParseHtmlString(objectColor, out Color color))
        {
            circleInstance.GetComponent<MeshRenderer>().material.color = color;
        }

        MegaScryptGameObject obj = new MegaScryptGameObject(circleInstance);
        obj.Declare("prototype", prototype);

        _gameObjects.Add(obj);
        return obj;
    }

    private object SpawnRect(List<object> parameters)
    {
        int width = (int)parameters[0];
        int height = (int)parameters[1];
        int x = (int)parameters[2];
        int y = (int)parameters[3];
        int z = 0;
        object prototype = parameters[4];
        string objectColor = (string)parameters[5];

        GameObject rectInstance = Instantiate(rectPrefab);
        rectInstance.transform.position = new Vector3(x, y, z);
        rectInstance.transform.localScale = new Vector3(width, height, 10);

        if (ColorUtility.TryParseHtmlString(objectColor, out Color color))
        {
            rectInstance.GetComponent<MeshRenderer>().material.color = color;
        }

        MegaScryptGameObject obj = new MegaScryptGameObject(rectInstance);
        obj.Declare("prototype", prototype);

        _gameObjects.Add(obj);
        return obj;
    }

    private object Max(List<object> parameters)
    {
        return Mathf.Max(
            Convert.ToSingle(parameters[0]),
            Convert.ToSingle(parameters[1])
        );
    }

    private object Min(List<object> parameters)
    {
        return Mathf.Min(
            Convert.ToSingle(parameters[0]),
            Convert.ToSingle(parameters[1])
        );
    }

    private object GetKey(List<object> parameters)
    {
        Enum.TryParse((string)parameters[0], out KeyCode key);
        return Input.GetKey(key);
    }

    private object DestroyObject(List<object> paramaters)
    {
        MegaScryptGameObject megObj = (MegaScryptGameObject)paramaters[0];
        GameObject targetObject = megObj.Target;

        for (int i = 0; i < _gameObjects.Count; i++)
        {
            if (targetObject == _gameObjects[i].Target)
            {
                Destroy(targetObject);
                _gameObjects.RemoveAt(i);
                break;
            }
        }

        return null;
    }

    private object PlaySfx(List<object> parameters)
    {
        string clipName = (string)parameters[0];
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == clipName)
            {
                audioSource.clip = clip;
                audioSource.Play();
                break;
            }
        }

        return null;
    }

    private object DrawText(List<object> parameters)
    {
        string text = (string)parameters[0];
        int xPosition = (int)parameters[1];
        int yPosition = (int)parameters[2];

        _queuedText.Add(new TextData
        {
            text = text,
            xPosition = xPosition,
            yPosition = yPosition
        });

        return null;
    }

    private object Log(List<object> paramaters)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var param in paramaters)
        {
            sb.Append(param.ToString() + ", ");
        }

        Debug.Log(sb.ToString());
        return null;
    }

    #endregion

    #region Structs

    private struct TextData
    {
        public string text;
        public int xPosition;
        public int yPosition;
    }

    #endregion
}
