using UnityEngine;
using System.Collections;

public class SoundAnalysisBehazviour : MonoBehaviour
{

    [SerializeField]
    public AudioSource audioSource;

    public float factor = 1;
    public float meanfactor;
    public int EnergySize = 256;
    public int samples = 1024;
    public int filterFirst = 256;

    public float meanEnergy;
    public float[] energies;
    public float[] spectrum;

    private bool firstFill = false;
    private int startIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        energies = new float[EnergySize];
        spectrum = new float[samples];
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        int i = 1;

        float energy = 0;
        while (i < samples - 1)
        {
            if (i < filterFirst)
                energy += spectrum[i] * spectrum[i];

            Debug.DrawLine(new Vector3(i - 1, spectrum[i - 1] + 10, 0), new Vector3(i, spectrum[i] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            i++;
        }

        energies[startIndex++] = energy;
        startIndex %= EnergySize;
        if (!firstFill && startIndex == 0) firstFill = true;

        meanEnergy = 0;
        for (int j = 0; j < EnergySize; j++)
        {
            meanEnergy += energies[j];
        }

        if (!firstFill)
            meanEnergy /= startIndex;
        else
            meanEnergy /= EnergySize;

        int currentK;
        int k = 1;

        float dEnergyK, dEnergyK1;
        while (k < EnergySize - 1)
        {
            currentK = (k + startIndex);
            dEnergyK = (energies[currentK % EnergySize] - energies[(currentK - 1) % EnergySize]);
            dEnergyK1 = (energies[(currentK + 1) % EnergySize] - energies[currentK % EnergySize]);
            Debug.DrawLine(new Vector3(k - 1, energies[currentK % EnergySize] * 100 * factor, 0), new Vector3(k, energies[(currentK + 1) % EnergySize] * 100 * factor, 0), (energies[(currentK) % EnergySize] > meanEnergy) ? Color.red : Color.yellow);
            Debug.DrawLine(new Vector3(k - 1, dEnergyK * 100 * factor + 100, 0), new Vector3(k, dEnergyK1 * 100 * factor + 100, 0), Color.yellow);

            if (dEnergyK > 0 && dEnergyK1 < 0 && energies[currentK % EnergySize] > meanfactor * meanEnergy)
            {
                Debug.DrawLine(new Vector3(k - 1, 100, 0), new Vector3(k - 1, energies[currentK % EnergySize] * 100 * factor + 110), Color.black);
            }

            k++;
        }
    }
}