using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;
    public Transform tf;
    public bool showmsg = false;
    public int currentFace = 6;
    public Text path;
    // Use this for initialization
    void Start () {
        

        
        path.text = PlayerPrefs.GetString("PlayerName", "empty");

        /*
        string FullFilePath = Application.persistentDataPath + "/" + "token" + ".bin";
        if (File.Exists(FullFilePath))
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(FullFilePath, FileMode.Open);
            object obj = Formatter.Deserialize(fileStream);
            fileStream.Close();
            // Return the uncast untyped object.
            Debug.Log(FullFilePath);
            Debug.Log(obj);
            path.text = obj.ToString();
        }
        else
        {
            // We must create a new Formattwr to Serialize with.
            BinaryFormatter Formatter = new BinaryFormatter();
            // Create a streaming path to our new file location.
            FileStream fileStream = new FileStream(FullFilePath, FileMode.Create);
            // Serialize the objedt to the File Stream
            Formatter.Serialize(fileStream, jsonResponse);
            // FInally Close the FileStream and let the rest wrap itself up.
            fileStream.Close();
            Debug.Log("Saved File");
        }
        */
    }
	
	// Update is called once per frame
	void Update () {
       

        if (rb.IsSleeping())
        {
            if (showmsg)
            {
                showmsg = false;
                Vector3[] faces = { -Vector3.up, Vector3.right, -Vector3.forward, Vector3.forward, -Vector3.right, Vector3.up };
                int ans = -1;
                for (int i = 0; i < faces.Length; i++)
                {
                    if (Vector3.Dot(Vector3.up, transform.TransformDirection(faces[i])) > 0.8f)
                    {
                        ans = i + 1;
                    }

                }
                currentFace = ans;
                Debug.Log(ans);
                try {
                    var jsonResponse = ApiService.authLogin("admin", "1q2w3e4r");

                    Debug.Log(jsonResponse);
                    PlayerPrefs.SetString("PlayerName", jsonResponse.ToString());
                    path.text = PlayerPrefs.GetString("PlayerName", "empty");
                } catch (System.Exception e) {
                    path.text = e.ToString();
                }
                
            }

            if (Input.touchCount>0 || Input.GetKey(KeyCode.Space))
            {
                showmsg = true;
                rb.AddForce(0, 15, 0, ForceMode.VelocityChange);

                int target = 1;
                if (target == 1) {
                    rb.AddTorque(Random.Range(-50,50), Random.Range(-50, 50), Random.Range(-50, 50));
                }
            }

        }
        
    }
}
