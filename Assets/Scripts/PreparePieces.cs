using UnityEngine;

public class PreparePieces : MonoBehaviour
{
    [SerializeField] private Transform[] pieces;

    private void Start()
    {
        RotatePieces();
        ShufflePieces();
    }

    private void RotatePieces()
    {
        foreach (var piece in pieces)
        {
            float angle = new System.Random().Next(0, 3) * 90;
            piece.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void ShufflePieces()
    {
        foreach (var piece in pieces)
        {
            int index = Random.Range(0, 6);
            var temporaryPosition = piece.position;
            piece.position = pieces[index].position;
            pieces[index].position = temporaryPosition;
        }
    }
}
