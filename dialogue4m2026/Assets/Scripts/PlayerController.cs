using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controlador simples para movimento do jogador (bola) usando física.
/// - Recebe input 2D (Vector2) via Input System
/// - Aplica força no FixedUpdate
/// - Opcionalmente limita a velocidade horizontal
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Campos serializáveis
    [Header("Movement")]
    [Tooltip("Force multiplier applied every FixedUpdate based on input.")]
    [SerializeField] private float moveSpeed = 10f;

    [Tooltip("Maximum horizontal speed. Set to 0 or negative to disable clamping.")]
    [SerializeField] private float maxSpeed = 8f;

    [Tooltip("When enabled, movement is relative to the assigned camera's forward/right.")]
    [SerializeField] private bool cameraRelativeMovement = true;

    [Tooltip("Optional camera transform. If null the script will try Camera.main at Awake().")]
    [SerializeField] private Transform cameraTransform = null;
    #endregion

    #region Interno
    // input armazenado entre frames
    private Vector2 moveInput = Vector2.zero;
    private Rigidbody rb;
    #endregion

    #region Unity callbacks
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        // Bom padrão para uma bola rolante
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    /// <summary>
    /// Callback esperado do Input System. A action deve ser do tipo Value com Vector2.
    /// Pode ser ligado via PlayerInput (Invoke Unity Events) ou SendMessage-style.
    /// </summary>
    public void OnMove(InputValue value)
    {
        if (value == null) return;
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }
    #endregion

    #region Movimento
    private void ApplyMovement()
    {
        if (rb == null) return;

        // Converte input 2D para direção em world-space
        Vector3 desired = new Vector3(moveInput.x, 0f, moveInput.y);

        if (cameraRelativeMovement && cameraTransform)
        {
            Vector3 camForward = cameraTransform.forward;
            camForward.y = 0f;
            camForward.Normalize();
            Vector3 camRight = cameraTransform.right;
            camRight.y = 0f;
            camRight.Normalize();

            desired = camRight * moveInput.x + camForward * moveInput.y;
        }

        if (desired.sqrMagnitude > 1f)
            desired.Normalize();

        // Aplica força (ForceMode.Force em FixedUpdate = independente de frame rate)
        Vector3 force = desired * moveSpeed;
        rb.AddForce(force, ForceMode.Force);

        // Limita velocidade horizontal preservando a componente vertical
        if (maxSpeed > 0f)
        {
            // Use linearVelocity on newer Unity versions (preserves compatibility with Package Manager physics)
            Vector3 horizontalVel = rb.linearVelocity;
            horizontalVel.y = 0f;
            float speed = horizontalVel.magnitude;
            if (speed > maxSpeed)
            {
                Vector3 limited = horizontalVel.normalized * maxSpeed;
                rb.linearVelocity = new Vector3(limited.x, rb.linearVelocity.y, limited.z);
            }
        }
    }
    #endregion

    #region API pública (runtime tweaking)
    public void SetMoveSpeed(float speed) => moveSpeed = speed;
    public void SetMaxSpeed(float max) => maxSpeed = max;
    public void SetCameraRelative(bool isEnabled) => cameraRelativeMovement = isEnabled;
    public void SetCameraTransform(Transform t) => cameraTransform = t;
    #endregion
}