namespace tmgcat.Bll.Interfaces;

public interface IDateTimeProvider
{
    DateTimeOffset GetCurrentTime();
}