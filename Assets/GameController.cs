using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Pickup _pickupPrefab = default;
    [SerializeField]
    private Component roadMesh = default;
    [SerializeField]
    private TMP_Text _timeText;
    [SerializeField]
    private Canvas _gameCanvas;
    [SerializeField]
    private Canvas _endingCanvas;
    [SerializeField]
    private TMP_Text _finalTimeText;

    public static GameController Instance { get; private set; }
    public bool GameRunning { get; set; } = true;

    private float _gameplayTime = 0.0f;

    private void OnEnable() =>
        Instance = this;
    private void OnDisable() =>
        Instance = null;

    // Update is called once per frame
    void Awake ()
    {
        SpawnPickup(_pickupPrefab);
    }

    void Update ()
    {
        _gameplayTime += Time.deltaTime;
        _timeText.text = $"Time: {_gameplayTime:F2}";
    }

    public void EndGame ()
    {
        GameRunning = false;
        _finalTimeText.text = $"Time: {_gameplayTime:F2}";
        _gameCanvas.gameObject.SetActive(false);
        _endingCanvas.gameObject.SetActive(true);
    }

    void SpawnPickup (Pickup prefab)
    {
        List<Vector3> possiblePositions = new();

        for (int i = 3; i < roadMesh.transform.childCount - 1; i++)
        {
            GameObject child = roadMesh.transform.GetChild(i).gameObject;
            var childPosition = child.transform.position;
            possiblePositions.Add(childPosition);
        }

        for (int i = 0; i < possiblePositions.Count; i++)
        {
            var x = possiblePositions[i].x;
            var y = possiblePositions[i].y + 0.5f;
            var z = possiblePositions[i].z;

            if (x > 0)
            {
                var leftPickup = Instantiate(prefab);
                leftPickup.transform.position = new Vector3(x + 5f, y, z);
                var middlePickup = Instantiate(prefab);
                middlePickup.transform.position = new Vector3(x + 10f, y, z);
                var rightPickup = Instantiate(prefab);
                rightPickup.transform.position = new Vector3(x + 15f, y, z);
            } 
            else {
                var leftPickup = Instantiate(prefab);
                leftPickup.transform.position = new Vector3(x - 5f, y, z);
                var middlePickup = Instantiate(prefab);
                middlePickup.transform.position = new Vector3(x - 10f, y, z);
                var rightPickup = Instantiate(prefab);
                rightPickup.transform.position = new Vector3(x - 15f, y, z);
            }

        }
    }

    public void OnReplayButtonPressed() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void OnMainMenuButtonPressed() =>
        SceneManager.LoadScene("MainMenuScene");
}
