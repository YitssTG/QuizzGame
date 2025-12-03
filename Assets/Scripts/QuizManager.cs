using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public DoubleLinkedList<Pregunta> preguntas = new DoubleLinkedList<Pregunta>();
    private Node<Pregunta> current;

    [Header("UI Elements")]
    public TextMeshProUGUI preguntaText;
    public Button btnVerdadero;
    public Button btnFalso;
    public TextMeshProUGUI feedbackText;
    public Button btnAnterior;

    void Start()
    {
        // Preguntas de cultura general
        preguntas.AddNode(new Pregunta("La Gran Muralla China es visible desde el espacio.", false));
        preguntas.AddNode(new Pregunta("El océano más grande del mundo es el Pacífico.", true));
        preguntas.AddNode(new Pregunta("La Torre Eiffel está en Italia.", false));
        preguntas.AddNode(new Pregunta("El ser humano tiene 206 huesos en el cuerpo.", true));
        preguntas.AddNode(new Pregunta("El café se originó en África.", true));
        preguntas.AddNode(new Pregunta("La capital de Australia es Sídney.", false)); // Es Canberra
        preguntas.AddNode(new Pregunta("Los murciélagos son mamíferos.", true));
        preguntas.AddNode(new Pregunta("El río Nilo es el más largo del mundo.", true));
        preguntas.AddNode(new Pregunta("El planeta más grande del sistema solar es Júpiter.", true));
        preguntas.AddNode(new Pregunta("La luz tarda 1 segundo en llegar de la Luna a la Tierra.", false)); // Tarda ~1.3 segundos

        current = preguntas.Head;
        ActualizarPregunta();

        btnVerdadero.onClick.AddListener(() => Responder(true));
        btnFalso.onClick.AddListener(() => Responder(false));
        btnAnterior.onClick.AddListener(() => Retroceder());

        btnAnterior.interactable = false;
        feedbackText.text = "";
    }

    void ActualizarPregunta()
    {
        if (current != null)
        {
            preguntaText.text = current.Value.ToString();
            feedbackText.text = "";
            btnAnterior.interactable = current.Prev != null;
        }
        else
        {
            preguntaText.text = "¡Has terminado el quiz!";
            feedbackText.text = "";
            btnVerdadero.interactable = false;
            btnFalso.interactable = false;
            btnAnterior.interactable = false;
        }
    }

    void Responder(bool respuestaUsuario)
    {
        if (current == null) return;

        if (respuestaUsuario == current.Value.respuestaCorrecta)
        {
            feedbackText.color = Color.green;
            feedbackText.text = "¡Correcto! Avanzando...";

            if (current.Next != null)
            {
                current = current.Next;
                ActualizarPregunta();
            }
            else
            {
                feedbackText.text = "¡Has completado el quiz!";
                btnVerdadero.interactable = false;
                btnFalso.interactable = false;
            }
        }
        else
        {
            // Si la respuesta fue falso y está mal, imprime en consola
            if (respuestaUsuario == false)
            {
                Debug.Log("Te equivocaste al elegir falso.");
            }

            feedbackText.color = Color.red;
            feedbackText.text = "Respuesta incorrecta. Intenta de nuevo.";
        }
    }

    void Retroceder()
    {
        if (current != null && current.Prev != null)
        {
            current = current.Prev;
            ActualizarPregunta();
        }
    }
}