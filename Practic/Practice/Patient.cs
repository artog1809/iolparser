namespace Practice;

/// <summary>
/// Пациент
/// </summary>
/// <param name="Name">Имя</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="Sex">Пол</param>
/// <param name="PatientId">Id</param>
public record Patient(string Name, DateTime BirthDate, string Sex, string PatientId);