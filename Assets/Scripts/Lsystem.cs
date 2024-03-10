using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class Lsystem : MonoBehaviour
{
    /// <summary>
    /// control
    /// </summary>
    public  Slider lengthSlider;
    public  Slider angleSlider;
    public  Slider branchWidthSlider;
    public  Slider leafLengthSlider;
    public  Slider leafStartWidthSlider;
    public  Slider leafEndWidthSlider;
    public  Slider leafColorSlider;
    public  Slider backgroundColorSlider;

    /// <summary>
    /// Button
    /// </summary>
    public Button treeNumAddButton;
    public Button treeNumMinusButton;

    public Button iratationsAddButton;
    public Button iratationsMinusButton;
    public Button animationButton;
    public Button springButton;
    public Button summerButton;
    public Button autumnButton;
    public Button winterButton;
    public Button branchColorButton;

    /// <summary>
    /// Text
    /// </summary>
    public TextMeshProUGUI iterationText;
    public TextMeshProUGUI treeNumText;
    public TextMeshProUGUI lengthText;
    public TextMeshProUGUI angleText;
    public TextMeshProUGUI branchWidthText;

    public TextMeshProUGUI leafLengthText;
    public TextMeshProUGUI startWidthText;
    public TextMeshProUGUI endWidthText;

    public GameObject _3DText;

    /// <summary>
    /// Lsystem
    /// </summary>
    private string axiom;
    private string currentString;
    private Dictionary<char, string> rules = new Dictionary<char, string>();////keys and values rules
    private Stack<TransformInfo> transformStack = new Stack<TransformInfo>();//stack tp save the position and rotation
    //private bool isGenerating = false

    //[SerializeField] private GameObject leaf;
    [SerializeField] private GameObject[] leaves = new GameObject[4];//leaf type
    [SerializeField] private GameObject[] branch = new GameObject[3];//branch type
    //[SerializeField] private GameObject branch;
    [SerializeField] private GameObject treeParent;//tree parent
    public GameObject tree;//tree

    public bool generateString = true;


    ///colors
    private int colorNum = 0;
    private int branchColorNum = 0;

    /// <summary>
    /// Parameter
    /// </summary>
    public int iterations;
    public float length = 0.8f;
    public float angle = 25.7f;
    public float width = 1.0f;

    public float leafLength = 2f;
    public float leafWidthStart = 1f;
    public float leafWidthEnd = 0.5f;
    private int maxGeneration;
    public int LsystemNums = 1;


    //private bool isGenerating = false;

    void Start()
    {
        //inaialize the parameter
        lengthSlider.value = length;
        angleSlider.value = angle;
        branchWidthSlider.value = width;
        leafLengthSlider.value = leafLength;
        leafStartWidthSlider.value = leafWidthStart;
        leafEndWidthSlider.value = leafWidthEnd;

        //Generation buttons
        iratationsAddButton.onClick.AddListener(OnIterationAdd);
        iratationsMinusButton.onClick.AddListener(OnIterationMinus);

        //select tree buttons
        treeNumAddButton.onClick.AddListener(OnTreeNumAdd);
        treeNumMinusButton.onClick.AddListener(OnTreeNumMinus);

        //animationButton.onClick.AddListener(OnAnimation);

        //select leaf color buttons
        springButton.onClick.AddListener(SelectLeafColorSpring);
        summerButton.onClick.AddListener(SelectLeafColorSummer);
        autumnButton.onClick.AddListener(SelectLeafColorAutumn);
        winterButton.onClick.AddListener(SelectLeafColorWinter);

        // //select branch color buttons
        branchColorButton.onClick.AddListener(ChangeBranchColor);

        //branch value change sliders
        lengthSlider.onValueChanged.AddListener(OnLengthChanged);
        angleSlider.onValueChanged.AddListener(OnAngleChanged);
        branchWidthSlider.onValueChanged.AddListener(OnBranchWidthChanged);
        //leaves value change sliders
        leafLengthSlider.onValueChanged.AddListener(OnLeafLengthChanged);
        leafStartWidthSlider.onValueChanged.AddListener(OnLeafStartWidthChanged);
        leafEndWidthSlider.onValueChanged.AddListener(OnLeafEndWidthChanged);

        if( LsystemNums == 1)
            treeNumMinusButton.interactable = false;

        //initialize the Lsystem
        LsystemNum(1);
        currentString = axiom;
        GenerateString();
        GenerateToTree();
    }
    void FixedUpdate()
    {
        iterationText.text = iterations.ToString();
        treeNumText.text = LsystemNums.ToString();
        lengthText.text = length.ToString("F1");
        angleText.text = angle.ToString("F1");
        branchWidthText.text = width.ToString("F1");
        leafLengthText.text = leafLength.ToString("F1");
        startWidthText.text = leafWidthStart.ToString("F1");
        endWidthText.text = leafWidthEnd.ToString("F1");

        if(Input.GetKeyDown(KeyCode.G))
        {
            if(iterations < maxGeneration)
            {
                OnIterationAdd();
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(iterations > 1)
                OnIterationMinus();
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(length < 4.8)
            {
                OnLengthChanged(length + 0.2f);
                lengthSlider.value = length;
            }
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(length > 0.2)
            {
                OnLengthChanged(length - 0.2f);
                lengthSlider.value = length;
            }
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(angle < 55)
            {
                OnAngleChanged(angle + 5f);
                angleSlider.value = angle;
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(angle > 5)
            {
                OnAngleChanged(angle - 5f);
                angleSlider.value = angle;
            }
        }
    }

    public void LsystemNum(int num)
    {
        switch(num)
        {
            case 1:
                axiom = "F";
                rules.Clear();
                rules.Add('F', "F[+F]F[-F]F");
                iterations = 5;
                length = 1.0f;
                angle = 25.7f;
                maxGeneration = 6;
                leafWidthStart = 0.2f;
                leafWidthEnd = 0.1f;
                break;
            case 2:
                axiom = "F";
                rules.Clear();
                rules.Add('F', "F[+F]F[-F][F]");
                iterations = 5;
                length = 4.0f;
                angle = 20.0f;
                maxGeneration = 6;
                leafWidthStart = 1f;
                leafWidthEnd = 0.1f;
                break;
            case 3:
                axiom = "F";
                rules.Clear();
                rules.Add('F', "FF-[-F+F+F]+[+F-F-F]");
                iterations = 4;
                length = 4.0f;
                angle = 22.5f;
                maxGeneration = 4;
                break;
            case 4:
                axiom = "X";
                rules.Clear();
                rules.Add('X', "F[+X]F[-X]+X");
                rules.Add('F', "FF");
                iterations = 7;
                length = 1f;
                angle = 20.0f;
                maxGeneration = 7;
                break;
            case 5:
                axiom = "X";
                rules.Clear();
                rules.Add('X', "F[+X][-X]FX");
                rules.Add('F', "FF");
                iterations = 7;
                angle = 25.7f;
                length = 1f;
                maxGeneration = 7;
                break;
            case 6:
                axiom = "X";
                rules.Clear();
                rules.Add('X', "F-[[X]+X]+F[+FX]-X");
                rules.Add('F', "FF");
                iterations = 5;
                length = 3.0f;
                angle = 22.5f;
                maxGeneration = 5;
                break;
            case 7:
                axiom = "X";
                rules.Clear();
                rules.Add('X', "F+[-X]-F[-X]+F[-X]+X");
                rules.Add('F', "FF");
                iterations = 6;
                length = 1.2f;
                angle = 35f;
                maxGeneration = 6;
                break;
            case 8:
                axiom = "X";
                rules.Clear();
                rules.Add('X', "F-[FX]+A[X]+X");
                rules.Add('F', "FF");
                iterations = 7;
                length = 1.0f;
                angle = 40.0f;
                maxGeneration = 7;
                break;
            case 9:
                axiom = "F";
                rules.Clear();
                rules.Add('F', "FF[*+F][//-F][*-F][//+F]");
                iterations = 5;
                length = 4f;
                angle = 25.7f;
                maxGeneration = 5;
                break;
        }
            
    }

    public void GenerateString()
    {
        //level = 0;
        currentString = axiom;
        if(generateString)
        {
            for(int k = 0; k < iterations; k++)
            {
                string newString = "";
                char[] stringCharacters = currentString.ToCharArray();
                for(int i = 0; i < stringCharacters.Length; i++)
                {
                    char currentStringCharacter = stringCharacters[i];//current character
                    //if exist the key, replace the value
                    if(rules.ContainsKey(currentStringCharacter))
                        newString += rules[currentStringCharacter];
                    else
                        newString += currentStringCharacter.ToString();
                }
                currentString = newString;
                Debug.Log(currentString);
            }
            generateString = false;
        }    
    }

    public void GenerateToTree()
    {
        Destroy(tree);//clear last time 
        tree = Instantiate(treeParent);//create a new tree


        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        int l=0;


        char[] finalStringCharacters = currentString.ToCharArray();
        //finalStringCharacters = currentString.ToCharArray();
        for(int i = 0; i < finalStringCharacters.Length; i++)
        {
            char currentStringCharacter = finalStringCharacters[i];
            if(currentStringCharacter == 'F')
            {
                //draw the branch
                Vector3 initialPosition = transform.position;
                //if leaf
                if(finalStringCharacters[(i+1) % finalStringCharacters.Length] == ']'|| finalStringCharacters[(i + 1) % finalStringCharacters.Length] == 'X' || finalStringCharacters[(i + 3) % finalStringCharacters.Length] == 'X' || finalStringCharacters[(i + 3) % finalStringCharacters.Length] == 'F' && finalStringCharacters[(i + 4) % finalStringCharacters.Length] == 'X')
                //if(l==level)
                {
                    //if leaf, draw leaf
                    GameObject leafSegment = Instantiate(leaves[colorNum]);
                    transform.Translate(Vector3.forward * leafLength);
                    leafSegment.transform.SetParent(tree.transform);//set the leaf to the tree parent
                    //画出叶子
                    leafSegment.GetComponent<LineRenderer>().startWidth = leafWidthStart;
                    leafSegment.GetComponent<LineRenderer>().endWidth = leafWidthEnd;
                    leafSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    leafSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                }
                else
                {
                    //else draw branch
                    GameObject branchSegment = Instantiate(branch[branchColorNum]);
                    transform.Translate(Vector3.forward * length);
                    branchSegment.transform.SetParent(tree.transform);//set the branch to the tree parent
                    //draw branch
                    branchSegment.GetComponent<LineRenderer>().startWidth = width;
                    branchSegment.GetComponent<LineRenderer>().endWidth = width;
                    branchSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    branchSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                }


            }
            else if(currentStringCharacter == '+')
            {
                transform.Rotate(Vector3.up * -angle);
            }
            else if(currentStringCharacter == '-')
            {

                transform.Rotate(Vector3.up * angle);
            }
            else if(currentStringCharacter == '/')
            {
                transform.Rotate(Vector3.right * -angle);
            }
            else if(currentStringCharacter == '*')
            {
                transform.Rotate(Vector3.right * angle);
            }
            else if(currentStringCharacter == '[')
            {
                TransformInfo ti = new TransformInfo
                {
                    position = transform.position,
                    rotation = transform.rotation
                };
                transformStack.Push(ti);
                l++;
            }
            else if(currentStringCharacter == ']')
            {
                TransformInfo ti = transformStack.Pop();
                transform.position = ti.position;
                transform.rotation = ti.rotation;
            }
            else if(currentStringCharacter == 'X')
            {
               continue;
            }
        }
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    
    public void OnLengthChanged(float value)
    {
        length = value;//
        GenerateToTree();//
    }
    public void OnAngleChanged(float value)
    {
        angle = value;
        GenerateToTree();

    }
    public void OnBranchWidthChanged(float value)
    {
        width = value;
        GenerateToTree();
    }
    public void OnLeafLengthChanged(float value)
    {
        leafLength = value;
        GenerateToTree();
    }
    public void OnLeafStartWidthChanged(float value)
    {
        leafWidthStart = value;
        GenerateToTree();
    }
    public void OnLeafEndWidthChanged(float value)
    {
        leafWidthEnd = value;
        GenerateToTree();
    }


    public void SelectLeafColorSpring()
    {
        colorNum = 0;
        GenerateToTree();
    }
    public void SelectLeafColorSummer()
    {
        colorNum = 1;
        GenerateToTree();
    }
    public void SelectLeafColorAutumn()
    {
        colorNum = 2;
        GenerateToTree();
    }
    public void SelectLeafColorWinter()
    {
        colorNum = 3;
        GenerateToTree();
    }
    public void ChangeBranchColor()
    {
        branchColorNum = (branchColorNum + 1) % branch.Length;
        GenerateToTree();
    }
    public void OnTreeNumAdd()
    {
        if(LsystemNums >= 8)
            treeNumAddButton.interactable = false;
        else
            treeNumMinusButton.interactable = true;
        LsystemNums++;
        if(LsystemNums == 9)
            _3DText.SetActive(true);
        else
            _3DText.SetActive(false);

        generateString = true;
        LsystemNum(LsystemNums);
        GenerateString();
        GenerateToTree();

        lengthSlider.value = length;
        angleSlider.value = angle;
        branchWidthSlider.value = width;
        leafLengthSlider.value = leafLength;
        leafStartWidthSlider.value = leafWidthStart;
        leafEndWidthSlider.value = leafWidthEnd;

    }
    public void OnTreeNumMinus()
    {
        if(LsystemNums <= 2)
            treeNumMinusButton.interactable = false;
        else
            treeNumAddButton.interactable = true;
        LsystemNums--;
        if(LsystemNums !=9)
            _3DText.SetActive(false);
        generateString = true;
        LsystemNum(LsystemNums);
        GenerateString();
        GenerateToTree();

        lengthSlider.value = length;
        angleSlider.value = angle;
        branchWidthSlider.value = width;
        leafLengthSlider.value = leafLength;
        leafStartWidthSlider.value = leafWidthStart;
        leafEndWidthSlider.value = leafWidthEnd;
    }
    public void OnIterationAdd()
    {
        if(iterations >= maxGeneration-1)
            iratationsAddButton.interactable = false;
        else
            iratationsMinusButton.interactable = true;
        iterations++;
        generateString = true;
        GenerateString();
        GenerateToTree();
    }
    public void OnIterationMinus()
    {
        if(iterations <= 2)
            iratationsMinusButton.interactable = false;
        else
            iratationsAddButton.interactable = true;
        iterations--;
        generateString = true;
        GenerateString();
        GenerateToTree();
    }
}
public class LsystemData
{
    public string axiom;
    public Dictionary<char, string> rules = new Dictionary<char, string>();
    public int iterations;
    public float length = 0.8f;
    public float angle = 25.7f;
    public float width = 1.0f;

    public float leafLength = 2f;
    public float leafWidthStart = 1f;
    public float leafWidthEnd = 0.5f;
    private int maxGeneration;
    public int LsystemNums = 1;
}