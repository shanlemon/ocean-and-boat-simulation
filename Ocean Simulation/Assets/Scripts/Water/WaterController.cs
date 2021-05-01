using UnityEngine;

//Controlls the water
public class WaterController : MonoBehaviour {

	public static WaterController current;

    [Header("Gerstner Waves Variables")]

    [SerializeField]
    private GerstnerData[] waveData;

    [SerializeField]
    private Material material;

	void Awake() 
    {
        if (current != null) Destroy(this);

		current = this;
	}

    private void Start() 
    {
        if (material != null)
        {
            GerstnerData[] data = {
                new GerstnerData(material.GetFloat("Wavelength1"), material.GetFloat("Speed1"), material.GetFloat("Steepness1"),  material.GetVector("Direction1")),
                new GerstnerData(material.GetFloat("Wavelength2"), material.GetFloat("Speed2"), material.GetFloat("Steepness2"),  material.GetVector("Direction2")),
                new GerstnerData(material.GetFloat("Wavelength3"), material.GetFloat("Speed3"), material.GetFloat("Steepness3"),  material.GetVector("Direction3")),
            };
            waveData = data;
        }
    }

    public float getHeightAtPosition(Vector3 position) 
    {   
        float time = Time.timeSinceLevelLoad;
        Vector3 currentPosition = GetWaveAddition(position, time);

        for (int i = 0; i < 3; i++) {
            Vector3 diff = new Vector3(position.x - currentPosition.x, 0, position.z - currentPosition.z);
            currentPosition = GetWaveAddition(diff, time);
        }

        return currentPosition.y;
    }

    public Vector3 GetWaveAddition(Vector3 position, float timeSinceStart) 
    {
        Vector3 result = new Vector3();
        
        foreach (GerstnerData data in waveData) 
        {
            result += WaveTypes.GerstnerWave(position, data.Direction, data.Steepness, data.WaveLength, data.Speed, timeSinceStart);
        }

        return result;
        // GerstnerData data1 = waveData[0];
        // return WaveTypes.GerstnerWave(position, data1.Direction, data1.Steepness, data1.WaveLength, data1.Speed, timeSinceStart, debug);
    }
}

[System.Serializable]
public class GerstnerData {
    [Header("Gerstner Data")]
    public float WaveLength = 0.1f;
    public float Speed = 0.1f;
    [Range(0.0f, 1.0f)] public float Steepness = 0.5f;
    public Vector2 Direction = new Vector2(1, 0);

    public GerstnerData(float WaveLength, float Speed, float Steepness, Vector2 Direction) 
    {
        this.WaveLength = WaveLength;
        this.Speed = Speed;
        this.Steepness = Steepness;
        this.Direction = Direction;
    }
}