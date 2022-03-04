using Customisation;
using Customisation.SO;
using UnityEngine;
using UnityEngine.UI;

public class OnlineOptions : MonoBehaviour
{
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private GameObject upgradeButtons;
    [SerializeField] private Text playerNickname;
    [SerializeField] private Text enemyNickname;
    [SerializeField] private CharacterChannel characterChannel;

    private string[] offlineNickname = new[]
    {
        "LaLa", "Hawk", "Popeye", "Princess", "Rainbow", "Chico", "Spud", "Pinkie", "Grease",
        "Braniac", "Gams", "Pinata", "Boo Bug", "Dirty Harry", "Dracula", "Shrinkwrap", "Guy", "Darling", "Naruto",
        "Sasuke", "Bodji", "LaLa",
        "Fike", "Twinkly", "Joker", "Raindrop", "Dear", "Ami", "Thor", "Dingo", "Spicy", "Tarzan", "Schnookums",
        "Dearey",
        "Tyke", "Bean",
        "Genius", "Rabbit", "Cottonball", "Ducky", "Pintsize", "Noctur", "Bro", "Depj", "Knucklebutt", "Mappg", "Druid",
        "Daddy", "Forgot", "Nothing",
        "Anonymous", "YourBaby", "Senorita", "Sugar", "Gator", "Stefan", "Angel", "Demon", "Desonans", "SweetMilk",
        "CCl",
        "Vladimir", "ANTON",
        "StAr", "HEHHE", "WhoKnow"
    };

    private string[] onlineNickname = new[]
    {
        "3D Waffle",
        "Hightower",
        "Papa Smurf",
        "57 Pixels",
        "Hog Butcher",
        "Pepper Legs",
        "101",
        "Houston",
        "Pinball Wizard",
        "Accidental",
        "Hyper",
        "Pluto",
        "Alpha",
        "Jester",
        "Pogue",
        "Jigsaw",
        "Prometheus",
        "Bearded Angler",
        "Joker's Grin",
        "Psycho Thinker",
        "Beetle King",
        "Judge",
        "Pusher",
        "Bitmap",
        "Junkyard Dog",
        "Riff Raff",
        "Blister",
        "K-9",
        "Roadblock",
        "Bowie",
        "Keystone",
        "Rooster",
        "Bowler",
        "Kickstart",
        "Sandbox",
        "Breadmaker",
        "Kill Switch",
        "Scrapper",
        "Broomspun",
        "Kingfisher",
        "Screwtape",
        "Buckshot",
        "Kitchen",
        "Sexual Chocolate",
        "Bugger",
        "Knuckles",
        "Shadow Chaser",
        "Cabbie",
        "Lady Killer",
        "Sherwood Gladiator",
        "Candy Butcher",
        "Liquid Science",
        "Shooter",
        "Capital F",
        "Little Cobra",
        "Sidewalk Enforcer",
        "Captain Peroxide",
        "Little General",
        "Skull Crusher",
        "Celtic Charger",
        "Lord Nikon",
        "Sky Bully",
        "Demo",
        "Voodoo Queen",
        "Serendipity",
        "Miss Lucky"
    };

    private void Awake()
    {
        if (ES3.Load(SaveKeys.IsOnline, false))
        {
            currentLevel.SetActive(false);
            upgradeButtons.SetActive(false);
            enemyNickname.text = onlineNickname[Random.Range(0, onlineNickname.Length)];
        }
        else
        {
            enemyNickname.text = "[AI] " + offlineNickname[Random.Range(0, offlineNickname.Length)];
        }

        characterChannel.OnLoadCharacterData += OnLoadCharacterData;
    }

    private void OnDestroy()
    {
        characterChannel.OnLoadCharacterData -= OnLoadCharacterData;
    }

    private void OnLoadCharacterData(CharacterLoadSave obj)
    {
        playerNickname.text = obj.CharacterSaveData.nickName;
    }
}