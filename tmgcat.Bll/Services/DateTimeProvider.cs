using tmgcat.Bll.Interfaces;

namespace tmgcat.Bll.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset GetCurrentTime()
    {
        return DateTimeOffset.UtcNow;
    }
}