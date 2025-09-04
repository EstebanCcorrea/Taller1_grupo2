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

    public override string ToString()
    {
        return $"{id} - {nombre} ({tipo}) | Peso: {peso} | Precio: {precio} | Tiempo: {tiempo}";
    }
}

