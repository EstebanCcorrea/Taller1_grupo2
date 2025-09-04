using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Producto
{
    public string id;
    public string nombre;
    public string tipo;
    public float peso;
    public float precio;
    public float tiempo;

    public Producto(string id, string nombre, string tipo, float peso, float precio, float tiempo)
    {
        this.id = id;
        this.nombre = nombre;
        this.tipo = tipo;
        this.peso = peso;
        this.precio = precio;
        this.tiempo = tiempo;
    }
}
