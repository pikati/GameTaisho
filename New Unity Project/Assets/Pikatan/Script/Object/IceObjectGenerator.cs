using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceObjectGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject ice;
    [SerializeField]
    private float speed;    //1秒に動かせる距離
    private Vector3 backPosition;
    private Vector3 playerPosition;
    private GameStateController ctrl;
    private GameObject player;
    private GameObject moveObject;
    private GeneratableIceCounter gCtrl;
    private WaterHeightController wCtrl;
    private bool isCreate;
    private PlayerInputManager pManager;
    private BoxCollider bCollider;
    private BoxCollider cbCol;
    private BreakBackGroundIce bIce;
    private float n;
    // Start is called before the first frame update
    void Start()
    {
        ctrl = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        gCtrl = GameObject.Find("GeneratableIceCounter").GetComponent<GeneratableIceCounter>();
        wCtrl = GameObject.Find("WaterHeightController").GetComponent<WaterHeightController>();
        player = GameObject.FindGameObjectWithTag("Player");
        pManager = player.GetComponent<PlayerInputManager>();
        backPosition = GameObject.Find("BackGroundBreakIce").transform.position;
        bIce = GameObject.Find("BackGroundBreakIce").GetComponent<BreakBackGroundIce>();
    }

    // Update is called once per frame
    void Update()
    {
        Create();
        if(isCreate)
        {
            Vector3 newPosition = Vector3.Slerp(moveObject.transform.position, playerPosition, Time.deltaTime * speed);
            newPosition.x = player.transform.position.x + n;
            moveObject.transform.position = newPosition;
            FindObjectOfType<AudioManager>().PlaySound("Bear", 0);
            FindObjectOfType<AudioManager>().PlaySound("IceburgBreak", 0);
            FindObjectOfType<AudioManager>().PlaySound("Thema", 0);
            player.GetComponent<PlayerAnimationController>().StartBark();
           

            if (Mathf.Abs(moveObject.transform.position.z - playerPosition.z) <= 0.1f && Mathf.Abs(moveObject.transform.position.y - playerPosition.y) <= 0.1f)
            {
                isCreate = false;
                ctrl.isProgressed = true;
                bCollider.enabled = true;
                cbCol.enabled = true;
                bCollider = null;
                cbCol = null;
                moveObject = null;
                player.GetComponent<PlayerAnimationController>().EndBark();
                gCtrl.generatableIceQuantity--;
            }
        }
    }

    private void Create()
    {
        if(pManager.isCreate && ctrl.isProgressed)
        {
            if (gCtrl.generatableIceQuantity <= 0) return;
            Vector3 pos = backPosition;
            pos.x = player.transform.position.x;
            pos.y = player.transform.position.y;
            ctrl.isProgressed = false;
            moveObject = Instantiate(ice, pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
            bCollider = moveObject.GetComponent<BoxCollider>();
            bCollider.enabled = false;
            cbCol = moveObject.transform.GetChild(16).gameObject.GetComponent<BoxCollider>();
            cbCol.enabled = false;
            isCreate = false;

            isCreate = true;
            n = player.GetComponent<PlayerManager>().isRight ? 5.0f : -5.0f;
            playerPosition = new Vector3(player.transform.position.x + n, player.transform.position.y - 1.0f, 0.0f);
            bIce.BreakIce();
        }
        
    }
}
