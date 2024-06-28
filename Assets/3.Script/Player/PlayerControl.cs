using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 10f;
    public CharacterController playerCTRL;
    public CharacterType characterType;
    // Start is called before the first frame update
    void Start()
    {
        playerCTRL = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    public void InputHandler()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z) * speed * Time.deltaTime;
        playerCTRL.Move(movement);
        //transform.Translate(movement, Space.Self);
        characterSkill();
    }

    private void characterSkill()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (characterType)
            {
                case CharacterType.a:
                    Debug.Log("a skilled");
                    break;
                case CharacterType.b:
                    Debug.Log("b skilled");
                    break;
                case CharacterType.c:
                    Debug.Log("c skilled");
                    break;

            }
        }
    }

        

}
