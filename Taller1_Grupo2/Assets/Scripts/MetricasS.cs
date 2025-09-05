using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Text;

public class MetricasS : MonoBehaviour
{
    public TMP_Text textGenerados;
    public TMP_Text textDespachados;
    public TMP_Text textPromedio;
    public TMP_Text textMetricas;
    public TMP_Text textPorTipo;

    public PilaProd pilaProd; 

    public void MostrarMetricasUI()
    {
        if (pilaProd == null) return;

        textGenerados.text = $"Total generados: {pilaProd.totalGenerados}";
        textDespachados.text = $"Total despachados: {pilaProd.totalDespachados}";
        textPromedio.text = $"Tiempo promedio: {pilaProd.ObtenerTiempoPromedioDespacho():0.00}s";
        textMetricas.text = $"Tipo más despachado: \n{pilaProd.ObtenerTipoMasDespachado()}";

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Despachos por tipo:");
        foreach (var par in pilaProd.despachadosPorTipo)
        {
            sb.AppendLine($"{par.Key}: {par.Value}");
        }
        textPorTipo.text = sb.ToString();
    }
}
