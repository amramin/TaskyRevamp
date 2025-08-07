namespace TaskyRevamp.Dto.GeneralDto;

public class DdlDto
{
    public Guid Id { get; set; }
    public string Name => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.Equals("ar") ? NameAr : NameEn;
    public string NameAr { get; set; }
    public string NameEn { get; set; }
}
