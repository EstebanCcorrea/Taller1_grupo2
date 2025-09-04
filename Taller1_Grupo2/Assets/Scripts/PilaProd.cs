using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PilaProd : MonoBehaviour
{
    [SerializeField] 
    
    public TMP_Text textPila;
    public Button ButtonIniciar;
    public TMP_Text textDesapilado;

    public List<Producto> catalogo = new List<Producto>();
    public Stack<Producto> pila = new Stack<Producto>();
    public MetricasS scriptMetricas;


    private Coroutine despachoCoroutine;

    
    public void CargarProductosDesdeArchivo()
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
            }
            else
            {
                Debug.LogWarning("Línea ignorada por formato inválido: " + linea);
            }
        }

        Debug.Log($"Se cargaron {catalogo.Count} productos.");
    }

   
    public void IniciarSimulacion()
    {
        if (catalogo.Count == 0) return;

        // Genera 1 a 3 productos aleatorios
        int cantidad = Random.Range(1, 4);

        for (int i = 0; i < cantidad; i++)
        {
            int index = Random.Range(0, catalogo.Count);
            Producto seleccionado = catalogo[index];
            pila.Push(seleccionado);
        }

        ActualizarTextoPila();

        // Iniciamos la corutina de despacho si no estaba corriendo
        if (despachoCoroutine == null)
        {
            despachoCoroutine = StartCoroutine(DespacharProductos());
        }
    }

    // Corutina que desapila productos según su tiempo
    private IEnumerator DespacharProductos()
    {
        while (pila.Count > 0)
        {
            Producto actual = pila.Peek();          
            yield return new WaitForSeconds(actual.tiempo); 
            Producto desapilado=pila.Pop();                              
            ActualizarTextoPila();                   

            if (textDesapilado != null)
            {
                textDesapilado.text = $"Último despacho:\n{desapilado.id} | {desapilado.nombre} | {desapilado.tipo}";
            }
        }

        despachoCoroutine = null; 
    }

  
    public void ActualizarTextoPila()
    {
        if (textPila == null) return;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Pila de productos:\n");

        foreach (Producto p in pila)
        {
            sb.AppendLine($"{p.id} | {p.nombre} | {p.tipo} | Peso: {p.peso} | Precio: {p.precio} | Tiempo: {p.tiempo}");
        }

        sb.AppendLine($"\nTotal en pila: {pila.Count}");
        textPila.text = sb.ToString();
    }
    public void MostrarMetricas()
    {
        if (scriptMetricas != null)
        {
            scriptMetricas.MostrarMetricasUI();
        }
        else
        {
            Debug.LogWarning("No se ha asignado el script MetricasS.");
        }
    }


}

