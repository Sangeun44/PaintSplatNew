using UnityEngine;
using System.Collections.Generic;
using Photon;
using UnityEngine.UI;

public class BoardBehavior : Photon.MonoBehaviour {
    public Text scoreText;
    public GameObject SplatterPrefab;
    public GameObject imageTarget;

    public Text winText;
    public Text loseText;

    public float timer;
    public int score = 0;

    private List<GameObject> splatters = new List<GameObject>();

    private void Update () {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) {
            imageTarget.SetActive(!imageTarget.activeSelf);
        }

        if (score >= 1000) {
            winText.enabled = true;
            timer = 0;
        }
        if (timer > 500000) {
            loseText.enabled = true;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {

        var other = collision.collider.gameObject;
        Vector3 hit_position = other.transform.position;
        if (other.CompareTag("Ball"))
        {
            score += 100;
            scoreText.text = score.ToString();

            PhotonNetwork.Destroy(other);
            Quaternion rot =  Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1)) ; //*transform.rotation;
            var splatter = Instantiate(SplatterPrefab, hit_position, rot) as GameObject;

            splatter.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;

            splatters.Add(splatter);
        }

    }
}
