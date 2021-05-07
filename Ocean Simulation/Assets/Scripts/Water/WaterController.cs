using UnityEngine;

//Controlls the water
public class WaterController : MonoBehaviour {

	public static WaterController current;

    [Header("Gerstner Waves Variables")]

    [SerializeField]
    private GerstnerData[] waveData;

    [SerializeField]
    private Material material;

    public bool isGamePaused = false;

	void Awake() 
    {
        if (current != null) Destroy(this);

		current = this;

        waveData = GetDataFromMaterial();
	}

    public GerstnerData[] GetDataFromMaterial()
    {
        if (material != null)
        {
            GerstnerData[] data = {
                new GerstnerData(material.GetFloat("Wavelength1"), material.GetFloat("Speed1"), material.GetFloat("Steepness1"),  material.GetVector("Direction1")),
                new GerstnerData(material.GetFloat("Wavelength2"), material.GetFloat("Speed2"), material.GetFloat("Steepness2"),  material.GetVector("Direction2")),
                new GerstnerData(material.GetFloat("Wavelength3"), material.GetFloat("Speed3"), material.GetFloat("Steepness3"),  material.GetVector("Direction3")),
            };
            return data;
        }

        // Debug.LogError("GetDataFromMaterial(): Material is NULL!");
        return waveData;
    }

    public void SetData(GerstnerData data1, GerstnerData data2, GerstnerData data3)
    {
        if (material != null)
        {
            GerstnerData[] data = { data1, data2, data3 };
            waveData = data;

            material.SetFloat("Wavelength1", data1.WaveLength);
            material.SetFloat("Speed1", data1.Speed);
            material.SetFloat("Steepness1", data1.Steepness);
            material.SetVector("Direction1", data1.Direction);

            material.SetFloat("Wavelength2", data2.WaveLength);
            material.SetFloat("Speed2", data2.Speed);
            material.SetFloat("Steepness2", data2.Steepness);
            material.SetVector("Direction2", data2.Direction);

            material.SetFloat("Wavelength3", data3.WaveLength);
            material.SetFloat("Speed3", data3.Speed);
            material.SetFloat("Steepness3", data3.Steepness);
            material.SetVector("Direction3", data3.Direction);
        }
        else
        {
            Debug.LogError("SetDataInMaterial(): Material is NULL!");
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