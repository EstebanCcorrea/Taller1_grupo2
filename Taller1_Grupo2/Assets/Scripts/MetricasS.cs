using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MetricasS: MonoBehaviour
{
    public TMP_Text textoMetricas;
    public TMP_Text textDespachosT;
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
    public void MostrarMetricasUI()
    {
        float promedio = ObtenerTiempoPromedioDespacho();
        string tipoTop = ObtenerTipoMasDespachado();

        textoMetricas.text = $" Métricas Globales:\n" +
                             $"Generados: {totalGenerados}\n" +
                             $"Despachados: {totalDespachados}\n" +
                             $"Promedio tiempo: {promedio:F2}s\n" +
                             $"Tipo más despachado: {tipoTop}";

        string resumenTipos = " Despachos por tipo:\n";
        foreach (var par in despachadosPorTipo)
        {
            resumenTipos += $"{par.Key}: {par.Value}\n";
        }

        textDespachosT.text = resumenTipos;
    }

}
