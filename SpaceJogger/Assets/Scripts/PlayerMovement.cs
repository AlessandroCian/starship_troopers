using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.SceneManagement; 


public class PlayerMovement : MonoBehaviour
{
    private bool turnLeft, turnRight;
    public float speed = 7.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 20.0f;
    private float verticalVelocity;
    private CharacterController myCharacterController;
    public bool jump;
    Thread thread;
    public int connectionPort = 8765;
    TcpListener server;
    TcpClient client;
    bool running;
    string position;
    string lastPosition;
    private bool canMove = false; // Flag to control movement


    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        ThreadStart ts = new ThreadStart(GetData);
        thread = new Thread(ts);
        thread.Start();
    }

     void GetData()
    {
        // Create the server
        server = new TcpListener(IPAddress.Any, connectionPort);
        server.Start();

        // Create a client to get the data stream
        client = server.AcceptTcpClient();

        // Start listening
        running = true;
        while (running)
        {
            Connection();
        }
        server.Stop();
    }

     void Connection()
    {
        // Read data from the network stream
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

        // Decode the bytes into a string
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        
        // Make sure we're not getting an empty string
        //dataReceived.Trim();
        if (dataReceived != null && dataReceived != "")
        {
            position = dataReceived;
            nwStream.Write(buffer, 0, bytesRead);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Access and print the current position
        if (Input.GetKeyDown(KeyCode.Space) && !canMove)
        {
            canMove = true; // Activate movement when space is pressed
        }

        if (!canMove) return; // Prevent movement until space bar is pressed

        Vector3 currentPosition = transform.position;

           if (transform.position.y < 1)
        {
            // Call Quit method if the y position is less than 1
            server.Stop();
            SceneManager.LoadScene(0);   
        }
        turnLeft = false;
        turnRight = false;
        jump = false;
        
        if (position == "right" && lastPosition != "right"){
         turnRight = true;  
        }
        if (position == "left" && lastPosition != "left"){
         turnLeft = true;  
        }
        if (position == "up" && lastPosition != "up"){
         jump = true;  
        }
        if (lastPosition != position){
        lastPosition = position;         
        }
        turnLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        turnRight = Input.GetKeyDown(KeyCode.RightArrow);

        if (turnLeft)
            transform.Rotate(new Vector3(0f, -90f, 0f));
        else if (turnRight)
            transform.Rotate(new Vector3(0f, 90f, 0f));

        Vector3 moveDirection = transform.forward * speed;

        // Check if the character is grounded
        if (myCharacterController.isGrounded)
        {
            // Jump
            if (Input.GetKey(KeyCode.UpArrow))
            {
                jump = true;
            }
            if (jump)
            {
                verticalVelocity = jumpSpeed;
                jump = false; // Reset jump flag after executing the jump
            }
            else
            {
                verticalVelocity = 0f;
            }
        }
        else
        {
            // Apply gravity
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        myCharacterController.Move(moveDirection * Time.deltaTime);
    }
}