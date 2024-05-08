using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePlayerMove : MonoBehaviour
{

    public float moveSpeed = 3.0f;
    public float rotationSpeed = 180.0f; // Velocidad de rotación en grados por segundo
    public float gravity = 9.81f; // Gravedad

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity; // Velocidad de caída

    private bool isLeftFoot = true; // Indica si el siguiente paso es con el pie izquierdo
    public AudioClip leftFootstepSound;
    public AudioClip rightFootstepSound;
    private AudioSource audioSource;
    private bool isPlayingFootstep = false; // Indica si se está reproduciendo un sonido de paso
    public float delayStep;


    float multiplier = 1;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = Vector3.zero;

        if (vertical > 0)
        {
            moveDirection = transform.forward * moveSpeed;
            animator.SetInteger("SmokeState", 1); // Caminar hacia adelante
            multiplier = 1;
        }
        else if (vertical < 0)
        {
            moveDirection = -transform.forward * moveSpeed / 3;
            animator.SetInteger("SmokeState", 2); // Caminar hacia atrás
            multiplier = 2.5f;
        }
        else
        {
            animator.SetInteger("SmokeState", 0); // Quieto
        }

        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);

        // Aplicar gravedad
        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0;
        }

        moveDirection.y = velocity.y;

        // Reproducir sonido de paso solo cuando se mueve hacia adelante o hacia atrás
        if (controller.isGrounded && (vertical > 0 || vertical < 0))
        {
            if (!isPlayingFootstep)
            {
                if (isLeftFoot)
                {
                    audioSource.PlayOneShot(leftFootstepSound);
                }
                else
                {
                    audioSource.PlayOneShot(rightFootstepSound);
                }

                isLeftFoot = !isLeftFoot; // Alternar entre pie izquierdo y derecho
                isPlayingFootstep = true; // Marcar que se está reproduciendo un sonido de paso
                StartCoroutine(ResetFootstep()); // Iniciar una corrutina para restablecer isPlayingFootstep
            }
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    IEnumerator ResetFootstep()
    {
        yield return new WaitForSeconds(delayStep * multiplier); // Esperar un tiempo antes de permitir otro paso
        isPlayingFootstep = false; // Permitir otro paso
    }
}
