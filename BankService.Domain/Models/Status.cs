
namespace BankSystem.Domain.Models;

/// <summary> Текущий статус контракта </summary>
public enum Status
{
    /// <summary> Контракт создан </summary>
    Created,
    /// <summary> Контракт заполнен </summary>
    Completed,
    /// <summary> Контракт направлен на ознакомление контрагенту</summary>
    ForAcquaintance,
    /// <summary> Контракт направлен наподписание сотруднику банка</summary>
    ForSigning,
    /// <summary> Контракт подписан </summary>
    Signed
}