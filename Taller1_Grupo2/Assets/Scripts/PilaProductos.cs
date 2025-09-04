using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class PilaProductos : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TMP_Text textPila;

    private List<Producto> catalogo = new List<Producto>();
    private List<string> lineasOriginales = new List<string>();

    void Start()
    {
        CargarProductosDesdeArchivo();
        MostrarProductosEnPantalla();
    }

    private void CargarProductosDesdeArchivo()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "productos.txt");

        if (!File.Exists(filePath))
        {
            Debug.LogError("No se encontró el archivo en: " + filePath);
            return;
        }

        string[] lineas = File.ReadAllLines(filePath);

        foreach (string linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea)) continue;

            string[] datos = linea.Split('|');

            if (datos.Length == 6 &&
                float.TryParse(datos[3], NumberStyles.Float, CultureInfo.InvariantCulture, out float peso) &&
                float.TryParse(datos[4], NumberStyles.Float, CultureInfo.InvariantCulture, out float precio) &&
                float.TryParse(datos[5], NumberStyles.Float, CultureInfo.InvariantCulture, out float tiempo))
            {
                Producto p = new Producto(datos[0], datos[1], datos[2], peso, precio, tiempo);
                catalogo.Add(p);
                lineasOriginales.Add(linea);
            }
            else
            {
                Debug.LogWarning("Línea ignorada por formato inválido: " + linea);
            }
        }

        Debug.Log($" Se cargaron {catalogo.Count} productos.");
    }



    private void MostrarProductosEnPantalla()
    {
        if (textPila == null) return;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Lista de productos:\n");

        foreach (string linea in lineasOriginales)
        {
            sb.AppendLine(linea);
        }

        textPila.text = sb.ToString();
    }
}
