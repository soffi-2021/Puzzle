using UnityEngine;

/// <summary> Активатор замков в мини-игре "Архитектурные работы".</summary>
/// <remarks> Данный класс необходим для того, 
/// чтобы заспавнить замки на панели в мини-игре.</remarks>
public class CastleActivator : MonoBehaviour
{
    /// <summary> Массив доступных замков. </summary>
    [SerializeField] private GameObject[] Castles;

    /// <summary> При старте создаем активируем один из 6 доступных замков. </summary>    
    private void Start()
    {
        // HACK: сделать 6 замков в префабы и спавнить здесь один случайный.
        // Убрать данный класс, перенести логику в класс - ArchitecturalWorks
        Castles[Random.Range(0, Castles.Length)].SetActive(true);
    }
}
