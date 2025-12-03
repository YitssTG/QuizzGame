public class Pregunta
{
    public string texto;
    public bool respuestaCorrecta;

    public Pregunta(string texto, bool respuestaCorrecta)
    {
        this.texto = texto;
        this.respuestaCorrecta = respuestaCorrecta;
    }

    public override string ToString()
    {
        return texto;
    }
}