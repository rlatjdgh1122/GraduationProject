using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaveManager : MonoBehaviour
{
    public Material waveMaterial;
    public Texture2D waveTexture;
    float[][] waveN, waveNm1, waveNp1;

    float Lx = 10;
    float Ly = 10;

    [SerializeField] private float dx = 0.1f;

    float dy { get => dx; }
    int nx, ny;

    public float CFL = 0.5f;
    public float c = 1;
    float dt;
    float t;

    private void Start()
    {
        nx = Mathf.FloorToInt(Lx / dx);
        ny = Mathf.FloorToInt(Ly / dy);
        waveTexture = new Texture2D(nx, ny, TextureFormat.RGBA32, false);

        waveN = new float[nx][];
        waveNm1 = new float[nx][];
        waveNp1 = new float[nx][];

        for (int i = 0; i < nx; i++)
        {
            waveN[i] = new float[ny];
            waveNm1[i] = new float[ny];
            waveNp1[i] = new float[ny];
        }

        waveMaterial.SetTexture("_MainTex", waveTexture);
        waveMaterial.SetTexture("_Displacement", waveTexture);
    }

    void WaveStep()
    {


        dt = CFL * dx / 2;
        t += dx;

        for (int i = 0; i < nx; i++)
        {

            for (int j = 0; j < ny; j++)
            {
                waveNm1[i][j] = waveN[i][j];
                waveN[i][j] = waveNp1[i][j];
            }
        }

        waveN[50][50] = dt * dt * 20 * Mathf.Sin(t*Mathf.Rad2Deg);

        for (int i = 1; i < nx - 1; i++) //do not process edges.
        {
            for (int j = 1; j < ny - 1; j++)
            {
                float n_ij = waveN[i][j];
                float n_ip1j = waveN[i + 1][j];
                float n_im1j = waveN[i - 1][j];
                float n_ijp1 = waveN[i][j + 1];
                float n_ijm1 = waveN[i][j - 1];
                float nm1_ij = waveNm1[i][j];
                waveNp1[i][j] = 2f * n_ij - nm1_ij + CFL * CFL * (n_ijm1 + n_ijp1 + n_im1j + n_ip1j - 4f * n_ij);
            }
        }
    }
    void ApplyMaterialToTexture(float[][] state, Texture2D tex)
    {
        for(int i = 0; i < nx; i++)
        {
            for(int j = 0; j < ny; j++)
            {
                float val = state[i][j];
                tex.SetPixel(i, j, new Color(val + 0.5f, val + 0.5f, val + 0.5f, 1f));
            }
        }
        tex.Apply();
    }

    private void Update()
    {
        WaveStep();
        ApplyMaterialToTexture(waveN, waveTexture);
    }
}
