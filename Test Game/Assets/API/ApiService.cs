
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class ApiService
{

    public static JObject getRequest(string api)
    {
        return request(false, ConstantAPI.GET_METHOD, api, null);
    }

    public static JObject postRequest(string api, JObject body)
    {
        return request(false, ConstantAPI.POST_METHOD, api, body);
    }

    public static JObject request(bool isLogin, string method, string apiPath, JObject body)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.String.Format(ConstantAPI.API_BASE_PATH + apiPath));
        request.Method = method;
        request.ContentType = "application/json";
        request.Headers.Add("Secret-Key", ConstantAPI.SECRET_KEY);
        if (!isLogin)
        {
            request.Headers.Add("Access-Token", PlayerPrefs.GetString(ConstantPref.PLAYER_PREFS_ACCESS_TOKEN));
            request.Headers.Add("User-Id", PlayerPrefs.GetString(ConstantPref.PLAYER_PREFS_USER_ID));
        }

        if (method == ConstantAPI.POST_METHOD)
        {
            string stringData = body.ToString(); //place body here

            var data = Encoding.ASCII.GetBytes(stringData); // or UTF8
            request.ContentLength = data.Length;

            var newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        var jsonResponse = reader.ReadToEnd();

        return JObject.Parse(jsonResponse);
    }

    // for login 
    public static JObject authLogin(string loginID, string password)
    {
        string apipath = ConstantAPI.API_LOGIN;
        JObject body = new JObject();
        body.Add("loginId", loginID);
        body.Add("password", password);
        return request(true, ConstantAPI.POST_METHOD, apipath, body);
    }

    // get
    public static JObject getGameList()
    {
        string apipath = ConstantAPI.API_GAME + "games/dice";
        return getRequest(apipath);
    }

    public static JObject getGameDetail(string gameUUID)
    {
        string apipath = ConstantAPI.API_GAME + "gameDetail/" + gameUUID;
        return getRequest(apipath);
    }

    // post
}
