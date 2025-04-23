using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Text;
using UnityEngine.UI;
//using Newtonsoft.Json;
using static ServerConnect;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;

public class ServerConnect : MonoBehaviour
{
    [SerializeField] ArmDataListSO armDataListSO;
    //회원가입
    [SerializeField] TMP_InputField registerIdField;
    [SerializeField] TMP_InputField registerpasswdField;
    [SerializeField] TMP_InputField registerEmailField;

    // 로그인
    [SerializeField] TMP_InputField loginIdField;
    [SerializeField] TMP_InputField loginPwField;

    [SerializeField] GameObject uiRegisterPanel;
    [SerializeField] GameObject uiLoginPanel;
    // bool isRegisterComplete =false;
    // bool isLoginComplete =false;
    string[] armName = { "라그나 블레이드 ", "파멸의 도끼", "그림자 숨결" };
    string[] armType = { "검", "도끼", "대거" };
    int[] armAtkVal = { 20, 40, -10 };
    int[] armAtkSpeedVal = { 0, -100, 400 };


    string gameServerUrl = "http://localhost:4416"; // 서버 주소

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void QuickLogin()
    {
        uiRegisterPanel.SetActive(false);
        uiLoginPanel.SetActive(true);
    }
    public async void RegisterInfoSeq()
    {
        if (registerIdField.text == "" || registerEmailField.text == "" || registerpasswdField.text == "")
        {
            return;
        }
        await SetUserInfoAsync(registerIdField.text, registerpasswdField.text, registerEmailField.text);

        for (int i = 0; i < 3; i++)
        {
            await PresentUserItemAsync(armName[i], armType[i], armAtkVal[i], armAtkSpeedVal[i], registerIdField.text);
        }

        uiRegisterPanel.SetActive(false);
        uiLoginPanel.SetActive(true);

    }


    public async void SetCharacterInitialInfo()
    {
        if (loginIdField.text == "" || loginPwField.text == "")
        {
            return;
        }

        await GetUserInfoAsync(loginIdField.text);
    }

    public async UniTask GetUserInfoAsync(string userId)
    {
        string serverTaskUrl = $"{gameServerUrl}/user/{userId}";

        var request = UnityWebRequest.Get(serverTaskUrl);
        await request.SendWebRequest().ToUniTask();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            JsonUser jsonToUser = JsonUtility.FromJson<JsonUser>(request.downloadHandler.text);

            if (loginIdField.text == jsonToUser.user_id && loginPwField.text == jsonToUser.user_pw)
            {
                await GetUserItemAsync(loginIdField.text);
            }

            // 제이슨 뜯어서 아이디 비밀번호 맞으면  아이템 제공하고 씬 넘김
        }
    }

    public async UniTask GetUserItemAsync(string ownerId)
    {
        Debug.Log(ownerId);
        string serverTaskUrl = $"{gameServerUrl}/item/{ownerId}";

        var request = UnityWebRequest.Get(serverTaskUrl);
        await request.SendWebRequest().ToUniTask();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            
            string wrappedJson = "{\"jArmItems\":" + request.downloadHandler.text + "}";


            JsonArmListWrapper result = JsonUtility.FromJson<JsonArmListWrapper>(wrappedJson);

            if (result?.jArmItems != null)
            {
                foreach (var arm in result.jArmItems)
                {
                    JsonArmItem setArmItem =new JsonArmItem(arm.arm_name,arm.arm_type,arm.arm_atk_bonus_val,arm.arm_atkSpeed_bonus_val,arm.owner_id);
                    
                    armDataListSO.armItems.Add(setArmItem);
                }
            }
            else
            {
                Debug.LogWarning("jArmItems is null or empty.");
            }

            await UniTask.Delay(200);
            SceneManager.LoadScene(1);

        }
    }

    public async UniTask SetUserInfoAsync(string user_id, string user_pw, string user_email)
    {
        var userJson = JsonUtility.ToJson(new JsonUser(user_id, user_pw, user_email));
        Debug.Log(userJson);
        var request = new UnityWebRequest($"{gameServerUrl}/user", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(userJson);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest().ToUniTask();

        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError(request.error);
        else
            Debug.Log("아이템 정보: " + request.downloadHandler.text);
    }


    public async UniTask PresentUserItemAsync(string arm_name, string arm_type, int arm_atk_bonus_value, int arm_atkSpeed_bonus_value, string owner_id)
    {
        var userJson = JsonUtility.ToJson(new JsonArmItem(arm_name, arm_type, arm_atk_bonus_value, arm_atkSpeed_bonus_value, owner_id));
        var request = new UnityWebRequest($"{gameServerUrl}/armItem", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(userJson);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest().ToUniTask();

        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError(request.error);
        else
            Debug.Log("유저 등록 결과: " + request.downloadHandler.text);
    }

    [System.Serializable]

    public class JsonUser
    {

        public string user_id;
        public string user_pw;
        public string user_email;


        public JsonUser(string user_id, string user_pw, string user_email)
        {
            this.user_id = user_id;
            this.user_pw = user_pw;
            this.user_email = user_email;

        }

    }
    [System.Serializable]
    public class JsonArmItem
    {
        public string arm_name;
        public string arm_type;
        public int arm_atk_bonus_val;
        public int arm_atkSpeed_bonus_val;
        public string owner_id;

        public JsonArmItem(string arm_name, string arm_type, int arm_atk_bonus_val, int arm_atkSpeed_bonus_val, string owner_id)
        {
            this.arm_name = arm_name;
            this.arm_type = arm_type;
            this.arm_atk_bonus_val = arm_atk_bonus_val;
            this.arm_atkSpeed_bonus_val = arm_atkSpeed_bonus_val;
            this.owner_id = owner_id;
        }

    }

    [System.Serializable]
    public class JsonArmListWrapper
    {
        public JsonArmItem[] jArmItems;
    }
}
