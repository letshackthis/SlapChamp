#if UNITY_EDITOR
using System.Collections;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Networking;

class MyCustomBuildProcessor : MonoBehaviour, IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    private string chat_id = "-788170866";
    private string TOKEN = "5020120212:AAFPSji6vNTv3yXaL7Xg-SYLmnLs-kJBsM8";
    public string API_URL
    {
        get
        {
            return string.Format("https://api.telegram.org/bot{0}/", TOKEN);
        }
    }

    public void OnPostprocessBuild(BuildReport report)
    {

        // Debug.Log("MyCustomBuildProcessor.OnPostprocessBuild for target " + report.summary.platform + 
        //           " at path " + report.summary.outputPath);

        var bytes = File.ReadAllBytes(report.summary.outputPath);
            
            string buildType = "";
            buildType = report.summary.outputPath.Contains("apk") ? "Build type: Test\n" : "Build type: Release\n";
            
            SendFile(bytes, report.summary.outputPath.Split('/').Last(),
                buildType + "Product name: " + Application.productName + "\nVersion: " + Application.version);
            
           // "\nBuild by: " + CloudProjectSettings.userName
    }


    public new void SendMessage(string text)
    {
        WWWForm form = new WWWForm();
        form.AddField("chat_id", chat_id);
        form.AddField("text", text);
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "sendMessage?", form);
        www.SendWebRequest();
    }
    
    public void SendFile(byte[] bytes, string filename, string caption = "")
    {
        WWWForm form = new WWWForm();
        form.AddField("chat_id", chat_id);
        form.AddField("caption", caption);
        form.AddBinaryData("document", bytes, filename, "filename");
        UnityWebRequest www = UnityWebRequest.Post(API_URL + "sendDocument?", form);
        www.SendWebRequest();
    }
}
#endif

