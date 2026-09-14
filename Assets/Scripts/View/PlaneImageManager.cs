using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneImageManager : MonoBehaviour
{
    public enum MovementAxis
    {
        X,
        Y,
        Z
    }

    [Header("Coronal")]
    public Slider CoronalSlider;
    public Transform CoronalTransform;
    public Image CoronalImage;
    public List<Sprite> CoronalImages = new List<Sprite>();
    public MovementAxis CoronalMovementAxis;
    public float CoronalMinPosition;
    public float CoronalMaxPosition;

    [Header("Sagittal")]
    public Slider SagittalSlider;
    public Transform SagittalTransform;
    public Image SagittalImage;
    public List<Sprite> SagittalImages = new List<Sprite>();
    public MovementAxis SagittalMovementAxis;
    public float SagittalMinPosition;
    public float SagittalMaxPosition;

    [Header("Transverse")]
    public Slider TransverseSlider;
    public Transform TransverseTransform;
    public Image TransverseImage;
    public List<Sprite> TransverseImages = new List<Sprite>();
    public MovementAxis TransverseMovementAxis;
    public float TransverseMinPosition;
    public float TransverseMaxPosition;

    private float previousCoronalValue = -1f;
    private float previousSagittalValue = -1f;
    private float previousTransverseValue = -1f;

    private void Start()
    {
        SetupSlider(CoronalSlider, CoronalImages.Count);
        SetupSlider(SagittalSlider, SagittalImages.Count);
        SetupSlider(TransverseSlider, TransverseImages.Count);

        UpdateCoronal();
        UpdateSagittal();
        UpdateTransverse();
    }

    private void Update()
    {
        if (CoronalSlider.value != previousCoronalValue)
        {
            UpdateCoronal();
        }

        if (SagittalSlider.value != previousSagittalValue)
        {
            UpdateSagittal();
        }

        if (TransverseSlider.value != previousTransverseValue)
        {
            UpdateTransverse();
        }
    }

    private void SetupSlider(Slider slider, int imageCount)
    {
        slider.minValue = 0f;
        slider.maxValue = Mathf.Max(0, imageCount - 1);
        slider.wholeNumbers = true;
    }

    private void UpdateCoronal()
    {
        int index = Mathf.RoundToInt(CoronalSlider.value);

        CoronalImage.sprite = CoronalImages[index];
        previousCoronalValue = CoronalSlider.value;

        float normalizedValue = CoronalImages.Count <= 1
            ? 0f
            : index / (float)(CoronalImages.Count - 1);

        MoveTransform(
            CoronalTransform,
            CoronalMovementAxis,
            Mathf.Lerp(CoronalMinPosition, CoronalMaxPosition, normalizedValue));
    }

    private void UpdateSagittal()
    {
        int index = Mathf.RoundToInt(SagittalSlider.value);

        SagittalImage.sprite = SagittalImages[index];
        previousSagittalValue = SagittalSlider.value;

        float normalizedValue = SagittalImages.Count <= 1
            ? 0f
            : index / (float)(SagittalImages.Count - 1);

        MoveTransform(
            SagittalTransform,
            SagittalMovementAxis,
            Mathf.Lerp(SagittalMinPosition, SagittalMaxPosition, normalizedValue));
    }

    private void UpdateTransverse()
    {
        int index = Mathf.RoundToInt(TransverseSlider.value);

        TransverseImage.sprite = TransverseImages[index];
        previousTransverseValue = TransverseSlider.value;

        float normalizedValue = TransverseImages.Count <= 1
            ? 0f
            : index / (float)(TransverseImages.Count - 1);

        MoveTransform(
            TransverseTransform,
            TransverseMovementAxis,
            Mathf.Lerp(TransverseMinPosition, TransverseMaxPosition, normalizedValue));
    }

    private void MoveTransform(Transform target, MovementAxis axis, float position)
    {
        Vector3 targetPosition = target.position;

        switch (axis)
        {
            case MovementAxis.X:
                targetPosition.x = position;
                break;

            case MovementAxis.Y:
                targetPosition.y = position;
                break;

            case MovementAxis.Z:
                targetPosition.z = position;
                break;
        }

        target.position = targetPosition;
    }
}