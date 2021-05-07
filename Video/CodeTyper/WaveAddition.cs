public Vector3 GetWaveAddition(Vector3 position, float timeSinceStart)
{
  Vector3 result = new Vector3();

  foreach (GerstnerData data in waveData)
  {
    result += WaveTypes.GerstnerWave(position, data.Direction, data.Steepness, data.WaveLength, data.Speed, timeSinceStart);
  }

  return result;
}