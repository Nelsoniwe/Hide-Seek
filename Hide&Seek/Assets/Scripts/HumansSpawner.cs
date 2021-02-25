using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumansSpawner : MonoBehaviour
{
    public List<GameObject> Skins;

    private List<GameObject> players = new List<GameObject>();

    private GameObject PlayerHunter;
    private GameObject PlayerHider;

    // Start is called before the first frame update
    void Start()
    {
        players.AddRange(GameObject.FindGameObjectsWithTag("Hunter"));
        players.AddRange(GameObject.FindGameObjectsWithTag("HunterPlayer"));
        players.AddRange(GameObject.FindGameObjectsWithTag("Hider"));
        players.AddRange(GameObject.FindGameObjectsWithTag("HiderPlayer"));

        if (PlayerPrefs.GetString("Skin Selected") == "")
        {
            PlayerPrefs.SetString("Skin Selected", Skins[0].GetComponentInChildren<SkinParams>().NameSkin);
        }


        for (int i = 0; i < players.Count; i++)
        {
            GameObject stepCopy = Instantiate(Skins[Random.Range(0, Skins.Count)], new Vector3(players[i].transform.position.x, players[i].transform.position.y - 0.7f, players[i].transform.position.z), players[i].transform.rotation) as GameObject;

            Transform Body = ChildObjManager.GetChildObject(stepCopy.transform, "Body");

            var outline = Body.gameObject.AddComponent<cakeslice.Outline>();

            stepCopy.transform.parent = players[i].transform;
        }
        StartCoroutine(PlayerSkinChanger());
    }

    IEnumerator PlayerSkinChanger()
    {
        if (StaticField.ChoosedPlay != ChoosePlay.none)
        {
            if (StaticField.ChoosedPlay == ChoosePlay.hide)
            {
                //PlayerHider = GameObject.FindGameObjectsWithTag("HiderPlayer")[0];

                //Transform OldSkin = ChildObjManager.GetChildObject(PlayerHider.transform, "Skin");
                //Destroy(OldSkin.gameObject);

                //GameObject stepCopy = Instantiate(FindSkinInList(Skins), new Vector3(PlayerHider.transform.position.x, PlayerHider.transform.position.y - 0.7f, PlayerHider.transform.position.z), PlayerHider.transform.rotation) as GameObject;
                //Transform Body = ChildObjManager.GetChildObject(stepCopy.transform, "Body");
                //var outline = Body.gameObject.AddComponent<cakeslice.Outline>();
                //stepCopy.transform.parent = PlayerHider.transform;
                SkinChanger("HiderPlayer");

            }
            else if (StaticField.ChoosedPlay == ChoosePlay.seek)
            {
                //PlayerHunter = GameObject.FindGameObjectsWithTag("HunterPlayer")[0];
                //Transform OldSkin = ChildObjManager.GetChildObject(PlayerHider.transform, "Skin");
                //Destroy(OldSkin.gameObject);

                //GameObject stepCopy = Instantiate(FindSkinInList(Skins), new Vector3(PlayerHider.transform.position.x, PlayerHider.transform.position.y - 0.7f, PlayerHider.transform.position.z), PlayerHider.transform.rotation) as GameObject;
                //Transform Body = ChildObjManager.GetChildObject(stepCopy.transform, "Body");
                //var outline = Body.gameObject.AddComponent<cakeslice.Outline>();
                //stepCopy.transform.parent = PlayerHider.transform;
                SkinChanger("HunterPlayer");
            }
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(PlayerSkinChanger());
        }
    }

    private void SkinChanger(string skinName)
    {
        GameObject Skin = GameObject.FindGameObjectsWithTag(skinName)[0];
        Transform OldSkin = ChildObjManager.GetChildObject(Skin.transform, "Skin");
        Destroy(OldSkin.gameObject);

        GameObject stepCopy = Instantiate(FindSkinInList(Skins), new Vector3(Skin.transform.position.x, Skin.transform.position.y - 0.7f, Skin.transform.position.z), Skin.transform.rotation) as GameObject;
        Transform Body = ChildObjManager.GetChildObject(stepCopy.transform, "Body");
       
        var outline = Body.gameObject.AddComponent<cakeslice.Outline>();
        outline.color = 2;
        stepCopy.transform.parent = Skin.transform;
    }



    public GameObject FindSkinInList(List<GameObject> skins)
    {
        foreach (var item in skins)
        {
            if (item.GetComponentInChildren<SkinParams>().NameSkin == PlayerPrefs.GetString("Skin Selected"))
            {
                return item;
            }
        }
        return null;
    }


}
