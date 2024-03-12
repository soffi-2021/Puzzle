using UnityEngine;

/// <summary> ��������� ������ � ����-���� "������������� ������".</summary>
/// <remarks> ������ ����� ��������� ��� ����, 
/// ����� ���������� ����� �� ������ � ����-����.</remarks>
public class CastleActivator : MonoBehaviour
{
    /// <summary> ������ ��������� ������. </summary>
    [SerializeField] private GameObject[] Castles;

    /// <summary> ��� ������ ������� ���������� ���� �� 6 ��������� ������. </summary>    
    private void Start()
    {
        // HACK: ������� 6 ������ � ������� � �������� ����� ���� ���������.
        // ������ ������ �����, ��������� ������ � ����� - ArchitecturalWorks
        Castles[Random.Range(0, Castles.Length)].SetActive(true);
    }
}
