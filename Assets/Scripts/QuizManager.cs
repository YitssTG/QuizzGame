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
        // Agrega las preguntas aquí:
        preguntas.AddNode(new Pregunta("El cielo es azul.", true));
        preguntas.AddNode(new Pregunta("Los gatos pueden volar.", false));
        preguntas.AddNode(new Pregunta("El agua hierve a 100 grados Celsius.", true));
        preguntas.AddNode(new Pregunta("El sol gira alrededor de la Tierra.", false));

        current = preguntas.Head;
        ActualizarPregunta();

        btnVerdadero.onClick.AddListener(() => Responder(true));
        btnFalso.onClick.AddListener(() => Responder(false));
        btnAnterior.onClick.AddListener(() => Retroceder());

        btnAnterior.interactable = false; // Deshabilitamos retroceso al inicio
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