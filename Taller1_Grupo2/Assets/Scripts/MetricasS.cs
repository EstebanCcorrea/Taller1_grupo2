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

    public PilaProd pilaProd; // Asignar desde el inspector

    public void MostrarMetricasUI()
    {
        if (pilaProd == null) return;

        textGenerados.text = $"Generados: {pilaProd.totalGenerados}";
        textDespachados.text = $"Despachados: {pilaProd.totalDespachados}";
        textPromedio.text = $"Promedio tiempo: {pilaProd.ObtenerTiempoPromedioDespacho():0.00}s";
        textMetricas.text = $"Metricas Globales: {pilaProd.ObtenerTipoMasDespachado()}";

        // Mostrar despachos por tipo
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Despachos por tipo:");
        foreach (var par in pilaProd.despachadosPorTipo)
        {
            sb.AppendLine($"{par.Key}: {par.Value}");
        }
        textPorTipo.text = sb.ToString();
    }
}
