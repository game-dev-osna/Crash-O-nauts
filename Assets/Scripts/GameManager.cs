using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;

            planets = new Queue<Planet>();
            players = new List<Player>();
            InputSystem.onDeviceChange += (device, change) => OnDeviceChange(device, change);
        }
    }

    private void Init()
    {
        foreach (Player player in players)
        {
            Destroy(player.gameObject);
        }
        players.Clear();
        foreach (Planet planet in planets)
        {
            Destroy(planet.gameObject);
        }
        planets.Clear();

        RegisterMainMenuPlayers();
    }

    public enum State { Pickup = 1, Escape = 2, Travel = 3, Fail = 4, Init = 5 };
    //TODO SET STATE TO INIT ON BACK TO MAIN MENU

    public State gamestate = State.Init;

    [System.Serializable]
    struct Planetdata
    {
        public Color grasscolor;
        public GameObject[] props;
        public float minRadius;
        public float maxRadius;
        public float timelimit;
    }

    [SerializeField]
    string menuScene;
    [SerializeField]
    string gameScene;
    [SerializeField]
    string selectionScene;
    [SerializeField]
    string creditScene;
    [SerializeField]
    string gameOverScene;

    [Header("PlanetCustomization")]
    [SerializeField]
    private Planetdata[] planetBlueprints;

    public GameObject impactParticleSystem;

    [Header("Sound")]
    public AudioSource source;
    public AudioClip startGame;
    public AudioClip rocketFinished;
    public AudioClip rocketNewPart;

    [Header("Options")]

    [SerializeField]
    private int maxPlayers = 0;
    [SerializeField]
    public float maxTime;
    [SerializeField]
    private float partScaleFactor = 0.2f;
    public float shuttleObjectsDistance = 0.0f;
    public float playersDistance = -4.0f;

    [Header("References")]

    public List<Player> players;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject selectionPrefab;
    [SerializeField]
    private GameObject planetPrefab;
    [SerializeField]
    private GameObject pathPrefab;

    public Planet activePlanet;


    //TODO Variables in general
    int maxPlanets = 3;
    int currentPlanet = 0;
    //maybe upgrades that whole group has
    public float totalTime = 250.0f;
    //this time is necessary for highscore calculations and/or which final ending


    private void RegisterMainMenuPlayers()
    {
        var devices = InputSystem.devices;
        for (int i = 0; i < devices.Count; i++)
            AddNewMainMenuPlayer(devices[i]);
    }

    void AddNewMainMenuPlayer(InputDevice device)
    {
        if (gamestate < State.Init)
            return;

        print("new player " + device.displayName);
        InputUser user = InputUser.PerformPairingWithDevice(device);

        MainMenuPlayer mainMenu = new GameObject().AddComponent<MainMenuPlayer>();
        mainMenu.Init(user);
    }

    //TODO Variables for current planet
    [SerializeField]
    private GameObject shuttlePrefab;

    private Shuttle shuttle;


    public float timer = 0.0f;
    //Entweder Counter
    int currentPickups = 0;
    //maybe check if all players are in shuttle
    SpacePath travelRoute;

    bool EverybodyDead()
    {
        foreach (Player player in players)
        {
            if (player.isAlive())
                return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO Overall logic:
        if (gamestate >= State.Init)
            return;

        if (timer >= maxTime)
            activePlanet.KillAllPlayersOnSurface();

        if (EverybodyDead())
            gamestate = State.Fail;

        if (gamestate == State.Fail)
            GameOver();

        if (gamestate == State.Escape || gamestate == State.Pickup)
        {
            //TODO planet-based logic:
            timer += Time.deltaTime;
            activePlanet.SetApocalypseLevel(timer / maxTime);
        }

        if (gamestate == State.Escape)
        {
            bool allSafe = true;
            for (int i = 0; i < players.Count; i++)
            {
                if (allSafe)
                    allSafe = players[i].IsOverShuttle();
            }
            if (allSafe)
            {
                gamestate = State.Travel;
                Debug.Log("Sucess");
                //PLANET WON
                currentPlanet++;
                totalTime += timer;

                if (currentPlanet == maxPlanets)
                {
                    //TODO FINAL GAME WON
                    //calculate correct ending based on time spent
                    Debug.Log("FINAL WIN - Score: " + totalTime);
                    Win();
                    return;
                }

                //foreach (Player player in players)
                //activePlanet.RemovePlayer(player);
                

                foreach (Player player in players)
                {
                    player.transform.parent = shuttle.transform;
                    player.transform.localPosition = Vector3.zero;
                    player.playerState = Player.PlayerState.Travelling;
                    player.rb.isKinematic = true;
                }

                //move planet in background
                Vector3 planetPos = activePlanet.transform.position;
                planetPos.x = 900.0f;
                activePlanet.transform.position = planetPos;

                shuttle.fireParticleSystem.gameObject.SetActive(false);
                shuttle.smokeParticleSystem.gameObject.SetActive(false);
                shuttle.engineParticleSystem.gameObject.SetActive(true);

                ActivateNextPlanet();
                Vector3 destination = SelectCrashPoint(activePlanet);
                travelRoute = Instantiate(pathPrefab).GetComponent<SpacePath>();
                travelRoute.Init(shuttle.transform.position, destination, -shuttle.transform.forward, activePlanet.crashNormal, shuttle.transform, 10.0f);
            }
        }

        if(gamestate == State.Travel)
        {
            bool arrived = travelRoute.AnimatePath();

            if (!arrived)
                return;

            foreach (Player player in players)
            {
                player.transform.parent = null;
                player.playerState = Player.PlayerState.Normal;
                player.rb.isKinematic = false;
            }

            SelectCrashPoint(activePlanet, true);
            Destroy(travelRoute.gameObject);
            InitActivePlanet();
        }
    }

    void AddPlanet()
    {
        planets.Enqueue(InitPlanet(planetOffset));
        planetOffset += new Vector3(100.0f, 20.0f, Random.Range(-20.0f, 20.0f));
    }

    Planet InitPlanet(Vector3 pos)
    {
        Planet planet = Instantiate(planetPrefab).GetComponent<Planet>();
        planet.transform.position += pos;
        PlanetFiller filler = planet.gameObject.GetComponent<PlanetFiller>();

        Planetdata pd = planetBlueprints[Random.Range(0, planetBlueprints.Length)];
        planet.maxTime = pd.timelimit;
        planet.grassColor = pd.grasscolor;

        filler.m_Prefabs = pd.props;

        ProceduralCylinder body = planet.gameObject.GetComponent<ProceduralCylinder>();
        body.minRadius = pd.minRadius;
        body.maxRadius = pd.maxRadius;

        body.Init();

        return planet;
    }

    void ActivateNextPlanet()
    {
        activePlanet = planets.Dequeue();
        timer = 0.0f;
        maxTime = activePlanet.maxTime;
    }

    void InitActivePlanet()
    {
        shuttle.fireParticleSystem.gameObject.SetActive(true);
        shuttle.smokeParticleSystem.gameObject.SetActive(true);
        shuttle.engineParticleSystem.gameObject.SetActive(false);

        SpawnShuttleParts(activePlanet);
        SpawnPlayers();

        foreach (Player player in players)
        {
            if (player.isAlive())
                player.SetPlanet(activePlanet);
        }

        gamestate = State.Pickup;
    }

    Vector3 SelectCrashPoint(Planet planet, bool keepOldDirection = false)
    {
        RaycastHit hit;

        Vector3 dir = planet.crashDirection;
        if (!keepOldDirection)
        {
            dir = Random.insideUnitCircle.normalized;
            dir.z = dir.x;
            dir.x = 0;
        }

        int layerMask = 1 << 10;
        Physics.Raycast(planet.transform.position, dir, out hit, Mathf.Infinity, layerMask);
        planet.crashPoint = hit.point;
        planet.crashNormal = hit.normal;
        planet.crashDirection = dir;
        return hit.point;
    }

    void SpawnShuttleParts(Planet planet)
    {
        currentPickups = 0;
        //initialize mesh (Denis stuff)
        //Place Pickups
        //Place shuttle
        //initialize planet variables
        // Place shuttle
        /*RaycastHit hit;
        Vector3 dir = Random.insideUnitCircle.normalized;
        dir.z = dir.x;
        dir.x = 0;
        if (Physics.Raycast(planet.transform.position, dir, out hit, Mathf.Infinity, layerMask))
        {*/
        int value = shuttle.shuttleParts.Count;
        var fire = shuttle.fireParticleSystem.main;
        fire.startLifetime = 5.0f * value;
        var smoke = shuttle.smokeParticleSystem.main;
        smoke.startLifetime = 5.0f * value;

        shuttle.transform.position = planet.crashPoint;
        shuttle.transform.rotation = Quaternion.identity;
        shuttle.gameObject.transform.LookAt(planet.transform.position, Vector3.Cross(planet.transform.up, (planet.transform.position - shuttle.gameObject.transform.position).normalized));
        shuttle.transform.Translate(shuttleObjectsDistance, 0, 0, Space.World);
        //}
        planet.Init(-9.7f);

        
        List<Vector3> pickupPositions = new List<Vector3>();
        pickupPositions.Add(new Vector3(shuttle.transform.position.x, shuttle.transform.position.y, shuttle.transform.position.z));
        int placingTry = 0;
        int maxIterations = 5;

        RaycastHit hit;
        int layerMask = 1 << 10;
        for (int i = 0; i < shuttle.shuttleParts.Count; i++)
        {
            RaycastHit partPlanetHit;
            Vector3 dir = Random.insideUnitCircle.normalized;
            dir.z = dir.x;
            dir.x = 0;
            //hits ONLY planet
            layerMask = 1 << 10;
            //this sucks!!
            Physics.Raycast(planet.transform.position, dir, out partPlanetHit, Mathf.Infinity, layerMask);
            float planetCircumference = 2 * partPlanetHit.distance * Mathf.PI;
            float placingDistance = planetCircumference / ((float)shuttle.shuttleParts.Count * 2);


            placingTry = 0;
            while (placingTry < maxIterations)
            {
                dir = Random.insideUnitCircle.normalized;
                dir.z = dir.x;
                dir.x = 0;
                bool canPlace = true;

                if (Physics.Raycast(planet.transform.position, dir, out partPlanetHit, Mathf.Infinity, layerMask))
                {

                    for (int p = 0; p < pickupPositions.Count; p++)
                    {
                        if (Vector3.Distance(pickupPositions[p], partPlanetHit.point) < placingDistance)
                        {
                            //is too close: jump back to while and increase placing try
                            placingTry++;
                            canPlace = false;
                            p = pickupPositions.Count;
                        }
                    }
                }
                if (canPlace) placingTry = maxIterations;
            }

            Debug.DrawRay(planet.transform.position, dir * 500, Color.yellow, 60);
            placingTry = 0;
            // Instantiate the part
            GameObject shuttlePart = shuttle.shuttleParts[i];
            Transform spawnTransform = shuttlePart.transform.Find("Spawnpoint");
            PickUp pickUp = spawnTransform.gameObject.AddComponent<PickUp>();
            int index = new int();
            index = i;
            pickUp.index = index;

            shuttlePart.transform.SetParent(null);
            shuttlePart.transform.localScale = Vector3.one * partScaleFactor;
            pickUp.GetComponent<Collider>().enabled = true;

            Vector3 up = partPlanetHit.point - planet.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-1, 0, 0), up);
            Quaternion offsetRotation = lookRotation * Quaternion.Inverse(spawnTransform.rotation);
            shuttlePart.transform.rotation = offsetRotation * shuttlePart.transform.rotation;


            Vector3 offsetPosition = partPlanetHit.point - spawnTransform.position;
            shuttlePart.transform.position += offsetPosition;
            shuttlePart.transform.Translate(shuttleObjectsDistance, 0, 0, Space.World);

            pickupPositions.Add(new Vector3(shuttlePart.transform.position.x, shuttlePart.transform.position.y, shuttlePart.transform.position.z));


        }

        StartCoroutine(SpawnPrefabForTime(impactParticleSystem, shuttle.transform.position, shuttle.transform.rotation));
    }

    Queue<Planet> planets;
    Vector3 planetOffset = Vector3.zero;

    public void InitLevel()
    {
        rocketNewPart = Resources.Load<AudioClip>("SFX/RocketSounds/RocketNewPart");
        rocketFinished = Resources.Load<AudioClip>("SFX/RocketSounds/RocketFinished");
        //canvas.gameObject.SetActive(false);
        //TODO first called in Start menu, then after each planet is won
        //Called whenever a new Planet is visited
        //reset planet variables
        //TODO reset destruction levels
        //canvas.SetActive(false);

        shuttle = Instantiate(shuttlePrefab).GetComponent<Shuttle>();

        //todo set pickups/time based on level
        currentPickups = 0;
        for (int i = 0; i < 3; i++)
            AddPlanet();
        //reset timer
        timer = 0.0f;
        totalTime = 0.0f;
        gamestate = State.Pickup;
        //todo spawn players

        ActivateNextPlanet();
        SelectCrashPoint(activePlanet);
        InitActivePlanet();
    }

    private IEnumerator MoveTransformToTarget(Transform toMove, Vector3 endPosition, Quaternion endRotation, float duration = 2.0f)
    {
        Vector3 startLocalPosition = toMove.localPosition;
        Quaternion startLocalRotation = toMove.localRotation;

        float startTime = Time.time;

        while (duration >= Time.time - startTime)
        {
            float percentage = (Time.time - startTime) / duration;

            toMove.localPosition = Vector3.Lerp(startLocalPosition, endPosition, percentage);
            toMove.localRotation = Quaternion.Slerp(startLocalRotation, endRotation, percentage);
            toMove.localScale = Vector3.Slerp(Vector3.one * partScaleFactor, Vector3.one, percentage);

            yield return null;
        }

        toMove.localPosition = endPosition;
        toMove.localRotation = endRotation;
    }

    private IEnumerator SpawnPrefabForTime(GameObject prefab, Vector3 position, Quaternion rotation, float seconds = 3.0f)
    {
        GameObject spawnedGameObject = Instantiate(prefab, position, rotation);

        yield return new WaitForSecondsRealtime(seconds);

        Destroy(spawnedGameObject);
    }

    public void ReturnPickup(PickUp pickUp)
    {
        pickUp.transform.parent.SetParent(shuttle.root);
        StartCoroutine(MoveTransformToTarget(pickUp.transform.parent, shuttle.partStartLocalPositions[pickUp.index], shuttle.partStartLocalRotations[pickUp.index]));

        currentPickups++;

        int value = shuttle.shuttleParts.Count - currentPickups;
        var fire = shuttle.fireParticleSystem.main;
        fire.startLifetime = 5.0f * value;
        var smoke = shuttle.smokeParticleSystem.main;
        smoke.startLifetime = 5.0f * value;

        if (currentPickups >= shuttle.shuttleParts.Count)
        {
            source.PlayOneShot(rocketFinished);
            Debug.Log("All Collected!");
            gamestate = State.Escape;
        } 
        else
        {
            source.PlayOneShot(rocketNewPart);
        }
    }


    Player FindLostPlayer(InputDevice device)
    {
        foreach (Player player in players)
            foreach (InputDevice playerDevice in player.user.lostDevices)
                if (playerDevice == device)
                    return player;
        return null;
    }

    Player FindPairedPlayer(InputDevice device)
    {
        foreach (Player player in players)
            foreach (InputDevice playerDevice in player.user.pairedDevices)
                if (playerDevice == device)
                    return player;
        return null;
    }

    bool IsDevicePaired(InputDevice device)
    {
        return FindPairedPlayer(device) != null;
    }

    void AddNewPlayer(InputDevice device)
    {
        //print(device.description);

        if (gamestate < State.Init)
            return;

        if (IsDevicePaired(device))
        {
            print("already paired " + device.displayName);
            return;
        }

        if (FindLostPlayer(device))
        {
            print("is lost device, reconnect?");
            return;
        }

        //print("new device " + device.displayName);
        int playerID = players.Count;

        if (playerID > maxPlayers)
            return;

        //print("new player " + device.displayName);
        InputUser user = InputUser.PerformPairingWithDevice(device);

        SkinSelecter selecter = Instantiate(selectionPrefab).GetComponent<SkinSelecter>();

        selecter.transform.position = new Vector3(((maxPlayers/-2f)+playerID)*3f+1,0f,0f);

        //player.transform.Translate(playersDistance, 0, 0, Space.World);

        selecter.name = "Selecter" + playerID;

        //players.Add(player);

        selecter.Init(user, playerID);


        Player player = Instantiate(playerPrefab).GetComponent<Player>();

        switch (device.displayName)
        {
            case "Keyboard":
                player.controlDevice = Player.ControlDevice.Keyboard;
                break;
            case "Xbox Controller":
                player.controlDevice = Player.ControlDevice.Xbox;
                break;
            case "Wireless Controller":
                player.controlDevice = Player.ControlDevice.Ps4;
                break;
            default:
                Debug.Log("Device not known, falling back to keyboard controls");
                player.controlDevice = Player.ControlDevice.Keyboard;
                break;
        }

        player.transform.position = new Vector3(0f, -20f, 0f);

        //player.transform.Translate(playersDistance, 0, 0, Space.World);

        player.name = "Player" + playerID;

        players.Add(player);

        player.Init(user, playerID);

        //player.transform.position =
        //InputControlScheme.FindControlSchemeForDevice(devices[i], user.actions.controlSchemes);
    }

    void RemovePlayer(InputDevice device)
    {
        Player player = FindPairedPlayer(device);

        if (!player)
            return;

        RemovePlayer(player);
    }

    void RemovePlayer(Player player)
    {
        print("player removed" + player);
        players.Remove(player);
        Destroy(player.gameObject);
    }

    void RemoveDisconnectedPlayers()
    {
        foreach (Player player in players)
        {
            if (player.user.pairedDevices.Count == 0)
                RemovePlayer(player);
        }
    }

    public void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                AddNewPlayer(device);
                print(device.displayName + " added!");
                // New Device.
                break;
            case InputDeviceChange.Disconnected:
                print(device.displayName + " disconnected!");
                // Device got unplugged.
                break;
            case InputDeviceChange.Reconnected:
                print(device.displayName + " reconnected!");
                // Plugged back in.
                break;
            case InputDeviceChange.Removed:
                print(device.displayName + " removed!");
                // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                break;
            case InputDeviceChange.Destroyed:
                print(device.displayName + " destroyed!");
                RemovePlayer(device);
                break;
            default:
                // See InputDeviceChange reference for other event types.
                break;
        }
    }

    private void RegisterPlayers()
    {
        var devices = InputSystem.devices;

        for (int i = 0; i < devices.Count; i++)
            AddNewPlayer(devices[i]);
    }

    private void SpawnPlayers()
    {
        int index = 0;
        foreach (Player player in players)
        {
            Vector3 pos = shuttle.transform.position - shuttle.transform.forward * 3.0f;
            pos.x = activePlanet.transform.position.x;
            Vector3 dir = Vector3.Cross(pos, activePlanet.transform.up).normalized;
            pos += index * dir * 3f;
            player.rb.velocity = Vector3.zero;
            player.Revive(pos);
            player.LoadSelection();
            index++;
        }

        //Debug.Break();
    }

    public void LoadCharacterSelection()
    {
        source.PlayOneShot(startGame);
        StartCoroutine(LoadSelectionScene());
        //canvas.SetActive(false);

        //init level (and start planet) before calling player.setplanet

    }

    public void NewGame()
    {
        gamestate = State.Init;
        StartCoroutine(LoadGameScene());
        //canvas.SetActive(false);

        //init level (and start planet) before calling player.setplanet

    }

    IEnumerator LoadSelectionScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(selectionScene, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        RegisterPlayers();
    }

    IEnumerator LoadGameScene()
    {
        source.PlayOneShot(startGame);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        InitLevel();
    }

    public void GameReset()
    {
        print("reset");
        /*Reset previous players
        foreach (Player player in players)
            player.gameObject.SetActive(false);*/

        foreach (Player player in players)
            player.Kill();
        SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
        RemoveDisconnectedPlayers();
        RegisterPlayers();
        gamestate = State.Init;
        Init();
    }

    public void Win()
    {
        gamestate = State.Init;
        foreach (Player player in players)
            player.Kill();
        SceneManager.LoadScene(creditScene, LoadSceneMode.Single);
    }

    public void GameOver()
    {
        gamestate = State.Init;
        SceneManager.LoadScene(gameOverScene, LoadSceneMode.Single);
    }
}
