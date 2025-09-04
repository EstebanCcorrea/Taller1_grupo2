using UnityEngine;
using System.Collections.Generic;

public class MetricasS: MonoBehaviour
{
    public int totalGenerados = 0;
    public int totalDespachados = 0;
    public float tiempoTotalDespacho = 0f;
    public Dictionary<string, int> despachadosPorTipo = new Dictionary<string, int>();

    public void RegistrarGenerados(int cantidad)
    {
        totalGenerados += cantidad;
    }

    public void RegistrarDespacho(string tipo, float tiempoDespacho)
    {
        totalDespachados++;
        tiempoTotalDespacho += tiempoDespacho;
        if (despachadosPorTipo.ContainsKey(tipo))
        {
            despachadosPorTipo[tipo]++;
        }
        else
        {
            despachadosPorTipo[tipo] = 1;
        }
    }
    public float ObtenerTiempoPromedioDespacho()
    {
        if (totalDespachados == 0) return 0f;
        return tiempoTotalDespacho / totalDespachados;
    }

    public string ObtenerTipoMasDespachado()
    {
        string tipoMas = "";
        int max = 0;

        foreach (var par in despachadosPorTipo)
        {
            if (par.Value > max)
            {
                max = par.Value;
                tipoMas = par.Key;
            }
        }

        return tipoMas;
    }

}
