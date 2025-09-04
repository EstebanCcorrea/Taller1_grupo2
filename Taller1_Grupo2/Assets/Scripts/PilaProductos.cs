using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;

public class SimuladorProductos : MonoBehaviour
{
    public TMP_Text textoLista; 
    private List<Producto> catalogo = new List<Producto>();

    void Start()
    {
        CargarProductosDesdeArchivo();
        MostrarProductosEnPantalla();
    }

    private void CargarProductosDesdeArchivo()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "productos.txt");

        if (File.Exists(filePath))
        {
            string[] lineas = File.ReadAllLines(filePath);

            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;

                string[] datos = linea.Split('|');

                if (datos.Length == 6 &&
                    float.TryParse(datos[3], out float peso) &&
                    float.TryParse(datos[4], out float precio) &&
                    float.TryParse(datos[5], out float tiempo))
                {
                    Producto p = new Producto(datos[0], datos[1], datos[2], peso, precio, tiempo);
                    catalogo.Add(p);
                }
                else
                {
                    Debug.LogWarning("Línea ignorada por formato inválido: " + linea);
                }
            }
        }
        else
        {
            Debug.LogError("No se encontró el archivo en: " + filePath);
        }
    }

    private void MostrarProductosEnPantalla()
    {
        textoLista.text = "Productos cargados:\n\n";
        foreach (Producto p in catalogo)
        {
            textoLista.text += p.ToString() + "\n";
        }
    }
}
