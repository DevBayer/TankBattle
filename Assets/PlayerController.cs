using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour {
      public float power = 15;
         public float maxspeed = 20;
         public float turnpower = 1;
         public float friction = 3;
         public Vector2 curspeed ;
         Rigidbody2D rigidbody2d;

         public GameObject bullet;
         public Transform firePoint;

         public GameObject turret;
         public GameObject barrel;
         
         // Use this for initialization
         void Start () {
            if (!isLocalPlayer)
            {
                return;
            }
            Camera.main.GetComponent<SmoothCamera2D>().target = transform;
             rigidbody2d = GetComponent<Rigidbody2D>();
         }

        public override void OnStartLocalPlayer()
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
            turret.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
         
     
         void FixedUpdate()
         {
            if (!isLocalPlayer)
            {
                return;
            }
             curspeed = new Vector2(rigidbody2d.velocity.x,    rigidbody2d.velocity.y);
     
             if (curspeed.magnitude > maxspeed)
             {
                 curspeed = curspeed.normalized;
                 curspeed *= maxspeed;
             }
     
             if (Input.GetKey(KeyCode.W))
             {
                 rigidbody2d.AddForce(-transform.up * power);
                  rigidbody2d.drag = friction;
             }
             if (Input.GetKey(KeyCode.S))
             {
                 rigidbody2d.AddForce(transform.up * (power/2));
                 rigidbody2d.drag = friction;
             }
              if (Input.GetKey(KeyCode.A))
             {
                 transform.Rotate(Vector3.forward * turnpower);
             }
              if (Input.GetKey(KeyCode.D))
             {
                 transform.Rotate(Vector3.forward * -turnpower);
             }
            if(Input.GetKey(KeyCode.R)){
                turret.GetComponent<Transform>().Rotate(0,0,2f);
            }
            if(Input.GetKey(KeyCode.T)){
                turret.GetComponent<Transform>().Rotate(0,0,-2f);
            }
     
             noGas();

             if(Input.GetKeyDown(KeyCode.Space)){
                CmdFire();
             }
     
         }
     
        [Command]
        void CmdFire(){
                Debug.Log("CMdFire -> "+gameObject.name);
                GameObject bulletInstance = Instantiate(bullet, firePoint.position, firePoint.rotation);
                bulletInstance.GetComponent<BulletController>().player = gameObject;
                Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), barrel.GetComponent<Collider2D>());		
                NetworkServer.Spawn(bulletInstance);
        }

         void noGas()
         {
             bool gas;
             if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
             {
                 gas = true;
             }
             else
             {
                 gas = false;
             }
     
             if (!gas)
             {
                 rigidbody2d.drag = friction * 2;
             }
         }
 }