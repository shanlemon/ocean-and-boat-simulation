using UnityEngine;

//Controlls the water
public class WaterController : MonoBehaviour {

	public static WaterController current;

    [Header("Gerstner Waves Variables")]

    [SerializeField]
    private GerstnerData[] waveData;

	void Awake() {
        if (current != null) Destroy(this);

		current = this;
	}

    public Vector3 GetWaveAddition(Vector3 position, float timeSinceStart) 
    {
        Vector3 result = new Vector3();
        
        foreach (GerstnerData data in waveData) 
        {
            result += WaveTypes.GerstnerWave(position, data.Direction, data.Steepness, data.WaveLength, data.Speed, timeSinceStart) / waveData.Length;
        }

        return result;
    }
}

[System.Serializable]
public class GerstnerData {
    [Header("Gerstner Data")]
    public float WaveLength = 0.1f;
    public float Speed = 0.1f;
    [Range(0.0f, 1.0f)] public float Steepness = 0.5f;
    public Vector2 Direction = new Vector2(1, 0);
}